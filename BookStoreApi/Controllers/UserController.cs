using LogicLayer.Interfaces;
using LogicLayer.Services;
//using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using modelLayer;
using RepostoryLayer.Entity;
using RepostoryLayer.Services;
using System.Reflection;

namespace BookStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // private readonly UserRepo _userRepo;
        private readonly IUserlogic userlogic;
        //private readonly IBus bus;
        public UserController(IUserlogic userlogic)//, IBus bus)
        {
            this.userlogic = userlogic;
           // this.bus = bus;

        }
        [HttpPost]
        public IActionResult Register(UserModel user)
        {
            var result = userlogic.RegisterUser(user);
            //  var result = _userRepo.RegisterUser(user);
            if (result == null)
            {
                return BadRequest(new Responsemodel<UserModel> { IsSuccess = false, Message = "Registration failled" });

            }
            return Ok(new Responsemodel<UserModel> { IsSuccess = true, Message = "Registration Successful", Data = result });

        }


        //login
        [HttpPost("login")]
        public IActionResult Login(LoginModel login)
        {
            var logindata = userlogic.Login(login);
            if (logindata != null)
            {
                return Ok(new Responsemodel<LoginToken> { IsSuccess = true, Message = "Login Successful", Data = logindata });
                // return Ok(logindata);
            }
            return BadRequest(new Responsemodel<LoginToken> { IsSuccess = false, Message = "Login failled" });

        }


        // forgot
        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> Forgot_PasswordAsync(string email)
        {
            var password = userlogic.ForgotPassword(email);

            if (password != null)
            {
                Send send = new Send();
                ForgotPasswordModel forgotPasswordModel = userlogic.ForgotPassword(email);
                send.SendMail(forgotPasswordModel.EmailId, forgotPasswordModel.token);
                Uri uri = new Uri("rabbitmq:://localhost/FunDooNotesEmailQueue");
              //  var endPoint = await bus.GetSendEndpoint(uri);
              //  await endPoint.Send(forgotPasswordModel);
                return Ok(new Responsemodel<string> { IsSuccess = true, Message = "Mail sent Successfully", Data = password.token });
            }
            else
            {
                // to Handle the case where password is null
                return NotFound(new Responsemodel<string> { IsSuccess = false, Message = "Email Does not Exist" });
            }
            //    var password = userlogic.ForgotPassword(email);
            //    if (password != null)
            //    {
            //        Send send = new Send();
            //        //Sendmessage send = new Sendmessage();

            //        send.SendingMail(password.EmailId, "Password is Trying to Changed is that you....!\nToken: " + password.token);

            //        Uri uri = new Uri("rabbitmq://localhost/NotesEmail_Queue");
            //        var endPoint = await bus.GetSendEndpoint(uri);
            //        await endPoint.Send(password);


            //        return Ok("User Found" + "\n Token:" + password.token);

            //    }
            //    return BadRequest("User Not Found");
            //}
        }


       // [Authorize]
        [HttpPost]
        [Route("ResetPassword")]
        public IActionResult reset_password(string password)
        {
            var userid = User.Claims.Where(x => x.Type == "Email").FirstOrDefault().Value;

            var data = userlogic.ResetPassword(userid, password);
            if (data != null)
            {
                return Ok(new Responsemodel<bool> { IsSuccess = true ,Data=data,Message="password changed sucesfully"}) ;//"Password Changed Sucessfully"
            }
            return BadRequest("Invalid Credentials");

        }
    }
}
