using Microsoft.AspNetCore.Mvc;
using NetCoreService;

namespace WebApplication1.Controllers
{
    public class TestController : Controller
    {
        private ITestService _testService;

        public TestController(ITestService testService)
        {
            _testService = testService;
        }

        public IActionResult Demo()
        {
            var count = _testService.GetUsersCount();
            return Content(count.ToString());
        }
    }
}