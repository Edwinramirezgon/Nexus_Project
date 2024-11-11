using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexusProject.API.Data;
using NexusProject.Shared.DTOs;
using System.Threading.Tasks;
using System;
using System.Linq;
using NexusProject.API.Helpers;
using Microsoft.EntityFrameworkCore;
using NexusProject.Shared.Entities;

namespace NexusProject.API.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/api/ActivityCollection3")]
    public class ActivityCollectionController3 : ControllerBase
    {

        private readonly DataContext _context;

        //Constructor
        public ActivityCollectionController3(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.ActivityColections
             .AsQueryable();
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Id.ToString().ToLower().Contains(pagination.Filter.ToLower()));
            }
            return Ok(await queryable
            .OrderBy(x => x.Id)
            .Paginate(pagination)
            .ToListAsync());
        }

        [HttpGet("totalPages")]
        public async Task<ActionResult> GetPages([FromQuery] PaginationDTO pagination)

        {
            var queryable = _context.ActivityColections.AsQueryable();
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Id.ToString().ToLower().Contains(pagination.Filter.ToLower()));
            }
            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return Ok(totalPages);
        }


        [HttpPost("CreateActivity")]
        public async Task<ActionResult> CreateActivity([FromBody] Activity model)
        {

            Activity activity = model;

            _context.Add(activity);
            await _context.SaveChangesAsync();

            return Ok(activity);

        }


        //Method Create
        [HttpPost]
        public async Task<ActionResult<Activity>> PostAsync(Activity activity)
        {
            _context.Add(activity);
            await _context.SaveChangesAsync();


            return Ok(activity);
        }

        //Method Get by ID (Read)
        [HttpGet("{Code:int}")]
        public async Task<ActionResult> GetAsync(int Code)
        {
            var activity = await _context.ActivityColections.FirstOrDefaultAsync
                (x => x.Id == Code);

            if (activity == null)
            {
                return NotFound();
            }
            return Ok(activity);
        }


        //Method Update
        [HttpPut]
        public async Task<ActionResult> PutAsync(Activity activity)
        {
            _context.Update(activity);

            await _context.SaveChangesAsync();
            return Ok(activity);

        }

        //Metod Delete
        [HttpDelete("{Code:int}")]
        public async Task<ActionResult> DeleteAsync(int Code)
        {
            var activity = await _context.ActivityColections.FirstOrDefaultAsync
                  (x => x.Id == Code);

            if (activity == null)
            {
                return NotFound();
            }
            _context.Remove(activity);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [AllowAnonymous]
        [HttpGet("combo")]
        public async Task<ActionResult> GetCombo()
        {
            return Ok(await _context.ActivityColections.ToListAsync());
        }
    }

}