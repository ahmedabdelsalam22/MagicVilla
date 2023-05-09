using AutoMapper;
using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dtos;
using MagicVilla_API.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MagicVilla_API.Repository
{
	public class UserRepository : IUserRepository
	{
		private readonly ApplicationDbContext _db;
		private string secretKey;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IMapper _mapper;

		public UserRepository(ApplicationDbContext db,IConfiguration configuration,
            UserManager<ApplicationUser> userManager, IMapper mapper)
		{
			_db = db;
			secretKey = configuration.GetValue<string>("ApiSettings:Secret");
			_userManager = userManager;
			_mapper = mapper;

        }

		public bool IsUniqueUser(string username)
		{
			var user = _db.ApplicationUsers.FirstOrDefault(u=>u.UserName == username);
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
			var user = _db.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDTO.UserName.ToLower());

			bool isValid = await _userManager.CheckPasswordAsync(user , loginRequestDTO.Password);

			if (user == null || isValid == false) 
			{
				return new LoginResponseDTO() 
				{
					User = null,
					Token = ""
				};
			}

			// if user is found .. generate jwt token.

			var roles = await _userManager.GetRolesAsync(user);

			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(secretKey);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[] 
				{
					new Claim(ClaimTypes.Name,user.Id.ToString()),
					new Claim(ClaimTypes.Role,roles.FirstOrDefault()),
				}),
			    Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key) , SecurityAlgorithms.HmacSha256Signature),
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);

			LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
			{
				User = _mapper.Map<UserDTO>(user),
				Token = tokenHandler.WriteToken(token),
				Role = roles.FirstOrDefault()
            };
			return loginResponseDTO;
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
