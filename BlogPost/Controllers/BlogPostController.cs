using BlogPost.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogPost.Controllers
{
    public class BlogPostController : Controller
    {
        private readonly BlogPostContext _context;

        public BlogPostController(BlogPostContext context)
        {
            this._context = context;
        }
        public IActionResult AllPosts()
        {
            var Allposts = _context.Blogs.ToList();
            return View(Allposts);
        }

        public IActionResult CreatePost()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitPost(Blog blog)
        {
            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AllPosts));
        }


        public async Task<IActionResult> EditPost(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> UpdatePost(int id, Blog blog)
        {
            if (id != blog.Id)
            {
                return NotFound();
            }
            _context.Update(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AllPosts));
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult DeletePost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = _context.Blogs.FirstOrDefault(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            _context.Blogs.Remove(blog);
            _context.SaveChanges(); 
            return RedirectToAction(nameof(AllPosts));
        }
    }
}
