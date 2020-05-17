using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using project_management_system.Interfaces;

namespace project_management_system.Controllers
{
    public class TaskController : Controller
    {
        private readonly ILogger<TaskController> _logger;
        private readonly IDatabaseDriver _databaseDriver;
        public TaskController(ILogger<TaskController> logger, IDatabaseDriver databaseDriver)
        {
            _logger = logger;
            _databaseDriver = databaseDriver;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}