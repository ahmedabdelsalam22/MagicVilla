using MagicVilla_Utility;
using MagicVilla_web.Models;
using MagicVilla_web.Models.Dtos;
using MagicVilla_web.Services.IServices;
using MagicVilla_Web.Models.Dtos;

namespace MagicVilla_web.Services
{
	public class AuthService : BaseService,IAuthService
	{
		private readonly IHttpClientFactory _clientFactory;
		private string _authUrl;

		public AuthService(IHttpClientFactory clientFactory,IConfiguration configuration):base(clientFactory)
		{
			_clientFactory = clientFactory;
			_authUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
		}

		public Task<T> LoginAsync<T>(LoginRequestDTO obj)
		{
			return SendAsync<T>(new ApiRequest()
			{
				ApiType = SD.ApiType.POST,
				Url = _authUrl + "/api/v1/users/login",
				Data = obj,
			});
		}

		public Task<T> RegisterAsync<T>(UserDto obj)
		{
			return SendAsync<T>(new ApiRequest()
			{
				ApiType = SD.ApiType.POST,
				Url = _authUrl + "/api/v1/users/register",
				Data = obj,
			});
		}
	}
}
