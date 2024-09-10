using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesImageSharingApp.Data;
using NotesImageSharingApp.Models;

namespace NotesImageSharingApp.Controllers;

public class NotesController : Controller
{
    private readonly ApplicationDbContext _context;

    // Constructor
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
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var note = await _context.Notes
            .FirstOrDefaultAsync(m => m.NoteId == id);
        if (note == null)
        {
            return NotFound();
        }

        return View(note);
    }

    // GET: Notes/Create
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // POST: Notes/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("NoteId,Title,Content,UserId,DateCreated")] Note note)
    {
        if (ModelState.IsValid)
        {
            _context.Add(note);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(note);
    }

    // GET: Notes/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var note = await _context.Notes.FindAsync(id);
        if (note == null)
        {
            return NotFound();
        }
        return View(note);
    }

    // POST: Notes/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("NoteId,Title,Content,UserId,DateCreated")] Note note)
    {
        if (id != note.NoteId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(note);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteExists(note.NoteId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(note);
    }

    // GET: Notes/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var note = await _context.Notes
            .FirstOrDefaultAsync(m => m.NoteId == id);
        if (note == null)
        {
            return NotFound();
        }

        return View(note);
    }

    // POST: Notes/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var note = await _context.Notes.FindAsync(id);
        if (note != null)
        {
            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    private bool NoteExists(int id)
    {
        return _context.Notes.Any(e => e.NoteId == id);
    }
}
