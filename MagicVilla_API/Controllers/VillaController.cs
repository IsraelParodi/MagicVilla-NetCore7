using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Models.DTO;
using MagicVilla_API.Utils;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MagicVilla_API.Controllers
{
    [Route("api/villa")]
    public class VillaController : Controller
    {

        // GET api/villa
        [HttpGet(Name = "GetVillas")]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            return Ok(new ApiResponse<List<VillaDTO>>(StatusCodes.Status200OK, "Villas found", VillaStore.villas));
        }

        // GET api/villa/1
        [HttpGet("{id}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            if (id == 0) return BadRequest();

            var villa = VillaStore.villas.FirstOrDefault(villa => villa.Id == id);

            if (villa == null) return NotFound();

            return Ok(new ApiResponse<VillaDTO>(StatusCodes.Status200OK, $"Villa with Id {id} found", villa));
        }

        // POST api/villa
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<VillaDTO>> Post([FromBody] VillaDTO villaDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (VillaStore.villas.FirstOrDefault(villa => villa.Name.ToLower() == villaDto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("NameExist", "There is a villa record with that name");
                return BadRequest(ModelState);
            }

            if (villaDto == null) return BadRequest();
            if (villaDto.Id > 0) return StatusCode(StatusCodes.Status500InternalServerError);

            villaDto.Id = VillaStore.villas.Max(villa => villa.Id) + 1;
            VillaStore.villas.Add(villaDto);
            // To return the villa created
            //return CreatedAtRoute("GetVillas", new { id = villa.Id }, villa);

            // To return all the villas
            return CreatedAtRoute("GetVillas", null, VillaStore.villas);
        }

        // PUT api/villa/1
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Put(int id, [FromBody] VillaDTO villaUpdate)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id == 0) return BadRequest();
            var villa = VillaStore.villas.FirstOrDefault(villa => villa.Id == id);
            if (villa == null) return NotFound();

            villa.Name = villaUpdate.Name;
            villa.Occupants = villaUpdate.Occupants;
            villa.SquareMeter = villaUpdate.SquareMeter;

            return Ok(new ApiResponse<VillaDTO>(StatusCodes.Status200OK, $"Villa with Id {id} updated", villa));
        }

        // PATCH api/villa/1
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Patch(int id, JsonPatchDocument<VillaDTO> villaPatch)
        {

            Console.WriteLine(villaPatch.GetType().FullName);
            Console.WriteLine(villaPatch.ToString());
            if (id == 0 || villaPatch == null) return BadRequest();

            var villa = VillaStore.villas.FirstOrDefault(v => v.Id == id);
            if (villa == null) return NotFound();

            villaPatch.ApplyTo(villa, ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(new ApiResponse<VillaDTO>(StatusCodes.Status200OK, $"Villa with Id {id} updated partially", villa));
        }

        // DELETE api/villa/1
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();
            var villa = VillaStore.villas.FirstOrDefault(villa => villa.Id == id);
            if (villa == null) return NotFound();
            VillaStore.villas.Remove(villa);

            return Accepted(new ApiResponse<VillaDTO>(StatusCodes.Status202Accepted, "Villa deleted successfully", villa));
        }
    }
}

