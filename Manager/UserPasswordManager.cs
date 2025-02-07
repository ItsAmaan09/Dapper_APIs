using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using BCrypt.Net;

namespace DAPPERCRUD
{
	public class UserPasswordManager
	{
		private IUserPasswordRepository _userPasswordRepository;
		private UserManager _userManager;
		public UserPasswordManager()
		{
			_userPasswordRepository = new UserPasswordRepository();
		}
		public UserPassword GetUserPassword(int id)
		{
			var user =  _userPasswordRepository.GetUserPassword(id);
			return user;
		}
		public int CreatePassword(UserPassword userPassword)
		{
			try
			{
				if (!IsUserExists(userPassword))
				{
					throw new Exception("User not found");
				}
				userPassword.Password = passwordHash(userPassword.Password);
				var id =  _userPasswordRepository.CreatePassword(userPassword);
				return id;
			}
			catch (System.Exception ex)
			{
				throw;
			}
		}

		public bool IsUserExists(UserPassword userPassword)
		{
			this._userManager = new UserManager();
			var result =  _userManager.GetUserDetails(userPassword.UserID);
			if (result == null) { return false; }
			return true;
		}

		public string passwordHash(string password)
		{
			password = BCrypt.Net.BCrypt.HashPassword(password);
			return password;
		}
		public bool verifyPassword(string enteredPassword, string storedHashedPassword)
		{
			return BCrypt.Net.BCrypt.Verify(enteredPassword, storedHashedPassword);
		}

		public bool ValidateUserCredentials(int userID, string enteredPassword)
		{
			var userPassword =  GetUserPassword(userID);
			if (userPassword == null) { return false; }
			bool isPasswordValid = verifyPassword(enteredPassword, userPassword.Password);
			return isPasswordValid;
		}
	}
}