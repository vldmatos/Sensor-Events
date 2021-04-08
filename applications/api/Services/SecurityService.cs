using API.Models;
using API.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
	public class SecurityService
	{
		private static readonly List<User> Users = new List<User>()
		{
			new User(){ Id = 1, Username = "user-creator", Password = "092813", Role = Roles.Creator },
			new User(){ Id = 2, Username = "user-viewer",  Password = "552341", Role = Roles.Viewer },
		};

		public static User GetUser(string username, string password)
		{
			var user = Users.Where(user => user.Username.ToLower() == username.ToLower() && user.Password == password).FirstOrDefault();
			if (user == null) return null;

			return new User() { Id = user.Id, Username = user.Username, Role = user.Role };
		}
	}
}
