using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using project_management_system.Context;
using project_management_system.Enums;
using project_management_system.Interfaces;
using project_management_system.Models;

namespace project_management_system.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDatabaseDriver _databaseDriver;
        private static readonly Random random = new Random();

        public HomeController(ILogger<HomeController> logger, IDatabaseDriver databaseDriver)
        {
            _logger = logger;
            _databaseDriver = databaseDriver;
        }

        public IActionResult Projects()
        {
            HomeFormModel model = new HomeFormModel();
            return View(model);
        }

        public IActionResult Board()
        {
            TaskModel model = new TaskModel()
            {
                Assignment = new Assignment(),
            };
            return View(model);
        }

        public async Task<IActionResult> Submit(HomeFormModel model)
        {
            //var project = new Project()
            //{
            //    ProjectID = random.Next(100),
            //    ProjectName = "Startup project",
            //    ProjectDescription = "For those who want to try something new",
            //    CreationDate = DateTime.Now,
            //    StartDate = DateTime.Now.AddDays(-3),
            //    EndDate = DateTime.Now.AddDays(10),
            //};

            //await _databaseDriver.InsertProject(project);

            return View("Index", model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
