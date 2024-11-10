using NexusProject.Shared.Entities;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace NexusProject.Web.Repositories
{
    public interface IChatService
    {
      Task<List<User>> GetContactsAsync();
       Task<List<Message>> GetConversationAsync(string userId, string contactId);

     Task SendMessageAsync(Message message);
    }
}
