using ELearningWeb.Helper;

namespace ELearningWeb.IRepository
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailRequest);

    }
}
