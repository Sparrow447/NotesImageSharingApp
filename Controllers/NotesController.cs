using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesImageSharingApp.Models;
using System.Linq;
using System.Threading.Tasks;

namespace NotesImageSharingApp.Controllers
{
    public class NotesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NotesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Notes
        public async Task<IActionResult> Index()
        {
            var notes = await _context.Notes.ToListAsync();
            return View(notes);
        }

        // GET: Notes/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var note = await _context.Notes
                .FirstOrDefaultAsync(m => m.NoteId == id);
            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        // GET: Notes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Notes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NoteId,Title,Content,DateCreated,UserId")] Note note)
        {
            if (ModelState.IsValid)
            {
                note.DateCreated = DateTime.Now; // Set the creation date
                _context.Add(note);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(note);
        }
    }
}