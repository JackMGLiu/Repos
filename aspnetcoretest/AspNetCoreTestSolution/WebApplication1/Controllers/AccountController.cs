using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("account/userlogin")]
        public IActionResult UserLogin()
        {
            return Json("");
        }
    }
}