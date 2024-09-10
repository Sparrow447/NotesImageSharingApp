using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesImageSharingApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesImageSharingApp.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Comments/Grid
        public IActionResult Grid()
        {
            List<Comment> comments = _context.Comments.ToList();
            return View(comments);
        }

        // GET: Comments/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var comment = await _context.Comments
                .FirstOrDefaultAsync(c => c.CommentId == id);

            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // GET: Comments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Comments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommentId,Content,DateCreated,UserId,NoteId,ImageId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.DateCreated = DateTime.Now; // Set the creation date
                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Grid));
            }
            return View(comment);
        }
    }
}
