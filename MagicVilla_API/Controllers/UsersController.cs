using MagicVilla_API.Models;
using MagicVilla_API.Models.Dtos;
using MagicVilla_API.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVilla_API.Controllers
{
	[Route("api/v{version:apiversion}/[controller]")]
	[ApiVersion("1.0")]
	[ApiVersion("2.0")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUserRepository _userRepository;
		protected APIResponse _apiResponse;

		public UsersController(IUserRepository userRepository)
		{
			_userRepository = userRepository;
			this._apiResponse = new();
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginRequestDTO model) 
		{
			var loginResponse = await _userRepository.Login(model);
			if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token)) 
			{
				_apiResponse.StatusCode = HttpStatusCode.BadRequest;
				_apiResponse.IsSuccess = false;
				_apiResponse.ErrorMessages.Add("Username or password is incorrect");
				return BadRequest(_apiResponse);
			}
			_apiResponse.StatusCode = HttpStatusCode.OK;
			_apiResponse.IsSuccess = true;
			_apiResponse.Result = loginResponse;
			return Ok(_apiResponse);
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterationRequestDTO model)
		{
			bool ifUsernameUnique = _userRepository.IsUniqueUser(model.UserName);
			if (!ifUsernameUnique) 
			{
				_apiResponse.StatusCode = HttpStatusCode.BadRequest;
				_apiResponse.IsSuccess = false;
				_apiResponse.ErrorMessages.Add("Username already exists");
				return BadRequest(_apiResponse);
			}
			var user = await _userRepository.Register(model);
			if (user == null) 
			{
				_apiResponse.StatusCode = HttpStatusCode.BadRequest;
				_apiResponse.IsSuccess = false;
				_apiResponse.ErrorMessages.Add("Error while registering");
				return BadRequest(_apiResponse);
			}
			_apiResponse.StatusCode = HttpStatusCode.OK;
			_apiResponse.IsSuccess = true;
			return BadRequest(_apiResponse);
		}

	}
}
