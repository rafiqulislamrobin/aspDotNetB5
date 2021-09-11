using DataImporter.Models.AccountModel;
using DataImporter.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace DataImporter.Controllers
{
    
    [Area("User")]

    public class AccountController : Controller
    {

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IEmailService _emailService;
        private readonly IRecaptchaService _recaptchaService;

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<AccountController> logger,
            IEmailService emailSender,
            IRecaptchaService recaptchaService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailService = emailSender;
            _recaptchaService = recaptchaService;


        }


        public async Task<IActionResult> Register(string returnUrl = null)
        {
            RegisterModel model = new();
            model.ReturnUrl = returnUrl;
            model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Register", "DataImporter");
            }
            else
            {
               var  captcha= _recaptchaService.ReCaptchaPassed(Request.Form["foo"]);
                if (captcha)
                {
                       var user = new IdentityUser
                       {
                           UserName = model.Email,
                           Email = model.Email
                       };           
                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                        var confirmationLink = Url.Action("ConfirmEmail", "Account",
                            new { userId = user.Id, token = token }, Request.Scheme);


                        if (_signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                        {
                            return RedirectToAction("ListUsers", "Administration");
                        }

                         _emailService.SendEmailAsync(model.Email, "Confirm your email",
                           $"Please confirm your account by" +
                           $" <a href='{HtmlEncoder.Default.Encode(confirmationLink)}'>clicking here</a>.");

                        return View("RegistrationSuccessView");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                }

                else
                {
                    return RedirectToAction("Error");
                }
            }

              

            return View(model);
        }
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token ==null)
            {
                return RedirectToAction("Index", "DataImporter");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"The User ID {userId} is invalid";
                return View("NotFound");
            }
            var result = await _userManager.ConfirmEmailAsync(user,token);
            if (result.Succeeded)
            {
                return View("ConfirmEmailView");
            }
            ViewBag.ErrorTitle = "Email cannot be confirmed";
            return View("RegistrationSuccessView");

        }

        [TempData]
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> Login( string returnUrl = null)
        {
          
            LoginModel model = new();

            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }
        

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            model.ReturnUrl = returnUrl;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            loginModel.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            
          

            
                if (!ModelState.IsValid)
                {
                    return RedirectToAction("error");
                }
                else
                {
                var captcha = _recaptchaService.ReCaptchaPassed(Request.Form["foo"]);


                if (captcha)
                {
                        // This doesn't count login failures towards account lockout
                        // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                        var result = await _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password,
                            loginModel.RememberMe, lockoutOnFailure: false);
                        if (result.Succeeded)
                        {
                            _logger.LogInformation("User logged in.");
                            return LocalRedirect(returnUrl);
                        }
                        if (result.RequiresTwoFactor)
                        {
                            return RedirectToPage("./LoginWith2fa", new
                            {
                                ReturnUrl = returnUrl,
                                RememberMe = loginModel.RememberMe
                            });
                        }
                        if (result.IsLockedOut)
                        {
                            _logger.LogWarning("User account locked out.");
                            return RedirectToPage("./Lockout");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                            ViewBag.Message = "Invalid login attempt.";
                            return View(loginModel);

                        }
                    }
                else
                {
                    ViewBag.Message2 = "suspicious as a Bot";
                }

                }
               // If we got this far, something failed, redisplay form
            return RedirectToAction("Index", "DataImporter");

        }

        [HttpPost]
        public async Task<IActionResult> LogOut(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "DataImporter");
            }
        }
    }
}
