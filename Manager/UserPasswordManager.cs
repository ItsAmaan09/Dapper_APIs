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
		private readonly IUserPasswordRepository _userPasswordRepository;
		public UserPasswordManager(IUserPasswordRepository userPasswordRepository)
		{
			_userPasswordRepository = userPasswordRepository;
		}
		public async Task<UserPassword> GetUserPassword(int id)
		{
			var user = await _userPasswordRepository.GetUserPassword(id);
			return user;
		}
		public async Task<int> CreatePassword(UserPassword userPassword)
		{
			try
			{
				// if (IsUserExists(userPassword))
				// {

				// }
				userPassword.Password = passwordHash(userPassword.Password);
				var id = await _userPasswordRepository.CreatePassword(userPassword);
				return id;
			}
			catch (System.Exception ex)
			{
				throw;
			}
		}

		// public bool IsUserExists(UserPassword userPassword)
		// {
		// 	return true;
		// }

		public string passwordHash(string password)
		{
			password = BCrypt.Net.BCrypt.HashPassword(password);
			return password;
		}
		public bool verifyPassword(string enteredPassword,string storedHashedPassword)
		{
			return BCrypt.Net.BCrypt.Verify(enteredPassword,storedHashedPassword);
		}

		public async Task<bool> ValidateUserCredentials(int userID,string enteredPassword)
		{
			var userPassword = await GetUserPassword(userID);
			if(userPassword == null) { return false; }
			bool isPasswordValid = verifyPassword(enteredPassword,userPassword.Password);
			return isPasswordValid;
		}
	}
}