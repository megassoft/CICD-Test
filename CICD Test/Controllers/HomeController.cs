using CICD_Test.Models;
using CICD_Test.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace CICD_Test.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(ICheckData checkData)
        {

        }
          
        public int Add(int x, int y)
        {
            //debug3 - done9
            int total = x + y;
             
            return total;
        }

        public int Multiply(int x, int y)
        {
        
            //debug
            int total = x * y;

            return total;
        }

        public IActionResult Index()
        {
            return View("Index");
        }

        public IActionResult Products()
        {
            return View(new Product { id=1, name = "PC" });
        }
    }
}
