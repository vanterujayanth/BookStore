using LogicLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using modelLayer;
using RepostoryLayer.Entity;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderlogic orderlogic;
        public OrderController(IOrderlogic orderlogic)
        {
            this.orderlogic = orderlogic;
        }

        [Authorize(Roles = Role.User)]
        [HttpGet]
        [Route("GetAllOrder")]
        public IActionResult Get_all_orders(int userid)
        {
            var data = orderlogic.GetOrders(userid);
            if (data != null)
            {
                return Ok(new { IsSuccess=true,Message="ALL orders are present . ",Data=data});
            }
            return BadRequest();
        }

        [Authorize(Roles = Role.User)]
        [HttpPost]
        [Route("AddOrder")]

        public IActionResult Add_order(orderModel model, int userid)
        {
            var data = orderlogic.AddToOrder(model, userid);
            if (data != null)
            {
                return Ok(new { IsSuccess = true, Message = "order is added. ", Data = data });
            }
            return BadRequest("unable to add the item in To the crt");
        }

        [Authorize(Roles = Role.User)]
        [HttpGet]
        [Route("GetOrderPrice")]

        public IActionResult Get_order_price(int userid)
        {
            var data =orderlogic.GetPriceInOrder(userid);
            if (data != null)
            {
                return Ok(new {IsSuccess = true, Message = "order is added. ", Data = data });
            }
            return BadRequest("data is not found");
        }

        [Authorize(Roles = Role.User)]
        [HttpGet]
        [Route("GetAllOrderss")]
        public IActionResult Get_all_orderss(int userid)
        {
            var data = orderlogic.GetallOrders(userid);
            if (data != null)
            {
                return Ok(new { IsSuccess = true, Message = "ALL orders are present . ", Data = data });
            }
            return BadRequest();
        }
    }
}
