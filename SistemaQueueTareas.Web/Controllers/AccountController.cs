using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Mvc;
using SistemaQueueTareas.IdentityData;
using Microsoft.Owin.Security;

namespace SistemaQueueTareas.Web.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

        // GET: /Account/Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var user = UserManager.FindByEmail(email);
            if (user != null && UserManager.CheckPassword(user, password))
            {
                var authManager = HttpContext.GetOwinContext().Authentication;
                var identity = UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authManager.SignIn(new AuthenticationProperties { IsPersistent = false }, identity);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Credenciales inválidas");
            return View();
        }

        // GET: /Account/Register
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string email, string username, string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                ModelState.AddModelError("", "Las contraseñas no coinciden");
                return View();
            }

            var user = new ApplicationUser
            {
                UserName = username,
                Email = email
            };

            var result = UserManager.Create(user, password);
            if (result.Succeeded)
            {
                // Asignar rol 'user' por defecto
                UserManager.AddToRole(user.Id, "user");
                return RedirectToAction("Login");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
            return View();
        }

        // GET: /Account/EditProfile
        [Authorize]
        public ActionResult EditProfile()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            return View(user);
        }

        [HttpPost]
        [Authorize]
        public ActionResult EditProfile(string email, string username)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            user.Email = email;
            user.UserName = username;

            var result = UserManager.Update(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
            return View(user);
        }

        // POST: /Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();
            return RedirectToAction("Login");
        }
    }
}