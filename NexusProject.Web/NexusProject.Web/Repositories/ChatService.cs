using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using NexusProject.Shared.Entities;
using NexusProject.Web.Repositories;

public class ChatService : IChatService
{
    private readonly HttpClient _httpClient;

    public ChatService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    
    public async Task<List<User>> GetContactsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<User>>("api/Users/combo");
    }

 
    public async Task<List<Message>> GetConversationAsync(string userId, string contactId)
    {
        return await _httpClient.GetFromJsonAsync<List<Message>>($"api/Messages/conversation/{userId}/{contactId}");
    }

    public async Task SendMessageAsync(Message message)
    {
        await _httpClient.PostAsJsonAsync("api/Messages", message);
    }
}