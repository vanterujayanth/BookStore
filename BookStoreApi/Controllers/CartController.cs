using LogicLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using modelLayer;

namespace BookStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartlogic cartlogic;
        public CartController(ICartlogic cartlogic)
        {
            this.cartlogic = cartlogic;
        }
        [Authorize(Roles = Role.User)]
        [HttpPost]
        [Route("AddToCart")]
        public IActionResult AddCart(int userid, CartModel model)
        {
            var data = cartlogic.AddToCart(model, userid);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }


        [Authorize(Roles = Role.User)]
        [HttpGet]
        [Route("GetCard")]
        public IActionResult GetCard(int userid)
        {
            var data = cartlogic.GetCartBooks(userid);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        


        [Authorize(Roles = Role.User)]
        [HttpPut]
        [Route("UpdateQuantity")]
        public IActionResult UpdateQty(int userid, CartModel model)
        {
            var data = cartlogic.UpdateQuantity(userid, model);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }


        [Authorize(Roles = Role.User)]
        [HttpPost]
        [Route("DeleteCart")]

        public IActionResult Delete_cart(DeleteCartModel model)
        {
            var data = cartlogic.DeleteCart(model);
            if (!data)
            {
                return NotFound("Cart Not found");
            }
            return Ok(new { message = "deleted sucessfully", result = true });
        }

    }
}




//[HttpGet]
//[Route("GetCardPrice")]
//public IActionResult GetCardPrice(int userid)
//{
//    var data = business.GetPrice(userid);
//    if (data == null)
//    {
//        return NotFound();
//    }
//    return Ok(data);
//}