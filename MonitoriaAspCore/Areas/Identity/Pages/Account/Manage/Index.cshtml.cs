using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MonitoriaAspCore.Models;

namespace MonitoriaAspCore.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IHostingEnvironment _environment;

       
        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            IHostingEnvironment IHostingEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _environment = IHostingEnvironment;
        }

        public string Username { get; set; }

        //public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {   
            [Required]
            [Display(Name="CPF")]
            public string Cpf { get; set; }

            [Required]
            [Display(Name = "Login")]
            public string UserName { get; set; }

            [Required]
            [Display(Name = "Nome")]
            public string Name { get; set; }

            [Required]
            [Display(Name = "Gênero")]
            public string Gender { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            public string Theme { get; set; }

            
            public string UrlImage { get; set; }
          

            /*[Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }*/
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var userName = await _userManager.GetUserNameAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                Cpf = user.Cpf,
                UserName = user.UserName,
                Name = user.Name,
                Gender = user.Gender,
                Email = user.Email,
                Theme = user.Theme,
                UrlImage = user.UrlImage,
                //PhoneNumber = phoneNumber
            };

            //IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            //   atualiza imagem
            var newFileName = string.Empty;
            if (HttpContext.Request.Form.Files != null)
            {
                var fileName = string.Empty;
                string PathDB = string.Empty;

                var files = HttpContext.Request.Form.Files;

                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        //Getting FileName
                        fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                        //Assigning Unique Filename (Guid)
                        var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                        //Getting file Extension
                        var FileExtension = Path.GetExtension(fileName);

                        // concating  FileName + FileExtension
                        newFileName = myUniqueFileName + FileExtension;

                        // Combines two strings into a path.
                        fileName = Path.Combine(_environment.WebRootPath, "images\\user") + $@"\{newFileName}";

                        // if you want to store path of folder in database
                        PathDB = "user/" + newFileName;

                        using (FileStream fs = System.IO.File.Create(fileName))
                        {
                            file.CopyTo(fs);
                            fs.Flush();


                            if(user.UrlImage != "user.jpg")
                            {
                                var imgDelete = Path.Combine(_environment.WebRootPath, "images\\user") + $@"\{user.UrlImage}";
                                if (System.IO.File.Exists(imgDelete))
                                {
                                    System.IO.File.Delete(imgDelete);
                                }
                            }
                            user.UrlImage = newFileName;
                        }
                    }
                }
            }




            user.Cpf = Input.Cpf;
            user.UserName = Input.UserName;
            user.Name = Input.Name;
            user.Gender = Input.Gender;
            user.Email = Input.Email;
            user.Theme = Input.Theme;

            var result = await _userManager.UpdateAsync(user);
            

            var email = await _userManager.GetEmailAsync(user);
            if (Input.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, Input.Email);
                if (!setEmailResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting email for user with ID '{userId}'.");
                }
            }

            /*var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }*/

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }


            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = userId, code = code },
                protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(
                email,
                "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            StatusMessage = "Verification email sent. Please check your email.";
            return RedirectToPage();
        }
    }
}
