//using LogicLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using modelLayer;
using RepostoryLayer.Entity;
using RepostoryLayer.Interfaces;

namespace BookStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        //private readonly IWishlistlogic wishlistlogic;
        private readonly IwishlistRepo iwishlistRepo;
        public WishlistController(IwishlistRepo iwishlistRepo)  //(IWishlistlogic wishlistlogic)
        {
            this.iwishlistRepo = iwishlistRepo;
          //  this.wishlistlogic = wishlistlogic;
        }

         [Authorize(Roles = Role.User)]
        [HttpGet]
        [Route("GetWhishList")]

        public IActionResult get_wishlist(int userid)
        {
            var data = iwishlistRepo.GetWhishListBooks(userid);

            if (data == null)
            {
                return BadRequest();
            }

            return Ok(new { IsSuccess = true, Message = "wishlist items are", Data = data });
        }

         [Authorize(Roles = Role.User)]
        [HttpPost]
        [Route("AddToWishList")]

        public IActionResult Add_whishlist(AddWhishlist model)
        {
            var data = iwishlistRepo.AddToWishList(model);

            if (data == null)
            {
                return BadRequest();
            }
            return Ok(new Responsemodel<List<BookEntity>> { IsSuccess = true, Message = "Added Succesfully", Data = data });
        }

         [Authorize(Roles = Role.User)]
        [HttpPost]
        [Route("DeleteWhishList")]

        public IActionResult delete_whishlist(DeleteCartModel model)
        {
            var data = iwishlistRepo.DeleteWhishlist(model);

            if (data == null)
            {
                return BadRequest(new Responsemodel<bool> { IsSuccess = false, Message = "Someting error" });
            }

            return Ok(new Responsemodel<bool> { IsSuccess = true, Message = "deleted sucessfully", Data = true });
        }
    }
}
