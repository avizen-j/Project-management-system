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

        public IActionResult Main()
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

            //var assignment = new Assignment()
            //{
            //    AssignmentID = random.Next(100),
            //    AssignmentDescription = "Test description",
            //    AssignmentName = "Update table fields",
            //    CreationDate = DateTime.Now,
            //    StartDate = DateTime.Now,
            //    EndDate = DateTime.Now.AddMinutes(2),
            //    Priority = Priority.Major,
            //    Status = Status.Done,
            //    Type = Enums.Type.NewFeature,
            //};

            //await _databaseDriver.InsertAssignment(assignment);

            //var duplicateUser = new User()
            //{
            //    UserID = random.Next(199),
            //    Username = "MantasDeveloperis",
            //    Password = "djskdsfjklfdskjl",
            //    RegistrationDate = DateTime.Now,
            //    Email = "universityadvisor.lt@gmail.com",
            //};

            //await _databaseDriver.InsertUser(duplicateUser);

            return View("Index", model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
