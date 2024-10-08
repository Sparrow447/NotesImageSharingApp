using Microsoft.AspNetCore.Mvc;
using NotesImageSharingApp.Data;
using NotesImageSharingApp.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NotesImageSharingApp.Controllers
{
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostViewModel model)
        {
            if (ModelState.IsValid)
            {
                var post = new Post
                {
                    Note = model.Note,
                    CreatedDate = DateTime.Now
                };

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
                    CreatedDate = DateTime.Now
                };

                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new { id = model.PostId });
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
            return NotFound();
            }
            var model = new PostViewModel
            {
            Note = post.Note,
            ImageUrl = post.ImageUrl
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, PostViewModel model)
        {
            if (id != model.Id)
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