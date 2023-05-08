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

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = villlaUrl + "/api/v1/villaAPI",
            });
        }
        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = villlaUrl + "/api/v1/villaAPI/" + id,
            });
        }
        public Task<T> CreateAsync<T>(VillaCreateDto dto)
        {
            return SendAsync<T>(new ApiRequest() 
            {
                ApiType = SD.ApiType.POST,
                Url = villlaUrl+ "/api/v1/villaAPI",
                Data = dto,
            });
        }
        public Task<T> UpdateAsync<T>(VillaUpdateDto dto)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Url = villlaUrl + "/api/v1/villaAPI/" + dto.Id,
                Data = dto,
            });
        }
        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = villlaUrl + "/api/v1/VillaAPI/" + id,            
            });
        }

    }
}
