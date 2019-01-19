using System.Threading.Tasks;

namespace OnlineBanking.BLL.Services.Abstract
{
    public interface IEmailSendingService
    {
        Task<bool> SendAsync(string email, string subject, string message);
    }
}
