using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DAPPERCRUD;

namespace DAPPERCRUD
{
	public class UserRepository : IUserRepository
	{
		private readonly DapperContext _context;
		public UserRepository(DapperContext context)
		{
			_context = context;
		}
		public async Task<IEnumerable<User>> GetUsers()
		{
			var query = "SELECT * FROM Users";
			using (var connection = _context.CreateConnection())
			{
				var users = await connection.QueryAsync<User>(query);
				return users.ToList();
			}
		}
		public async Task<User> GetUserDetails(int UserID)
		{
			var query = "SELECT * FROM Users WHERE UserID = @UserID";
			using (var connection = _context.CreateConnection())
			{
				var user = await connection.QuerySingleOrDefaultAsync<User>(query,new {UserID});
				return user;
			}
		}
	}
}