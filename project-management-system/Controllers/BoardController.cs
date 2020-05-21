using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
            var assignment = await _databaseDriver.GetAssignmentById(id);
            var taskModel = new TaskModel(_databaseDriver);
            taskModel.Assignment = assignment;
            return View("../Home/Assignment", taskModel);
        }

        public async Task<IActionResult> RemoveAssignee(int id, string assignedUser)
        {

            var assignment = await _databaseDriver.GetAssignmentById(id);
            var user = await _databaseDriver.GetUserByUsername(assignedUser);
            try
            {
                await _databaseDriver.RemoveUserFromAssignment(assignment.AssignmentID, user.UserID);
            }
            catch
            {
                return View("../Shared/Error", new ErrorViewModel { Message = "Assignee cannot be removed" });
            }
            var taskModel = new TaskModel(_databaseDriver);
            taskModel.Assignment = assignment;
            return View("../Home/Assignment", taskModel);
        }

        public async Task<IActionResult> UpdateAssignee(int id, string assigneeUsername)
        {
            var assignment = await _databaseDriver.GetAssignmentById(id);
            var user = await _databaseDriver.GetUserByUsername(assigneeUsername);
            try
            {
                await _databaseDriver.LinkUserAssignment(assignment.AssignmentID, user.UserID);
            }
            catch
            {
                return View("../Shared/Error", new ErrorViewModel { Message = "Assignee cannot be added" });
            }
            var taskModel = new TaskModel(_databaseDriver);
            taskModel.Assignment = assignment;
            return View("../Home/Assignment", taskModel);
        }
        public async Task<IActionResult> CreateTask(TaskModel taskModel)
        {
            Assignment assignment = taskModel.Assignment;
            assignment.AssignmentID = random.Next(10000);
            assignment.Status = Status.ToDo.ToString();
            await _databaseDriver.InsertAssignment(assignment);
            return RedirectToAction("Index");
        }

        public async Task UpdateBoardAssignment(int assignmentId, string status)
        {
            await _databaseDriver.UpdateAssignmentStatus(assignmentId, status);
        }

    }
}