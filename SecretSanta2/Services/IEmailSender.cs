using System.Threading.Tasks;

namespace SecretSanta2.Services
{
    public interface IEmailSender
    {
        Task SendAsync(string toEmail, string subject, string body);
    }
}