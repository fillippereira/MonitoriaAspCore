using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MonitoriaAspCore.Models;

namespace MonitoriaAspCore.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public ForgotPasswordModel(UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public AppMessage ModelMessage { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Cpf")]
            public string Cpf { get; set; }
            [Required]
            [Display(Name = "Login")]
            public string UserName { get; set; }


            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(Input.UserName);
                if (user == null)
                {

                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }



                if (user.Cpf == Input.Cpf)
                {
                    var NovaSenha = _userManager.PasswordHasher.HashPassword(user, Input.Password);
                    user.PasswordHash = NovaSenha;
                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        ModelMessage = new AppMessage("Senha alterada com sucesso!", "success"); ;
                        TempData["StatusMessage"] = ModelMessage.Message;
                        return RedirectToPage("./Login");

                    }
                }
                else
                {
                    ModelState.AddModelError("Cpf", "You cannot assign a future date.");
                    ModelMessage = new AppMessage("O Cpf informado não pertence ao usuário!", "warning");
                    TempData["StatusMessage"] = ModelMessage.Message;
                    return Page();
                }

                // For more information on how to enable account confirmation and password reset please 
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                /* var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                 var callbackUrl = Url.Page(
                     "/Account/ResetPassword",
                     pageHandler: null,
                     values: new { code },
                     protocol: Request.Scheme);

                 await _emailSender.SendEmailAsync(
                     Input.Email,
                     "Reset Password",
                     $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                     */
                ModelMessage = new AppMessage("Oops, não foi possível alterar a senha. Verifique os dados e tente novamente", "danger");
                TempData["StatusMessage"] = ModelMessage.Message   ;
                return Page();
            }

            return Page();
        }
    }
}
