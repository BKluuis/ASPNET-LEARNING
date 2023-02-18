using BankUsingControllers.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BankUsingControllers.Controllers
{
    public class AccountController : Controller
    {
        //Hard-coded bank account
        Account account = new Account(1001, "Example Name", 5000);

        [Route("/")]
        public IActionResult Home()
        {
            if (Request.Method == "GET")
            {
                return Content("<h1>Welcome to the best bank in the entire world</h1>", "text/html");
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("/account-details")]
        public IActionResult AccountDetails()
        {
            if (Request.Method == "GET")
            {
                return Json(account);
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("/account-statement")]
        public IActionResult AccountStatement()
        {
            if (Request.Method == "GET")
            {
                return File("dummy.pdf", "application/pdf");
            }
            else
            {
                return NotFound();
            }
        }

        [Route("/get-current-balance/{accountNumber:int?}")]
        public IActionResult AccountBalance(int? accountNumber)
        {
            if (Request.Method == "GET")
            {
                if (accountNumber == null)
                {
                    return NotFound("Account Number should be supplied");
                }
                if (accountNumber != account.Number)
                {
                    return BadRequest("Account number should be 1001");
                }

                return Content($"{account.Balance}", "text/plain");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
