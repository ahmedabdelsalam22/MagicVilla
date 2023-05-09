using MagicVilla_API.Models;
using MagicVilla_API.Models.Dtos;

namespace MagicVilla_API.Repository.IRepository
{
	public interface IUserRepository
	{
		bool IsUniqueUser(string username);
		Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
		Task<UserDTO> Register(RegisterationRequestDTO registerationRequestDTO);
	}
}
