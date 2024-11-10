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
    [Route("/api/Messages")]
    public class MessageController : ControllerBase
    {

        private readonly DataContext _context;

        public MessageController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("conversation/{userId}/{contactId}")]
        public async Task<IActionResult> GetConversation(string userId, string contactId)
        {
            var messages = await _context.Messages
                .Where(m => (m.SenderId == userId && m.ReceiverId == contactId) ||
                            (m.SenderId == contactId && m.ReceiverId == userId))
                .OrderBy(m => m.DateandTime)
                .ToListAsync();

            return Ok(messages);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Messages
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
            var queryable = _context.Messages.AsQueryable();
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
        public async Task<ActionResult> PostAsync(Message message)
        {
            _context.Add(message);

            await _context.SaveChangesAsync();
            return Ok(message);


        }

        

       
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            var message = await _context.Messages.FirstOrDefaultAsync
                (x => x.Id == id);

            if (message == null)
            {
                return NotFound();
            }
            return Ok(message);
        }

        //Method Update
        [HttpPut]
        public async Task<ActionResult> PutAsync(Message message)
        {
            _context.Update(message);

            await _context.SaveChangesAsync();
            return Ok(message);

        }

        //Metod Delete
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var message = await _context.Messages.FirstOrDefaultAsync
                  (x => x.Id == id);

            if (message == null)
            {
                return NotFound();
            }
            _context.Remove(message);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [AllowAnonymous]
        [HttpGet("combo")]
        public async Task<ActionResult> GetCombo()
        {
            return Ok(await _context.Messages.ToListAsync());
        }

    }
}
