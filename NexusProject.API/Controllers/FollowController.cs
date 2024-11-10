using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexusProject.API.Data;
using NexusProject.API.Helpers;
using NexusProject.Shared.DTOs;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NexusProject.Shared.Entities;

namespace NexusProject.API.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/api/Follows")]
    public class FollowController : ControllerBase
    {

        private readonly DataContext _context;
        private readonly IFileStorage _fileStorage;
        private readonly string _container;

        //Constructor
        public FollowController(DataContext context, IFileStorage fileStorage)
        {
            _context = context;
            _fileStorage = fileStorage;

        }


        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Follows
             .AsQueryable();
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Remarks.ToLower().Contains(pagination.Filter.ToLower()));
            }
            return Ok(await queryable
            .OrderBy(x => x.Id)
            .Paginate(pagination)
            .ToListAsync());
        }

        [HttpGet("totalPages")]
        public async Task<ActionResult> GetPages([FromQuery] PaginationDTO pagination)

        {
            var queryable = _context.Follows.AsQueryable();
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Remarks.ToLower().Contains(pagination.Filter.ToLower()));
            }
            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return Ok(totalPages);
        }


        [HttpPost("CreateActivity")]
        public async Task<ActionResult> CreateActivity([FromBody] Follow model)
        {

            Follow follow = model;            

            _context.Add(follow);
            await _context.SaveChangesAsync();

            return Ok(follow);

        }


        //Method Create
        [HttpPost]
        public async Task<ActionResult<Follow>> PostAsync(Follow follow)
        {
            _context.Add(follow);
            await _context.SaveChangesAsync();


            return Ok(follow);
        }

        //Method Get by ID (Read)
        [HttpGet("{Code:int}")]
        public async Task<ActionResult> GetAsync(int Code)
        {
            var follow = await _context.Follows.FirstOrDefaultAsync
                (x => x.Id == Code);

            if (follow == null)
            {
                return NotFound();
            }
            return Ok(follow);
        }


        //Method Update
        [HttpPut]
        public async Task<ActionResult> PutAsync(Follow follow)
        {
            _context.Update(follow);

            await _context.SaveChangesAsync();
            return Ok(follow);

        }

        //Metod Delete
        [HttpDelete("{Code:int}")]
        public async Task<ActionResult> DeleteAsync(int Code)
        {
            var follow = await _context.Follows.FirstOrDefaultAsync
                  (x => x.Id == Code);

            if (follow == null)
            {
                return NotFound();
            }
            _context.Remove(follow);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [AllowAnonymous]
        [HttpGet("combo")]
        public async Task<ActionResult> GetCombo()
        {
            return Ok(await _context.Follows.ToListAsync());
        }
    }

}