using MagicVilla_Utility;
using MagicVilla_web.Models;
using MagicVilla_web.Models.Dtos;
using MagicVilla_web.Services.IServices;
using NuGet.Common;

namespace MagicVilla_web.Services
{
    public class VillaNumberService : BaseService, IVillaNumberService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string villlaUrl;
        public VillaNumberService(IHttpClientFactory clientFactory,IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            villlaUrl = configuration.GetValue<String>("ServiceUrls:VillaAPI");
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = villlaUrl + "/api/v2/villaAPI",
                Token = token
            });
        }
        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = villlaUrl + "/api/v2/villaNumberAPI/" + id,
                Token = token
            });
        }
        public Task<T> CreateAsync<T>(VillaNumberCreateDTO dto, string token)
        {
            return SendAsync<T>(new ApiRequest() 
            {
                ApiType = SD.ApiType.POST,
                Url = villlaUrl+ "/api/v2/villaNumberAPI",
                Data = dto,
                Token = token
            });
        }
        public Task<T> UpdateAsync<T>(VillaNumberUpdateDTO dto, string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Url = villlaUrl + "/api/v2/villaNumberAPI/" + dto.VillaNo,
                Data = dto,
                Token = token
            });
        }
        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = villlaUrl + "/api/v2/VillaNumberAPI/" + id,
                Token = token
            });
        }

    }
}
