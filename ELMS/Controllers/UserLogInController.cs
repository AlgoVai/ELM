using ELearningWeb.DbContexts;
using ELearningWeb.DTO;
using ELearningWeb.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ELearningWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLogInController : ControllerBase
    {

        private readonly IUserLogInService _iRepository;
        public UserLogInController(IUserLogInService Irepository)
        {
            _iRepository = Irepository;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("LogIn")]
       public async Task<IActionResult> LogIn([FromBody] LogInDTO obj)
        {
            return Ok(JsonConvert.SerializeObject(await _iRepository.LogIn(obj.Email,obj.Password)));
        }
    }
}
