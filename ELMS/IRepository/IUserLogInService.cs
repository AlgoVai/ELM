using ELearningWeb.DTO;

namespace ELearningWeb.IRepository
{
    public interface IUserLogInService
    {

        Task<AuthDTO> LogIn(string Email,string Password);


    }
}
