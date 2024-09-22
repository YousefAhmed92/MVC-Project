using company.services.Helper;
using Company.app.Models;
using Company.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Company.app.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController
            (
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #region SignUp

        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel Input)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {

                    UserName = Input.Email.Split("@")[0],
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    //SecondName = Input.LastName,
                    SecondName = Input.SecondName,
                    IsActive = true,
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                    return RedirectToAction("Login");
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(Input);
        }
        #endregion

        #region Login

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel input)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(input.Email);
                if (user is not null )
                {
                    if (await _userManager.CheckPasswordAsync(user , input.Password))
                    {
                        var result = await _signInManager
                                    .PasswordSignInAsync(user, input.Password, input.RememberMe, true);
                        if (result.Succeeded)
                            return RedirectToAction("Index","Home");
                    }
                }
                ModelState.AddModelError("","Incorrect Email or Password");
                return View(input);
            }
            return View(input);

        }

        #endregion

        #region SignOut
        public new async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        #endregion


        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel input)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync (input.Email);
                if (user is not null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var url = Url.Action("ResetPassword", "Account", new { email = input.Email, Token = token }, Request.Scheme);

                    var email = new Email
                    {
                        Body = url,
                        Subject = "Reset Your Password",
                        To = input.Email
                    };

                    EmailSetting.SendEmail(email);
                    return RedirectToAction(nameof(CheckYourInbox));


                }
            } 
            return View(input);
           
        }

        public IActionResult CheckYourInbox()
        {
            return View();
        }



    }
}
