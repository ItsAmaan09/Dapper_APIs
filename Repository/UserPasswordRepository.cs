using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace DAPPERCRUD
{
	public class UserPasswordRepository : IUserPasswordRepository
	{
		private readonly DapperContext _context;
		public UserPasswordRepository(DapperContext context)
		{
			_context = context;
		}
		public async Task<UserPassword> GetUserPassword(int UserID)
		{
			try
			{
				var query = "SELECT * FROM userPassword WHERE UserID = @UserID";
				using (var connection = _context.CreateConnection())
				{
					var user = await connection.QuerySingleOrDefaultAsync<UserPassword>(query, new { UserID });
					return user;
				}
			}
			catch (System.Exception ex)
			{
				throw;
			}
		}
		public async Task<int> CreatePassword(UserPassword userPassword)
		{
			try
			{
				var query = "INSERT INTO userPassword (UserID, Password) VALUES (@UserID, @Password) " + "SELECT CAST(SCOPE_IDENTITY() AS int) ";

				var parameters = new DynamicParameters();
				parameters.Add("UserID",userPassword.UserID,System.Data.DbType.Int32);
				parameters.Add("Password",userPassword.Password,System.Data.DbType.String);

				using (var connection = _context.CreateConnection())
				{
					int id = await connection.QuerySingleAsync<int>(query,parameters);

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