
namespace DAPPERCRUD;

	public interface IUserRepository
	{
		public IEnumerable<User> GetUsers();
		public User GetUserDetails(int id);
		public User AddUser(User user);
		public User GetUserByUsername(string username);
		// public Task<User> UpdateUser(int id,User user);
		// public Task<User> DeleteUser(int id);
	}
