using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAPPERCRUD
{
	public class UserManager
	{
		private IUserRepository _userRepository;
		private  UserPasswordManager _userPasswordManager;
		public UserManager()
		{
			this._userRepository = new UserRepository();
		}
		public async Task<IEnumerable<User>> GetUsers()
		{
			var users = await _userRepository.GetUsers();
			return users.ToList();
		}
		public async Task<User> GetUserDetails(int id)
		{
			var user = await _userRepository.GetUserDetails(id);
			return user;
		}
		public async Task<User> GetUserByUsername(string name)
		{
			var user = await _userRepository.GetUserByUsername(name);
			return user;
		}
		public async Task<User> AddUser(User user)
		{
			var userCreated = await _userRepository.AddUser(user);
			return userCreated;
		}

		public async Task IsUserVerified(UserLogin userLogin)
		{
			var user = await GetUserByUsername(userLogin.Username);
			if (user == null) { throw new Exception("User not found"); }
			else
			{
				this._userPasswordManager = new UserPasswordManager();
				var result = await _userPasswordManager.ValidateUserCredentials(user.UserID, userLogin.Password);
				if (!result) { throw new Exception("Password is invalid"); }
			}
		}
	}
}