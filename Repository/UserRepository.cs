using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DAPPERCRUD;
using Microsoft.Data.SqlClient;

namespace DAPPERCRUD
{
	public class UserRepository : IUserRepository
	{
		// private readonly DapperContext _context;
		private readonly string connectionString = string.Empty;
		public UserRepository()
		{
			this.connectionString = DapperContext.Instance.GetDatabaseConnectionString();
		}
		public async Task<IEnumerable<User>> GetUsers()
		{
			var query = "SELECT * FROM Users";
			using (var connection = new SqlConnection(this.connectionString))
			{
				var users = await connection.QueryAsync<User>(query);
				return users.ToList();
			}
		}
		public async Task<User> GetUserDetails(int UserID)
		{
			var query = "SELECT * FROM Users WHERE UserID = @UserID";
			using (var connection = new SqlConnection(this.connectionString))
			{
				var user = await connection.QuerySingleOrDefaultAsync<User>(query, new { UserID });
				return user;
			}
		}
		public async Task<User> GetUserByUsername(string UserName)
		{
			var query = "SELECT * FROM Users WHERE UserName = @UserName";
			using (var connection = new SqlConnection(this.connectionString))
			{
				var user = await connection.QuerySingleOrDefaultAsync<User>(query, new { UserName });
				return user;
			}
		}
		public async Task<User> AddUser(User user)
		{
			var query = "INSERT INTO Users (UserName,FirstName,LastName,Gender,EmailAddress,MobileNumber,IsActive,IsDeleted,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn) VALUES (@UserName,@FirstName,@LastName,@Gender,@EmailAddress,@MobileNumber,1,0,@CreatedBy,@CreatedOn,@ModifiedBy,@ModifiedOn)" +
			"SELECT CAST(SCOPE_IDENTITY() as int)";
			var parameters = new DynamicParameters();
			parameters.Add("UserName", user.UserName, DbType.String);
			parameters.Add("FirstName", user.FirstName, DbType.String);
			parameters.Add("LastName", user.LastName, DbType.String);
			parameters.Add("Gender", user.Gender, DbType.String);
			parameters.Add("EmailAddress", user.EmailAddress, DbType.String);
			parameters.Add("MobileNumber", user.MobileNumber, DbType.String);
			parameters.Add("CreatedBy","MA",DbType.String);
			parameters.Add("CreatedOn",DateTime.Now,DbType.DateTime);
			parameters.Add("ModifiedBy",null,DbType.String);
			parameters.Add("ModifiedOn",null,DbType.DateTime);

			using (var connection = new SqlConnection(this.connectionString))
			{
				int id = await connection.QuerySingleAsync<int>(query, parameters);

				var userCreated = new User
				{
					UserID = id,
					UserName = user.UserName,
					FirstName = user.FirstName,
					LastName = user.LastName,
					Gender = user.Gender,
					EmailAddress = user.EmailAddress,
					MobileNumber = user.MobileNumber
				};
				return userCreated;
			}
		}
	}
}