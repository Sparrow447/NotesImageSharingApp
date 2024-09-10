using Microsoft.AspNetCore.Mvc;

namespace NotesImageSharingApp.Controllers
{
    public class ImageController : Controller
    {
        // GET: Images
        public IActionResult Index()
        {
            return View(); // Ensure the Index view exists
        }
    }
}