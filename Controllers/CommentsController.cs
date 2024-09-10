using Microsoft.AspNetCore.Mvc;

namespace NotesImageSharingApp.Controllers
{
    public class CommentsController : Controller
    {
        // GET: Comments
        public IActionResult Index()
        {
            return View(); // Ensure the Index view exists
        }
    }
}