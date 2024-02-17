using ELearningWeb.DTO;
using ELearningWeb.Helper;
using ELearningWeb.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ELearningWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {

        private IRegistrationService _iRepository;
        public RegistrationController(IRegistrationService irepository)
        {
            _iRepository = irepository;
        }
        [HttpPost]
        [Route("Registration")]
        public async Task<IActionResult> Registration(SignUpDTO obj)
        {
            return Ok(JsonConvert.SerializeObject(await _iRepository.Registration(obj)));
        }
        [HttpPut]
        [Route("OtpVerification")]
        public async Task<IActionResult> OtpVerify(OtpVerifyDTO obj)
        {
            return Ok(JsonConvert.SerializeObject(await _iRepository.OtpVerify(obj)));
        }
    }
}
