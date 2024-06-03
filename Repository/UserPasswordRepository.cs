using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DAPPERCRUD
{
	public class UserPasswordRepository : IUserPasswordRepository
	{
		private readonly string connectionString = string.Empty;
		public UserPasswordRepository()
		{
			this.connectionString = DapperContext.Instance.GetDatabaseConnectionString();
		}
		public UserPassword GetUserPassword(int UserID)
		{
			try
			{
				var query = "SELECT * FROM userPassword WHERE UserID = @UserID";
				using (var connection = new SqlConnection(this.connectionString))
				{
					var user =  connection.QuerySingleOrDefault<UserPassword>(query, new { UserID });
					return user;
				}
			}
			catch (System.Exception ex)
			{
				throw;
			}
		}
		public int CreatePassword(UserPassword userPassword)
		{
			try
			{
				var query = "INSERT INTO userPassword (UserID, Password) VALUES (@UserID, @Password) " + "SELECT CAST(SCOPE_IDENTITY() AS int) ";

				var parameters = new DynamicParameters();
				parameters.Add("UserID",userPassword.UserID,System.Data.DbType.Int32);
				parameters.Add("Password",userPassword.Password,System.Data.DbType.String);

				using (var connection = new SqlConnection(this.connectionString))
				{
					int id =  connection.QuerySingle<int>(query,parameters);

					return id;
				}
			}
			catch (System.Exception ex)
			{
				 throw;
			}
		}
	}
}