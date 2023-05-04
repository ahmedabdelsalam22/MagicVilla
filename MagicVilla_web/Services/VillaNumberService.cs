using MagicVilla_Utility;
using MagicVilla_web.Models;
using MagicVilla_web.Models.Dtos;
using MagicVilla_web.Services.IServices;

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

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = villlaUrl + "/api/villaAPI",
            });
        }
        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = villlaUrl + "/api/villaNumberAPI/" + id,
            });
        }
        public Task<T> CreateAsync<T>(VillaNumberCreateDTO dto)
        {
            return SendAsync<T>(new ApiRequest() 
            {
                ApiType = SD.ApiType.POST,
                Url = villlaUrl+ "/api/villaNumberAPI",
                Data = dto,
            });
        }
        public Task<T> UpdateAsync<T>(VillaNumberUpdateDTO dto)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Url = villlaUrl + "/api/villaNumberAPI/" + dto.VillaId,
                Data = dto,
            });
        }
        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = villlaUrl + "/api/VillaNumberAPI/" + id,            
            });
        }

    }
}
