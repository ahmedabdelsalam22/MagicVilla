using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public VillaAPIController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDto>>> GetVillas()
        {
            var villas = await _dbContext.Villas.ToListAsync();
            return Ok(villas);
        }

        [HttpGet("id", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<VillaDto>> GetVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest("Id 0 is not allowed!");
            }
            var villaDto =await _dbContext.Villas.FirstOrDefaultAsync(v => v.Id == id);
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
        public async Task<ActionResult<VillaDto>> CreateVilla([FromBody] VillaCreateDto villaDto)
        {
            bool isFound = await _dbContext.Villas.FirstOrDefaultAsync(u => u.Name.ToLower() == villaDto.Name.ToLower()) != null;


            if (isFound)
            {
                ModelState.AddModelError("customError", "Villa already exists");
                return BadRequest(ModelState);
            }
            if (villaDto == null)
            {
                return BadRequest(villaDto);
            }
           
            Villa villa = new()
            {
                Name = villaDto.Name,
                Details = villaDto.Details,
                Occupancy = villaDto.Occupancy,
                Sqft = villaDto.Sqft,
                Rate = villaDto.Rate,
                ImageUrl = villaDto.ImageUrl,
                Amenity = villaDto.Amenity,
                CreatedDate = villaDto.CreatedDate,
                UpdatedDate = villaDto.UpdatedDate,
            };
            await _dbContext.Villas.AddAsync(villa);
            await _dbContext.SaveChangesAsync();

            return CreatedAtRoute("GetVilla", new { id = villa.Id }, villa);
        }

        [HttpDelete("id", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = await _dbContext.Villas.FirstOrDefaultAsync(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            _dbContext.Villas.Remove(villa);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("id", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDto villaDto)
        {
            if (villaDto == null | id != villaDto.Id)
            {
                return BadRequest();
            }

            Villa villa = new()
            {
                Id = villaDto.Id,
                Name = villaDto.Name,
                Details = villaDto.Details,
                Occupancy = villaDto.Occupancy,
                Sqft = villaDto.Sqft,
                Rate = villaDto.Rate,
                ImageUrl = villaDto.ImageUrl,
                Amenity = villaDto.Amenity,
                CreatedDate = villaDto.CreatedDate,
                UpdatedDate = villaDto.UpdatedDate,
            };
             _dbContext.Villas.Update(villa);
           await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("id", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
            var villa =await _dbContext.Villas.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

            VillaUpdateDto villaDto = new()
            {
                Id = villa.Id,
                Name = villa.Name,
                Details = villa.Details,
                Occupancy = villa.Occupancy,
                Sqft = villa.Sqft,
                Rate = villa.Rate,
                ImageUrl = villa.ImageUrl,
                Amenity = villa.Amenity,
                CreatedDate = villa.CreatedDate,
                UpdatedDate = villa.UpdatedDate,
            };

            if (villa == null)
            {
                return NotFound();
            }
            patchDto.ApplyTo(villaDto, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Villa _villa = new()
            {
                Id = villaDto.Id,
                Name = villaDto.Name,
                Details = villaDto.Details,
                Occupancy = villaDto.Occupancy,
                Sqft = villaDto.Sqft,
                Rate = villaDto.Rate,
                ImageUrl = villaDto.ImageUrl,
                Amenity = villaDto.Amenity,
                CreatedDate = villaDto.CreatedDate,
                UpdatedDate = villaDto.UpdatedDate,
            };
            _dbContext.Villas.Update(_villa);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
