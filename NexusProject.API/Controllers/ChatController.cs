using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexusProject.API.Data;
using NexusProject.Shared.DTOs;
using NexusProject.Shared.Entities;
using System.Threading.Tasks;
using System;
using System.Linq;
using NexusProject.API.Helpers;
using Microsoft.EntityFrameworkCore;

namespace NexusProject.API.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/api/Chats")]
    public class ChatCOntroller : ControllerBase
    {

        private readonly DataContext _context;
        private readonly IFileStorage _fileStorage;
        private readonly string _container;

        //Constructor
        public ChatCOntroller(DataContext context, IFileStorage fileStorage)
        {
            _context = context;
            _fileStorage = fileStorage;

        }


        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Chats
             .AsQueryable();
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.ChatId.ToLower().Contains(pagination.Filter.ToLower()));
            }
            return Ok(await queryable
            .OrderBy(x => x.ChatId)
            .Paginate(pagination)
            .ToListAsync());
        }

        [HttpGet("totalPages")]
        public async Task<ActionResult> GetPages([FromQuery] PaginationDTO pagination)

        {
            var queryable = _context.Chats.AsQueryable();
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.ChatId.ToLower().Contains(pagination.Filter.ToLower()));
            }
            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return Ok(totalPages);
        }


        [HttpPost("CreateActivity")]
        public async Task<ActionResult> CreateActivity([FromBody] Chat model)
        {

            Chat chat = model;

            _context.Add(chat);
            await _context.SaveChangesAsync();

            return Ok(chat);

        }


        //Method Create
        [HttpPost]
        public async Task<ActionResult<Chat>> PostAsync(Chat chat)
        {
            _context.Add(chat);
            await _context.SaveChangesAsync();


            return Ok(chat);
        }

        //Method Get by ID (Read)
        [HttpGet("{Code:int}")]
        public async Task<ActionResult> GetAsync(string Code)
        {
            var chat = await _context.Chats.FirstOrDefaultAsync
                (x => x.ChatId == Code);

            if (chat == null)
            {
                return NotFound();
            }
            return Ok(chat);
        }


        //Method Update
        [HttpPut]
        public async Task<ActionResult> PutAsync(Chat chat)
        {
            _context.Update(chat);

            await _context.SaveChangesAsync();
            return Ok(chat);

        }

        //Metod Delete
        [HttpDelete("{Code:int}")]
        public async Task<ActionResult> DeleteAsync(string Code)
        {
            var chat = await _context.Chats.FirstOrDefaultAsync
                  (x => x.ChatId == Code);

            if (chat == null)
            {
                return NotFound();
            }
            _context.Remove(chat);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [AllowAnonymous]
        [HttpGet("combo")]
        public async Task<ActionResult> GetCombo()
        {
            return Ok(await _context.Chats.ToListAsync());
        }
    }

}