using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVCApplication.Controllers
{
    public class Master : Controller
    {
        public IActionResult Country()
        {
            return View();
        }
        public IActionResult State()
        {
            return View();
        }
        public IActionResult City()
        {
            return View();
        }
    }
}
