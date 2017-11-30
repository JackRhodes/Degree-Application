using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Degree_Application.Models;
using Degree_Application.Models.AccountViewModels;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace Degree_Application.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private UserManager<AccountModel> _userManager;
        private SignInManager<AccountModel> _signInManager;

        //Inbuilt .NET core namespace that allows for the creation and hashing
        //protected UserManager<AccountModel> _userManager = new UserManager<AccountModel>();

        public AccountController(UserManager<AccountModel> userManager,
            SignInManager<AccountModel> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, IFormFile image)
        {

            if (ModelState.IsValid)
            {

                //Map values to an ApplicationUser object
                AccountModel account = new AccountModel() { UserName = model.UserName, Email = model.Email, Mobile = model.Mobile, PostCode = model.PostCode };

                //Wait for the account to be created
                var result = await _userManager.CreateAsync(account, model.Password);

                if (model.ProfilePicture != null)
                {

                    await ImageUpload(image);

                    using (var memoryStream = new MemoryStream())
                    {
                        await model.ProfilePicture.CopyToAsync(memoryStream);
                       // account.ProfilePicture = memoryStream.ToArray();
                    }

                }

            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
                if (result.Succeeded)
                {
                    RedirectToAction(nameof(HomeController.Index), "Home");
                }
            }

            return View();
        }

        [HttpPost("UploadFiles")]
        public async Task<IActionResult> ImageUpload(IFormFile image)
        {

            // full path to file in temp location
            var filePath = Path.GetTempFileName();

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { filePath });
        }
    }
}