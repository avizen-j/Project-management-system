using System;
using System.Collections.Generic;
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
    public class BoardController : Controller
    {
        private readonly ILogger<BoardController> _logger;
        private readonly IDatabaseDriver _databaseDriver;
        private static readonly Random random = new Random();

        public BoardController(ILogger<BoardController> logger, IDatabaseDriver databaseDriver)
        {
            _logger = logger;
            _databaseDriver = databaseDriver;
        }

        public IActionResult Index()
        {
            var model = new TaskModel(_databaseDriver);
            return View("../Home/Board", model);
        }

        public async Task<IActionResult> Task(int id)
        {
            var task = await _databaseDriver.GetAssignmentById(id);
            var taskModel = new TaskModel();
            taskModel.Assignment = task;
            return View("../Home/Assignment", taskModel);
        }
        public async Task<IActionResult> CreateTask(TaskModel taskModel)
        {      
            Assignment task = taskModel.Assignment;
            task.AssignmentID = random.Next(10000);
            task.Status = Status.ToDo.ToString();
            await _databaseDriver.InsertAssignment(task);
            return RedirectToAction("Index");
        }
        public async Task UpdateBoardAssignment(int assignmentId, string status)
        {
            await _databaseDriver.UpdateAssignmentStatus(assignmentId, status);
        }


    }
}