using System.Text;
using DAPPERCRUD;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// JWT AUTH @start

// var key = Encoding.ASCII.GetBytes("this_is_a_very_secure_key_that_is_at_least_32_bytes_long!");
var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
	options.SaveToken = true;
	options.RequireHttpsMetadata = false;
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		// ValidateLifetime = false,
		// ValidateIssuerSigningKey = true,
		ValidIssuer = builder.Configuration["Jwt:Issuer"],
		ValidAudience = builder.Configuration["Jwt:Audience"],
		IssuerSigningKey = new SymmetricSecurityKey(key)
	};
});
builder.Services.AddAuthorization();
// JWT AUTH @end

builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserPasswordRepository, UserPasswordRepository>();

builder.Services.AddScoped<CustomerManager>();
builder.Services.AddScoped<UserManager>();
builder.Services.AddScoped<UserPasswordManager>();


builder.Services.AddControllers().AddJsonOptions(options =>
{
	options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
