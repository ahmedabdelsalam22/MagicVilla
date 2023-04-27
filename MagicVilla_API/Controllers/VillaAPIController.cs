using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDto>> GetVillas()
        {
            return Ok(VillaStore.VillaList);
        }

        [HttpGet("id",Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<VillaDto> GetVilla(int id)
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<VillaDto> CreateVilla([FromBody] VillaDto villaDto)
        {
            //if (!ModelState.IsValid) 
            //{
            //    return BadRequest(ModelState);
            //}

            if (VillaStore.VillaList.FirstOrDefault(u => u.Name.ToLower() == villaDto.Name.ToLower()) !=null) 
            {
                ModelState.AddModelError("customError","Villa already exists");
                return BadRequest(ModelState);
            }
            if (villaDto == null)
            {
                return BadRequest(villaDto);
            }
            if (villaDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            villaDto.Id = VillaStore.VillaList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;

            VillaStore.VillaList.Add(villaDto);

            //return Ok(villaDto);
            return CreatedAtRoute("GetVilla", new {id = villaDto.Id} ,villaDto);
        }

        [HttpDelete("id",Name ="DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteVilla(int id) 
        {
            if (id == 0) 
            {
                return BadRequest();
            }
            var villa = VillaStore.VillaList.FirstOrDefault(u=>u.Id == id);
            if (villa == null) 
            {
                return NotFound();
            }
            VillaStore.VillaList.Remove(villa);
            return NoContent();
        }

        [HttpPut("id", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDto villaDto) 
        {
            if (villaDto == null | id != villaDto.Id)
            {
                return BadRequest();
            }
            var villa = VillaStore.VillaList.FirstOrDefault(u => u.Id == id);
            villa.Name = villaDto.Name;
            villa.Occupancy = villaDto.Occupancy;
            villa.Sqft = villaDto.Sqft;
            return NoContent();
        }

        [HttpPut("id", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdatePartialVilla(int id,JsonPatchDocument<VillaDto> patchDto)
        {
            if (patchDto == null || id == 0) 
            {
                return BadRequest();
            }
            var villa = VillaStore.VillaList.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            patchDto.ApplyTo(villa, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}
