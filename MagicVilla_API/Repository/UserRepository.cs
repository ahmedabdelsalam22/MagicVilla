using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dtos;
using MagicVilla_API.Repository.IRepository;

namespace MagicVilla_API.Repository
{
	public class UserRepository : IUserRepository
	{
		private readonly ApplicationDbContext _db;

		public UserRepository(ApplicationDbContext db)
		{
			_db = db;
		}

		public bool IsUniqueUser(string username)
		{
			var user = _db.LocalUsers.FirstOrDefault(u=>u.UserName == username);
			if (user == null)
			{
				return true;
			}
			else {
				return false;
			}
		}

		public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
		{
			throw new NotImplementedException();
		}

		public async Task<LocalUser> Register(RegisterationRequestDTO registerationRequestDTO)
		{
			LocalUser user = new LocalUser()
			{
				UserName = registerationRequestDTO.UserName,
				Name = registerationRequestDTO.Name,
				Role = registerationRequestDTO.Role,
				Password = registerationRequestDTO.Password
			};

			_db.LocalUsers.Add(user);
			await _db.SaveChangesAsync();
			user.Password = "";
			return user;
		}
	}
}
