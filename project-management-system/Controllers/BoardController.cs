using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using project_management_system.Models;

namespace project_management_system.Controllers
{
    public class BoardController : Controller
    {
        public IActionResult Index()
        {
            var model = new TaskModel();
            return View("../Views/Home.Board", model);
        }
    }
}