using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NexusProject.API.Data;
using NexusProject.API.Helpers;
using NexusProject.Shared.DTOs;
using NexusProject.Shared.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NexusProject.API.Controllers
{


   [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/api/Tutors")]
    public class TutorController : ControllerBase
    {

        private readonly DataContext _context;

        public TutorController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Tutors
             .AsQueryable();
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Id.ToString().Contains(pagination.Filter.ToLower()));
            }
            return Ok(await queryable
            .OrderBy(x => x.Id)
            .Paginate(pagination)
            .ToListAsync());
        }

        [HttpGet("totalPages")]
        public async Task<ActionResult> GetPages([FromQuery] PaginationDTO pagination)

        {
            var queryable = _context.Tutors.AsQueryable();
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Id.ToString().Contains(pagination.Filter.ToLower()));
            }
            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return Ok(totalPages);
        }

        //Method Create
        [HttpPost]
        public async Task<ActionResult> PostAsync(Tutor tutor)
        {
            _context.Add(tutor);
         
                    await _context.SaveChangesAsync();
                    return Ok(tutor);
               
            
        }

        //Method Get by ID (Read)
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetAsync(string id)
        {
            var tutor = await _context.Tutors.FirstOrDefaultAsync
                (x => x.UserDocument == id);

            if (tutor == null)
            {
                return NotFound();
            }
            return Ok(tutor);
        }

        //Method Update
        [HttpPut]
        public async Task<ActionResult> PutAsync(Tutor tutor)
        {
            _context.Update(tutor);
          
                    await _context.SaveChangesAsync();
                    return Ok(tutor);
               
        }

        //Metod Delete
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var tutor = await _context.Tutors.FirstOrDefaultAsync
                  (x => x.Id == id);

            if (tutor == null)
            {
                return NotFound();
            }
            _context.Remove(tutor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [AllowAnonymous]
        [HttpGet("combo")]
        public async Task<ActionResult> GetCombo()
        {
            return Ok(await _context.Tutors.ToListAsync());
        }

    }
}
