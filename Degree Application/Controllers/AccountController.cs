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
using Degree_Application.Data;

namespace Degree_Application.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private UserManager<AccountModel> _userManager;
        private SignInManager<AccountModel> _signInManager;
        private Degree_ApplicationContext _dbContext;

        //Inbuilt .NET core namespace that allows for the creation and hashing
        //protected UserManager<AccountModel> _userManager = new UserManager<AccountModel>();

        public AccountController(UserManager<AccountModel> userManager,
            SignInManager<AccountModel> signInManager, Degree_ApplicationContext dbcontext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbcontext;
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
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {

                if (model.ProfilePicture != null)
                {

                    //Map values to an ApplicationUser object
                    AccountModel account = new AccountModel() { UserName = model.UserName, Email = model.Email, Mobile = model.Mobile, PostCode = model.PostCode };

                    using (var memoryStream = new MemoryStream())
                    {
                        await model.ProfilePicture.CopyToAsync(memoryStream);
                        // account.ProfilePicture = memoryStream.ToArray();

                        ImageModel image = new ImageModel();

                        image.Image = memoryStream.ToArray();

                        _dbContext.Image.Add(image);

                        account.ProfilePicture = image;

                        await _dbContext.SaveChangesAsync();

                    }

                    //Wait for the account to be created
                    var result = await _userManager.CreateAsync(account, model.Password);

                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(account, isPersistent: false);

                        return RedirectToAction("Index", "Home");
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
                    return RedirectToAction("Index", "Home");
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