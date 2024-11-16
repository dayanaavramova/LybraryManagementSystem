using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class BookController : BaseController
    {
        public IActionResult All()
        {
            return View();
        }
    }
}
