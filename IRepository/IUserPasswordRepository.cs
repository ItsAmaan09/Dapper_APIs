using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAPPERCRUD
{
	public interface IUserPasswordRepository
	{
		public Task<UserPassword> GetUserPassword(int userID);
		public Task<int> CreatePassword(UserPassword userPassword);
	}
}