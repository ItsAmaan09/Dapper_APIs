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
		public IEnumerable<User> GetUsers()
		{
			var users =  _userRepository.GetUsers();
			return users.ToList();
		}
		public User GetUserDetails(int id)
		{
			var user =  _userRepository.GetUserDetails(id);
			return user;
		}
		public User GetUserByUsername(string name)
		{
			var user =  _userRepository.GetUserByUsername(name);
			return user;
		}
		public User AddUser(User user)
		{
			var userCreated =  _userRepository.AddUser(user);
			return userCreated;
		}

		public void IsUserVerified(UserLogin userLogin)
		{
			var user =  GetUserByUsername(userLogin.Username);
			if (user == null) { throw new Exception("User not found"); }
			else
			{
				this._userPasswordManager = new UserPasswordManager();
				var result =  _userPasswordManager.ValidateUserCredentials(user.UserID, userLogin.Password);
				if (!result) { throw new Exception("Password is invalid"); }
			}
		}
	}
}