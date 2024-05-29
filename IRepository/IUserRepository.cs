
namespace DAPPERCRUD;

	public interface IUserRepository
	{
		public Task<IEnumerable<User>> GetUsers();
		public Task<User> GetUserDetails(int id);
		public Task<User> AddUser(User user);
		public Task<User> GetUserByUsername(string username);
		// public Task<User> UpdateUser(int id,User user);
		// public Task<User> DeleteUser(int id);
	}
