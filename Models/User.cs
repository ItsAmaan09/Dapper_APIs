using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAPPERCRUD
{
	public class User
	{
		public int UserID { get; set; }
		public string UserName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Gender { get; set; }
		public string EmailAddress { get; set; }
		public string MobileNumber { get; set; }
	}
	public class UserPassword
	{
		public int UserPasswordID { get; set; }
		public int UserID { get; set; }
		public string Password { get; set; }
	}
}