using Microsoft.AspNetCore.Mvc;
using NotesImageSharingApp.Models; // Ensure this is present
using NotesImageSharingApp.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesImageSharingApp.Controllers
{
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Konstruktør som tar inn ApplicationDbContext
        public PostController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Posts/Index
        public IActionResult Index()
        {
            var posts = _context.Posts.ToList();  // Eksempel på hvordan du kan bruke _context
            return View(posts);
        }

    [HttpPost]
    public async Task<IActionResult> Create(PostViewModel model)
    {
        if (ModelState.IsValid)
        {
            var post = new Post
            {
                Content = model.Content,  // Changed from Note to Content
                CreatedDate = DateTime.Now
            };

            // Handle image upload
            if (model.Image != null)
            {
                var fileName = Path.GetFileName(model.Image.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Image.CopyToAsync(stream);
                }
            }

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        return View(model);
    }

        [HttpPost]
        public async Task<IActionResult> AddComment(CommentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var comment = new Comment
                {
                    PostId = model.PostId,
                    Content = model.Content,
                    Author = model.Author,
                    DateCreated = DateTime.Now
                };

                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new { id = model.PostId });
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(
            int id, PostViewModel model)
        {
            if (id != model.PostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var post = await _context.Posts.FindAsync(id);
                if (post == null)
                {
                    return NotFound();
                }

                post.Content = model.Content;

                // Handle image upload
                if (model.Image != null)
                {
                    var fileName = Path.GetFileName(model.Image.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.Image.CopyToAsync(stream);
                    }
                }

                _context.Posts.Update(post);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, PostViewModel model)
        {
            if (id != model.PostId)
            {
            return NotFound();
            }

            if (ModelState.IsValid)
            {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            post.Note = model.Note;

            if (model.Image != null)
            {
                var fileName = Path.GetFileName(model.Image.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                await model.Image.CopyToAsync(stream);
                }
                post.ImageUrl = "/images/" + fileName;
            }

            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
            return NotFound();
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}