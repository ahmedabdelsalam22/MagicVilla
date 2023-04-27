using MagicVilla_API.Data;
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
        public ActionResult<IEnumerable<VillaDto>> GetVillas() 
        {
            return Ok(VillaStore.VillaList);
        }

        [HttpGet("id")]
        public ActionResult<VillaDto> GetVilla (int id) 
        {
            if (id == 0)
            {
                return BadRequest("Id 0 is not allowed!");
            }
            var villaDto = VillaStore.VillaList.FirstOrDefault(v => v.Id == id);
            if (villaDto == null) 
            {
                return NotFound($"No villa found with id: {id}");
            }
            return Ok(villaDto);
        }
    }
}
