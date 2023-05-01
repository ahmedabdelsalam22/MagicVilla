using AutoMapper;
using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dtos;
using MagicVilla_API.Repository.IRepository;
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
        private readonly IVillaRepository _repository;
        private readonly IMapper _Mapper;
        public VillaAPIController(IVillaRepository repository, IMapper Mapper)
        {
            _repository = repository;
            _Mapper = Mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDto>>> GetVillas()
        {
            IEnumerable<Villa> villas = await _repository.GetAllAsync();

            var villaDto = _Mapper.Map<List<VillaDto>>(villas);

            return Ok(villaDto);
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
            var villa = await _repository.GetAsync(v => v.Id == id);
            if (villa == null)
            {
                return NotFound($"No villa found with id: {id}");
            }

            var villaDto = _Mapper.Map<VillaDto>(villa);

            return Ok(villaDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<VillaDto>> CreateVilla([FromBody] VillaCreateDto createDto)
        {
            bool isFound = await _repository.GetAsync(filter: u => u.Name.ToLower() == createDto.Name.ToLower()) != null;


            if (isFound)
            {
                ModelState.AddModelError("customError", "Villa already exists");
                return BadRequest(ModelState);
            }
            if (createDto == null)
            {
                return BadRequest(createDto);
            }
            Villa villa = _Mapper.Map<Villa>(createDto);

            await _repository.CreateAsync(villa);

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
            var villa = await _repository.GetAsync(filter: u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            await _repository.RemoveAsync(villa);
            return NoContent();
        }

        [HttpPut("id", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDto updateDto)
        {
            if (updateDto == null | id != updateDto.Id)
            {
                return BadRequest();
            }
            Villa villa = _Mapper.Map<Villa>(updateDto);

            await _repository.UpdateAsync(villa);
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
            var villa = await _repository.GetAsync( filter: u => u.Id == id ,tracked:false);

            VillaUpdateDto villaDto = _Mapper.Map<VillaUpdateDto>(villa);
           

            if (villa == null)
            {
                return NotFound();
            }
            patchDto.ApplyTo(villaDto, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Villa _villa = _Mapper.Map<Villa>(villaDto);

            await _repository.UpdateAsync(_villa);
            return NoContent();
        }
    }
}
