using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProiectMedii.Migrations.LibraryIdentity;
using ProiectMedii.Models;

namespace ProiectMedii.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IEmailSender _emailSender;
        private readonly IUserEmailStore<IdentityUser> _emailStore;
        private readonly ProiectMedii.Data.ProiectMediiContext _context;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            ProiectMedii.Data.ProiectMediiContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
        }
        [BindProperty]
        public Client Client { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(50, MinimumLength = 6, ErrorMessage = "Lungimea trebuie sa fie cuprinsă între 6 și 50 de caractere.")]
            [RegularExpression(@"^[A-Z][a-z]{2,}\s[A-Z][a-z]{2,}$", ErrorMessage = "Numele trebuie sa fie de forma: Prenume Nume")]
            [Display(Name = "Name")]
            public string Name { get; set; }

            [Display(Name = "Telefon")]
            [StringLength(10, ErrorMessage = "Format: 07xxxxxxxx")]
            [RegularExpression(@"^07\d{8}", ErrorMessage = "Format: 07xxxxxxxx")]
            public string ContactMobile { get; set; }

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

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
            var result = await _userManager.CreateAsync(user, Input.Password);

            Client.ContactMail = Input.Email;
            Client.ClientName = Input.Name;
            Client.ContactMobile = Input.ContactMobile;
            _context.Client.Add(Client);
            await _context.SaveChangesAsync();

            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");
                var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmail",
                pageHandler: null,
                values: new
                {
                    area = "Identity",
                    userId = userId,
                    code = code,
                    returnUrl = returnUrl
                },
                protocol: Request.Scheme);
                await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                $"Please confirm your account by <a href = '{HtmlEncoder.Default.Encode(callbackUrl)}' > clicking here </ a >.");
                if (_userManager.Options.SignIn.RequireConfirmedAccount)
                {
                    return RedirectToPage("RegisterConfirmation", new
                    {
                        email = Input.Email,
                        returnUrl = returnUrl
                    });
                }
                else
                {
                    await _signInManager.SignInAsync(user,
                    isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty,
                    error.Description);
                }
            }
            return Page();
        }
    }
}

