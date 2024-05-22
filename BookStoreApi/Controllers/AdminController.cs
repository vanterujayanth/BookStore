using LogicLayer.Interfaces;
using LogicLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using modelLayer;

namespace BookStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminlogic adminlogic;
        public AdminController(IAdminlogic adminlogic)
        {
            this.adminlogic = adminlogic;
        }
        [HttpPost("login")]
        public IActionResult Login(LoginModel login)
        {
            var logindata = adminlogic.Login(login);
            if (logindata != null)
            {
                return Ok(new Responsemodel<LoginToken> { IsSuccess = true, Message = "Login Successful", Data = logindata });
                // return Ok(logindata);
            }
            return BadRequest(new Responsemodel<LoginToken> { IsSuccess = false, Message = "Login failled" });

        }
    }
}
