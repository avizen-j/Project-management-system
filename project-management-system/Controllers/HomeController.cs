﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using project_management_system.Context;
using project_management_system.Interfaces;
using project_management_system.Models;

namespace project_management_system.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDatabaseDriver _databaseDriver;

        public HomeController(ILogger<HomeController> logger, IDatabaseDriver databaseDriver)
        {
            _logger = logger;
            _databaseDriver = databaseDriver;
        }

        public IActionResult Index()
        {
            HomeFormModel model = new HomeFormModel();
            return View(model);
        }

        public IActionResult Board()
        {
            TaskModel model = new TaskModel();
            return View(model);
        }

        public IActionResult Submit(HomeFormModel model)
        {
            // TEMPORARY: For demonstration and test purposes only.
            var creationDate = DateTime.Now;
            model.User.RegistrationDate = creationDate;
            //_databaseDriver.InsertUser(model.User);

            var task = new UserTask()
            {
                TaskID = 1,
                TaskName = "Create something",
                TaskDescription = "aaasdas das dasd asdas d asd asd asd as das d",
                Priority = "Major",
                Status = "InProgress"
            };
            _databaseDriver.InsertTask(task);

            return View("Index", model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
