using MagicVilla_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Villa> GetVillas() 
        {
           return new List<Villa>() 
           {
             new Villa {Id = 1,Name = "Pool View"},
             new Villa {Id = 1,Name = "Beach View"},
           };
        }
    }
}
