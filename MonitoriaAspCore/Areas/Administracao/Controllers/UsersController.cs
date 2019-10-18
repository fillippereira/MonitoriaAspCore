using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MonitoriaAspCore.Areas.Administracao.Models;
using MonitoriaAspCore.Models;


namespace MonitoriaAspCore.Areas.Administracao.Controllers
{   [Area("Administracao")]
    public class UsersController : Controller
    {
        
        private readonly UserManager<ApplicationUser> userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        

        public async Task<IActionResult> Index()
        {
            return View(userManager.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).ToList());
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                var NewUser = new ApplicationUser
                {
                    Cpf = user.Cpf,
                    UserName = user.UserName,
                    Name = user.Name,
                    Gender = user.Gender,
                    Email = user.Email,
                    Theme = "Escuro",
                    UrlImage = "user.jpg"
                };

                IdentityResult result = await userManager.CreateAsync(NewUser, "Mc#2019");
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            return View(user);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                var UpdateUser = await userManager.FindByIdAsync(user.Id);

                UpdateUser.Cpf = user.Cpf;
                UpdateUser.UserName = user.UserName;
                UpdateUser.Name = user.Name;
                UpdateUser.Gender = user.Gender;
                UpdateUser.Email = user.Email;
                UpdateUser.Theme = user.Theme;

                IdentityResult result = await userManager.UpdateAsync(UpdateUser);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            return View(user);
        }




        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(ApplicationUser  user)
        {
            var UserDelete = await userManager.FindByIdAsync(user.Id);
            if (UserDelete != null)
            {
                IdentityResult result = await userManager.DeleteAsync(UserDelete);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            else
                ModelState.AddModelError("", "User Not Found");
            return View("Index", userManager.Users);
        }

    }
}