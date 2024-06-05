namespace DAPPERCRUD
{
	public class UserManager
	{
		private IUserRepository _userRepository;
		private UserPasswordManager _userPasswordManager;
		public UserManager()
		{
			this._userRepository = new UserRepository();
		}
		public IEnumerable<User> GetUsers()
		{
			var users = _userRepository.GetUsers();
			return users.ToList();
		}
		public User GetUserDetails(int id)
		{
			var user = _userRepository.GetUserDetails(id);
			return user;
		}
		public User GetUserByUsername(string name,string email)
		{
			var user = _userRepository.GetUserByUsernameOrEmail(name,email);
			return user;
		}
		public User AddUser(User user)
		{
			try
			{
				if (!user.IsValid())
				{
					throw new Exception("User is not valid");
				}

				if (this.IsDuplicateUser(user))
				{
					throw new Exception("User with same username already exists");
				}

				if(this.IsDuplicateUserEmail(user))
				{
					throw new Exception("User with same email already exists");
				}

				var userCreated = _userRepository.AddUser(user);
				return userCreated;

			}
			catch (Exception ex)
			{
				throw;
			}
		}

		public void IsUserVerified(UserLogin userLogin)
		{
			var user = GetUserByUsername(userLogin.Username,null);
			if (user == null) { throw new Exception("User not found"); }
			else
			{
				this._userPasswordManager = new UserPasswordManager();
				var result = _userPasswordManager.ValidateUserCredentials(user.UserID, userLogin.Password);
				if (!result) { throw new Exception("Password is invalid"); }
			}
		}
		public bool IsDuplicateUser(User user)
		{
			User user1 = this.GetUserByUsername(user.UserName,null);

			if (user1 != null)
			{
				return true;
			}
			return false;
		}
		public bool IsDuplicateUserEmail(User user)
		{
			User user1 = this.GetUserByUsername(null,user.EmailAddress);
			if (user1 != null)
			{
				return true;
			}
			return false;
		}
	}
}