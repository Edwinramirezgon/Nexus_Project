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
        private readonly IFileStorage _fileStorage;

        // Constructor
        public MessageController(DataContext context, IFileStorage fileStorage)
        {
            _context = context;
            _fileStorage = fileStorage;
        }

        // Método para obtener mensajes con filtro por ChatId y fecha opcional
        [HttpGet]
        public async Task<IActionResult> GetMessages(
            [FromQuery] string? chatId,
            [FromQuery] DateTime? afterDate,
            [FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Messages.AsQueryable();

            // Filtrar por ChatId si se proporciona
            if (!string.IsNullOrWhiteSpace(chatId))
            {
                queryable = queryable.Where(m => m.ChatId == chatId);
            }

            // Filtrar por fecha si se proporciona
            if (afterDate.HasValue)
            {
                queryable = queryable.Where(m => m.DateandTime > afterDate.Value);
            }

            // Paginación
            var paginatedMessages = await queryable
                .OrderBy(m => m.DateandTime)
                .Paginate(pagination)
                .ToListAsync();

            return Ok(paginatedMessages);
        }

        [HttpGet("totalPages")]
        public async Task<ActionResult> GetPages([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Messages.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.MessageId.ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return Ok(totalPages);
        }

        // Método para crear un mensaje
        [HttpPost]
        public async Task<ActionResult<Message>> PostAsync(Message message)
        {
            _context.Add(message);
            await _context.SaveChangesAsync();
            return Ok(message);
        }

        // Método para obtener un mensaje por ID
        [HttpGet("{Code:int}")]
        public async Task<ActionResult> GetAsync(string Code)
        {
            var message = await _context.Messages.FirstOrDefaultAsync(x => x.MessageId == Code);

            if (message == null)
            {
                return NotFound();
            }
            return Ok(message);
        }

        // Método para actualizar un mensaje
        [HttpPut]
        public async Task<ActionResult> PutAsync(Message message)
        {
            _context.Update(message);
            await _context.SaveChangesAsync();
            return Ok(message);
        }

        // Método para eliminar un mensaje por ID
        [HttpDelete("{Code:int}")]
        public async Task<ActionResult> DeleteAsync(string Code)
        {
            var message = await _context.Messages.FirstOrDefaultAsync(x => x.MessageId == Code);

            if (message == null)
            {
                return NotFound();
            }
            _context.Remove(message);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Método para obtener una lista de mensajes (sin paginación ni filtros) para un combo
        [AllowAnonymous]
        [HttpGet("combo")]
        public async Task<ActionResult> GetCombo()
        {
            return Ok(await _context.Messages.ToListAsync());
        }
    }
}
