using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TestandoIdentity.Models;
using TestandoIdentity.Models.IdentityModels;

namespace TestandoIdentity.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public HomeController(ILogger<HomeController> logger,
                              UserManager<ApplicationUser> userManager,
                              RoleManager<ApplicationRole> roleManager)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> CriarUsuario()
        {
            var regBase = await _userManager.FindByNameAsync("User@User");

            if (regBase != null) return View("Index");

            var user = new ApplicationUser
            {
                Email = "User@User",
                UserName = "User@User"
            };

            var result = await _userManager.CreateAsync(user, "Senha@123");

            await _userManager.AddToRoleAsync(user, "Usuario");

            return Json(result);
        }

        public async Task<IActionResult> DeletarUsuario()
        {
            var regBase = await _userManager.FindByNameAsync("User@User");

            if (regBase == null) return View("Index");

            var result = await _userManager.DeleteAsync(regBase);

            return Json(result);
        }      

        public IActionResult ListarUsuario()
        {
            var result = _userManager.Users.ToList();

            return Json(result);
        }

        public async Task<IActionResult> CriarRole()
        {
            var regBase = await _roleManager.FindByNameAsync("Usuario");

            if (regBase != null) return View("Index");

            var role = new ApplicationRole { Name = "Usuario" };

            var result = await _roleManager.CreateAsync(role);           

            return Json(result);
        }

        public async Task<IActionResult> DeletarRole()
        {
            var regBase = await _userManager.FindByNameAsync("Usuario");

            if (regBase == null) return View("Index");

            var result = await _userManager.DeleteAsync(regBase);

            return Json(result);
        }

        public IActionResult ListarRoles()
        {
            var result = _roleManager.Roles.ToList();

            return Json(result);
        }
    }
}
