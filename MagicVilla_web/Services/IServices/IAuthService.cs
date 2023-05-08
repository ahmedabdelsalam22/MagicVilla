using MagicVilla_web.Models.Dtos;
using MagicVilla_Web.Models.Dtos;

namespace MagicVilla_web.Services.IServices
{
	public interface IAuthService
	{
		Task<T> LoginAsync<T>(LoginRequestDTO obj);
		Task<T> RegisterAsync<T>(UserDto obj);
	}
}
