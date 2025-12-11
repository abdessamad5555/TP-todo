using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TextTemplating;
using System.Text.Json;
using Todooo.Models;
using Todooo.Services;
using Todooo.ViewModel;
using Microsoft.Extensions.Logging;

namespace Todooo.Controllers
{
    

    public class UserController : Controller
    {
        
        private readonly ILogger<UserController> _logger;
        private readonly IAuthService _auth;

        
        public UserController(ILogger<UserController> logger, IAuthService auth)
        {
            _logger = logger;
            _auth = auth;
        }


        public IActionResult Index()
        {
            // 3. Appelez le logger directement pour une journalisation spécifique à la logique métier.
            _logger.LogInformation("L'utilisateur est sur la page d'accueil.");

            
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Uservm vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            if (!_auth.Login(vm.email, vm.password))
            {
                _logger.LogWarning("Login échoué pour : " + vm.email);
                ViewBag.error = "Email ou mot de passe incorrect";
                return View(vm);
            }

            
            HttpContext.Session.SetString("user", vm.email);

            return RedirectToAction("Add", "Todo");
        }
        public IActionResult Logout()
        {
            string email = HttpContext.Session.GetString("user");
            if (!string.IsNullOrEmpty(email))
            {
                _logger.LogInformation($"Utilisateur déconnecté : {email}");
            }

            HttpContext.Session.Remove("user");
            return RedirectToAction("Login"); 
        }


    }
}