using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GameSource.Controllers.GameSourceUser
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}