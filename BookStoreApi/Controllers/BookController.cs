using LogicLayer.Interfaces;
using LogicLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using modelLayer;
using RepostoryLayer.Entity;
using static MassTransit.ValidationResultExtensions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBooklogic booklogic;
        public BookController(IBooklogic booklogic)
        {
            this.booklogic = booklogic;
        }




        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        [Route("AddBooks")]
        public IActionResult AddBook(BookModel book)
        {
            var result = booklogic.Add(book);
            //  var result = _userRepo.RegisterUser(user);
            if (result == null)
            {
                return BadRequest(new Responsemodel<BookEntity> { IsSuccess = false, Message = "Registration failled" });

            }
            return Ok(new Responsemodel<BookModel> { IsSuccess = true, Message = "Registration Successful",Data=result });

        }

        [HttpGet]
        [Route("GetAllBooks")]
        public IActionResult Get()
        {
            var data = booklogic.GetAllBooks();
            if (data == null)
            {
                return BadRequest(new Responsemodel<BookEntity> { IsSuccess = false, Message = " failled to fetch notes" });

            }
            return Ok(new  { IsSuccess = true, Message = "Successful fetch notes", Data = data });

        }
        [Authorize (Roles=Role.Admin)]
        [HttpPut("update")]
        public IActionResult UpdateEmployee([FromBody] BookModel model,int noteid)
        {
            if (model == null)
            {
                return BadRequest("Model is null");
            }

            try
            {
                var result = booklogic.EmployeeUpdate(noteid, model);
                if (result != null)
                {
                    return Ok(new { IsSuccess = true, Message = "Successfully updateted the notes" });

                }
                else
                {
                    return StatusCode(500, "A problem happened while handling your request.");
                }
            }
            catch (Exception ex)
            {
                // Log exception here
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("delete")]
        public IActionResult DeleteBook([FromBody] int noteid)
        {

            try
            {
                var result = booklogic.DeleteBook(noteid);
                if (result != null)
                {
                    return Ok(new { IsSuccess = true, Message = "Successfully updateted the notes" });

                }
                else
                {
                    return StatusCode(500, "A problem happened while handling your request.");
                }
            }
            catch (Exception ex)
            {
                // Log exception here
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet]
        [Route("GetById")]
        public IActionResult Get_byid(int id)
        {
            var data = booklogic.GetBookById(id);
            if (data == null)
            {
                return BadRequest(new Responsemodel<BookEntity> { IsSuccess = false, Message = " failled to fetch notes" });

            }
            return Ok(new { IsSuccess = true, Message = "Successful fetch notes", Data = data });

        }

        //[HttpGet]
        //[Route("GetByTitle")]
        //public IActionResult Get_by_title(string Title)
        //{
        //    var data = bookBusiness.GetBookByTitle(Title);
        //    if (data == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(data);
        //}

    }
}
