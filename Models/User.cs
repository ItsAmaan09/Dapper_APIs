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
		public bool IsValid()
		{
			return !string.IsNullOrWhiteSpace(this.UserName)
			&& !string.IsNullOrWhiteSpace(this.FirstName)
			&& !string.IsNullOrWhiteSpace(this.LastName)
			&& !string.IsNullOrWhiteSpace(this.EmailAddress);
		}
	}
}