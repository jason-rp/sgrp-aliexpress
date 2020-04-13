using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SGRP.Aliexpress.Data;
using SGRP.Aliexpress.Web.Hubs;
using SGRP.Aliexpress.Web.Models;

namespace SGRP.Aliexpress.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly AliexpressHub _aliexpressHub;
        public LoginController(ApplicationDbContext context, AliexpressHub aliexpressHub)
        {
            _context = context;
            _aliexpressHub = aliexpressHub;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _context.Admins.Any(n => n.Username == model.Username && n.Password == model.Password);
                if (result)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "Invalid username/password !!!");
            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "")
        {
            var model = new LoginViewModel { };
            return View();
        }
    }
}