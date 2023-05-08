using MagicVilla_Utility;
using MagicVilla_web.Models;
using MagicVilla_web.Models.Dtos;
using MagicVilla_web.Services.IServices;

namespace MagicVilla_web.Services
{
    public class VillaService : BaseService,IVillaService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string villlaUrl;
        public VillaService(IHttpClientFactory clientFactory,IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            villlaUrl = configuration.GetValue<String>("ServiceUrls:VillaAPI");
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = villlaUrl + "/api/v1/villaAPI",
                Token = token
            });
        }
        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = villlaUrl + "/api/v1/villaAPI/" + id,
                Token = token
            });
        }
        public Task<T> CreateAsync<T>(VillaCreateDto dto, string token)
        {
            return SendAsync<T>(new ApiRequest() 
            {
                ApiType = SD.ApiType.POST,
                Url = villlaUrl+ "/api/v1/villaAPI",
                Data = dto,
                Token = token
            });
        }
        public Task<T> UpdateAsync<T>(VillaUpdateDto dto, string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Url = villlaUrl + "/api/v1/villaAPI/" + dto.Id,
                Data = dto,
                Token = token
            });
        }
        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = villlaUrl + "/api/v1/VillaAPI/" + id,
                Token = token
            });
        }

    }
}
