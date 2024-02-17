using ELearningWeb.DTO;
using ELearningWeb.Helper;

namespace ELearningWeb.IRepository
{
    public interface IRegistrationService
    {

       public Task<MessageHelper> Registration(SignUpDTO obj);
        public Task<MessageHelper> OtpVerify(OtpVerifyDTO obj);
    }
}
