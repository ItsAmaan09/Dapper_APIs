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
var key = Encoding.ASCII.GetBytes(builder.Configuration["JWT:Key"]);
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
		ValidIssuer = builder.Configuration["JWT:Issuer"],
		ValidAudience = builder.Configuration["JWT:Audience"],
		IssuerSigningKey = new SymmetricSecurityKey(key)
	};
	options.Events = new JwtBearerEvents
	{
		OnAuthenticationFailed = context =>
		{
			Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
			return Task.CompletedTask;
		},
		OnTokenValidated = context =>
		{
			Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
			return Task.CompletedTask;
		}
	};
});
builder.Services.AddAuthorization();
// JWT AUTH @end

// builder.Services.AddSingleton<DapperContext>();
// builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
// builder.Services.AddScoped<IUserPasswordRepository, UserPasswordRepository>();
// builder.Services.AddScoped<IUserRepository, UserRepository>();

// builder.Services.AddScoped<CustomerManager>();
// builder.Services.AddScoped<UserPasswordManager>();
// builder.Services.AddScoped<UserManager>();


builder.Services.AddControllers().AddJsonOptions(options =>
{
	options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

var app = builder.Build();
var configurationHelper = DapperContext.Instance;

configurationHelper.Initialize(app.Configuration);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
// IConfiguration configuration = app.Configuration;
// IWebHostEnvironment environment = app.Environment;
app.UseAuthorization();

app.MapControllers();

app.Run();
