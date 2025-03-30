using BlogPost.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace BlogPost.Controllers
{
    public class BlogPostController : Controller
    {
        private readonly BlogPostContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BlogPostController(BlogPostContext context, IHttpContextAccessor httpContextAccessor)

        {
            this._context = context;
           this._httpContextAccessor = httpContextAccessor;
        }
        public IActionResult AllPosts()
        {
            var Allposts = _context.Blogs.ToList();
            return View(Allposts);
        }

        public IActionResult CreatePost()
        {
            if (_httpContextAccessor.HttpContext?.Session.GetString("UserId") != null)
            {
                return View();
            }
            else
            {
                return View("~/Views/User/LoginPage.cshtml");

            }
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
