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
            throw new NotImplementedException();
        }
        public Task<T> GetAsync<T>(int id)
        {
            throw new NotImplementedException();
        }
        public Task<T> CreateAsync<T>(VillaCreateDto dto)
        {
            throw new NotImplementedException();
        }
        public Task<T> UpdateAsync<T>(VillaUpdateDto dto)
        {
            throw new NotImplementedException();
        }
        public Task<T> DeleteAsync<T>(int id)
        {
            throw new NotImplementedException();
        }

    }
}
