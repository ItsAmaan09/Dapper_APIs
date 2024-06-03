using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAPPERCRUD
{
	public interface IUserPasswordRepository
	{
		public UserPassword GetUserPassword(int userID);
		public int CreatePassword(UserPassword userPassword);
	}
}