using LanchesDev.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanchesDev.Controllers
{

    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signManager)
        {
            _userManager = userManager;
            _signInManager = signManager;
        }

        [HttpGet]
        public IActionResult Login (string returnUrl)
        {
            return View(new LoginViewModel()
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
                return View(loginVM);

            var user = await _userManager.FindByNameAsync(loginVM.UserName);

            if(user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);

                if(result.Succeeded)
                {
                    if(string.IsNullOrEmpty(loginVM.ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return RedirectToAction(loginVM.ReturnUrl);
                }
            }

            ModelState.AddModelError("", "Usuário/Senha inválidos ou não localizados");
            return View(loginVM);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(LoginViewModel registroVM)
        {
            if(ModelState.IsValid)
            {
                var user = new IdentityUser() { UserName = registroVM.UserName };

                var result = await _userManager.CreateAsync(user, registroVM.Password);

                if(result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(registroVM);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
