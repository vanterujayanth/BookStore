using LogicLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using modelLayer;

namespace BookStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddresslogic addresslogic;
        public AddressController(IAddresslogic addresslogic)
        {
            this.addresslogic = addresslogic;
        }


        [Authorize(Roles = Role.User)]
        [HttpPost]
        [Route("AddAddress")]

        public IActionResult add_address(AddressModel model)
        {
            var data = addresslogic.AddAddress(model);
            if (data != null)
            {
                return Ok(data);
            }
            return BadRequest();

        }

        [Authorize(Roles = Role.User)]
        [HttpGet]
        [Route("GetAddressByUserId")]

        public IActionResult get_address(int userid)
        {
            var data = addresslogic.GetAddresses(userid);
            if (data != null)
            {
                return Ok(new { success = true, Message = "Successfull", Data = data });
            }
            return BadRequest();
        }

        [Authorize(Roles = Role.User)]
        [HttpPut]
        [Route("UpdateAddress")]

        public IActionResult update_address(UpdateAddressModel model)
        {
            var data = addresslogic.UpdateAddress(model);
            if (data != null)
            {
                return Ok(data);
            }
            return BadRequest();
        }

    }
}
