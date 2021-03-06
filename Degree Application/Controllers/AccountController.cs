﻿using System;
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
        private Degree_ApplicationContext _context;

        //Inbuilt .NET core namespace that allows for the creation and hashing
        //protected UserManager<AccountModel> _userManager = new UserManager<AccountModel>();

        public AccountController(UserManager<AccountModel> userManager,
            SignInManager<AccountModel> signInManager, Degree_ApplicationContext dbcontext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = dbcontext;
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

        [HttpGet]
        public IActionResult Edit()
        {
            AccountModel account = _context.Users.FirstOrDefault(x => x.Id == _userManager.GetUserId(HttpContext.User));
            return View(account);
        }

        [HttpPost]
        public IActionResult Edit(AccountModel model)
        {
            if (ModelState.IsValid)
            {
               var account =  _context.Users.FirstOrDefault(x => x.Id == model.Id);

                account.Mobile = model.Mobile;
                account.PostCode = model.PostCode;

                _context.SaveChangesAsync();

            }
            
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {

                //Map values to an ApplicationUser object
                AccountModel account = new AccountModel() { UserName = model.UserName, Email = model.Email, Mobile = model.Mobile, PostCode = model.PostCode };

                if (model.ProfilePicture != null)
                {


                    using (var memoryStream = new MemoryStream())
                    {
                        await model.ProfilePicture.CopyToAsync(memoryStream);
                        // account.ProfilePicture = memoryStream.ToArray();

                        ImageModel image = new ImageModel();

                        image.Image = memoryStream.ToArray();

                        _context.Image.Add(image);

                        account.ProfilePicture = image;

                        await _context.SaveChangesAsync();

                    }

                }

                //Wait for the account to be created
                var result = await _userManager.CreateAsync(account, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(account, isPersistent: false);

                    return RedirectToAction("Index", "Item");
                }

                else
                {

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                }

            }

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true. This will need implementing.
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Item");
                }

                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
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

            return Ok(new { filePath });
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Item");
        }

    }
}