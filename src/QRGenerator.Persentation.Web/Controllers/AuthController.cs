﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using TrollMarket.Persentation.Web;
using Microsoft.AspNetCore.Authorization;
using QRGenerator.Persentation.Web.Models.Auth;
using QRGenerator.Persentation.Web.Services;

namespace QRGenerator.Persentation.Web.Controllers
{
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly UserService _userService;
        private readonly ILogger<AuthController> _logger;
        public AuthController( ILogger<AuthController> logger, UserService userService)
        {
            _userService = userService;
            _logger = logger;
        }
        [HttpGet("Login")]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                TempData["DataLogin"] = true;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Login");
            }
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            try
            {
                var authTicket = _userService.Login(vm);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, authTicket.Principal, authTicket.Properties);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Login");
            }
        }
       
        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
        [HttpGet("Register/{role}")]
        //public IActionResult Register(string role)
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        TempData["DataLogin"] = true;
        //        return RedirectToAction("Index", "Home");
        //    }
        //    else
        //    {
        //        RegisterViewModel vm = new RegisterViewModel
        //        {
        //            Role = role
        //        };
        //        return View("Register", vm);
        //    }
        //}
        [Authorize]
        [HttpGet("AccesDanied")]
        public IActionResult AccesDanied()
        {
            return View("AccesDanied");
        }
        [Route("{*url}", Order = int.MaxValue)]
        public IActionResult HandleUnknownRoutes()
        {
            return RedirectToAction("AccesDanied");
        }
    }
}
