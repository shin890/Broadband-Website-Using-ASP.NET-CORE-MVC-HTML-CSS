﻿using Microsoft.AspNetCore.Mvc;

namespace Broadband.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
