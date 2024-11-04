using NexusProject.Shared.Responses;

namespace NexusProject.API.Helpers
{
    public interface IMailHelper
    {
        Response SendMail(string toName, string toEmail, string subject, string body);
    }
}
