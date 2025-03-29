using BlogPost.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogPost.Controllers
{
    public class UserController : Controller
    {
        private readonly BlogPostContext _context;
        public UserController(BlogPostContext context)
        {
            this._context = context;
        }

       
        public ActionResult Login() {

            return View("LoginPage");
        }

        [HttpPost]
        public ActionResult LoginUp(User userFromLoginPage)
        {

           var user = _context.Users.FirstOrDefault(u => u.UserName == userFromLoginPage.UserName && u.Password == userFromLoginPage.Password);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                TempData["ErrorMessage"] = "No user found with the given credentials.";
                return View("ErrorPage");
            }
           


            HttpContext.Session.SetInt32("UserId", user.Id);

            return RedirectToAction("AllPosts", "BlogPost");
        }

        public ActionResult Signin()
        {

            return View("SignupPage");
        }


        [HttpPost]
        public ActionResult SignUp(User userfrompage)
        {
            var exitingUser = _context.Users.FirstOrDefault(u => u.UserName == userfrompage.UserName);
            if (exitingUser != null)
            {
                ModelState.AddModelError("", "Username already exists.");
                return RedirectToAction("Signup");
              
            }

            User user = new User
            {
                UserName = userfrompage.UserName,
                Password = userfrompage.Password,
                CreatedAt = DateTime.Now,
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Login");
            
        }

        public ActionResult Logout() {
            HttpContext.Session.Clear();
            return RedirectToAction("AllPosts", "BlogPost");
        }
    }
}
