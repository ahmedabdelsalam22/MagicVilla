using MagicVilla_web.Models.Dtos;

namespace MagicVilla_Web.Models.Dtos
{
	public class LoginResponseDTO
	{
        public UserDto User { get; set; }
        public string Token { get; set; }
    }
}
