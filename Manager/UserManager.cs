using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAPPERCRUD
{
	public class UserManager
	{
		private IUserRepository _userRepository;
		public UserManager(IUserRepository userRepository)
		{
			_userRepository = userRepository;
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
		public async Task<User> AddUser(User user)
		{
			var userCreated = await _userRepository.AddUser(user);
			return userCreated;
		}
	}
}