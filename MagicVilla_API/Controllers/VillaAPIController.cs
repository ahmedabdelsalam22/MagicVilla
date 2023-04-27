using MagicVilla_API.Models;
using MagicVilla_API.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<VillaDto> GetVillas() 
        {
           return new List<VillaDto>() 
           {
             new VillaDto {Id = 1,Name = "Pool View"},
             new VillaDto {Id = 1,Name = "Beach View"},
           };
        }
    }
}
