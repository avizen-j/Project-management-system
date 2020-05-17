using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using project_management_system.Interfaces;
using project_management_system.Models;

namespace project_management_system.Controllers
{
    public class BoardController : Controller
    {
        private readonly ILogger<BoardController> _logger;
        private readonly IDatabaseDriver _databaseDriver;

        public BoardController(ILogger<BoardController> logger, IDatabaseDriver databaseDriver)
        {
            _logger = logger;
            _databaseDriver = databaseDriver;
        }

        public IActionResult Index()
        {
            var model = new TaskModel();
            return View("../Views/Home/Board", model);
        }

        public IActionResult Task(int id)
        {
            var task = _databaseDriver.GetTask(id);
            var taskModel = new TaskModel(task.Result.TaskID, task.Result.TaskName, task.Result.TaskDescription, task.Result.Priority, task.Result.Status);
            return View("../Home/Task", taskModel);
        }
    }
}