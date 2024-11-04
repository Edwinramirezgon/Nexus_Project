using System.Threading.Tasks;

namespace NexusProject.Web.Auth
{
    public interface ILoginService
    {
        Task LoginAsync(string token);
        Task LogoutAsync();

    }
}
