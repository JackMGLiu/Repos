using Microsoft.AspNetCore.Mvc;
using UEditorNetCore;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")] //≈‰÷√¬∑”…
    public class UEditorController : Controller
    {
        private UEditorService ue;
        public UEditorController(UEditorService ue)
        {
            this.ue = ue;
        }

        public void Do()
        {
            ue.DoAction(HttpContext);
        }
    }
}