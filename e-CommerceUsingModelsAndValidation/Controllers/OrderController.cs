using e_CommerceUsingModelsAndValidation.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_CommerceUsingModelsAndValidation.Controllers
{
    public class OrderController : Controller
    {
        [Route("/order")]
        public IActionResult Index(Order order)
        {
            if(Request.Method != "POST")
            {
                return NotFound("No route found for this request (Are you using the proper HTTP method?)");
            }
            if (!ModelState.IsValid)
            {
                string errors = string.Join("\n", ModelState.Values
                    .SelectMany(value => value.Errors)
                    .Select(error => error.ErrorMessage));
                return BadRequest(errors);
            }
            order.OrderNo = new Random().Next(1, 99999);
            return Json(new { OrderNumber = order.OrderNo });
        }
    }
}
