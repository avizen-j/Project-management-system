using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
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

        public async Task<IActionResult> ProgressAnalysis(int projectId)
        {
            var project = await _databaseDriver.GetProjectById(projectId);
            var model = new ProjectModel(_databaseDriver);
            model.Project = project;
            return View("../Home/ProgressAnalysis", model);
        }

        public async Task<IActionResult> Task(int id)
        {
            try
            {
                var assignment = await _databaseDriver.GetAssignmentById(id);
                var project = await _databaseDriver.GetProjectById(assignment.ProjectID);
                assignment.Project = project;
                var taskModel = new TaskModel(_databaseDriver);
                taskModel.Assignment = assignment;
                return View("../Home/Assignment", taskModel);
            }
            catch
            {
                return View("../Shared/Error", new ErrorViewModel { Message = "Task cannot be opened" });
            }
        }

        public async Task<IActionResult> RemoveAssignee(int id, string assignedUser)
        {
            try
            {
                var assignment = await _databaseDriver.GetAssignmentById(id);
                var user = await _databaseDriver.GetUserByUsername(assignedUser);
                await _databaseDriver.RemoveUserFromAssignment(assignment.AssignmentID, user.UserID);
                var taskModel = new TaskModel(_databaseDriver);
                taskModel.Assignment = assignment;
                return View("../Home/Assignment", taskModel);
            }
            catch
            {
                return View("../Shared/Error", new ErrorViewModel { Message = "Assignee cannot be removed" });
            }
        }

        public async Task<IActionResult> UpdateAssignee(int id, string assigneeUsername)
        {
            try
            {
                var assignment = await _databaseDriver.GetAssignmentById(id);
                var user = await _databaseDriver.GetUserByUsername(assigneeUsername);
                await _databaseDriver.LinkUserAssignment(assignment.AssignmentID, user.UserID);
                var taskModel = new TaskModel(_databaseDriver);
                taskModel.Assignment = assignment;
                return View("../Home/Assignment", taskModel);
            }
            catch
            {
                return View("../Shared/Error", new ErrorViewModel { Message = "Assignee cannot be added" });
            }
        }
        public async Task<IActionResult> CreateTask(TaskModel taskModel)
        {
            try
            {
                Assignment assignment = taskModel.Assignment;
                assignment.AssignmentID = random.Next(10000);
                assignment.Status = Status.ToDo;
                await _databaseDriver.InsertAssignment(assignment);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("../Shared/Error", new ErrorViewModel { Message = "Task cannot be created" });
            }
        }

        public async Task<IActionResult> UpdateAssignmentPriority(int id, Priority priority)
        {
            try
            {
                await _databaseDriver.UpdateAssignmentPriority(id, priority);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("../Shared/Error", new ErrorViewModel { Message = "Priority cannot be updated" });
            }
        }

        public async Task<IActionResult> UpdateAssignmentProject(int id, string projectNumber)
        {
            try
            {
                var pNumber = int.Parse(projectNumber);
                await _databaseDriver.UpdateAssignmentProject(id, pNumber);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("../Shared/Error", new ErrorViewModel { Message = "Project cannot be updated" });
            }
        }

        public async Task<IActionResult> UpdateAssignmentStartDate(int id, DateTime startDate)
        {
            try
            {
                await _databaseDriver.UpdateAssignmentStartDate(id, startDate);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("../Shared/Error", new ErrorViewModel { Message = "Start date cannot be updated" });
            }
        }

        public async Task<IActionResult> UpdateAssignmentEndDate(int id, DateTime endDate)
        {
            try
            {
                await _databaseDriver.UpdateAssignmentEndDate(id, endDate);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("../Shared/Error", new ErrorViewModel { Message = "Start date cannot be updated" });
            }
        }

        public async Task UpdateBoardAssignment(int assignmentId, Status status)
        {
            await _databaseDriver.UpdateAssignmentStatus(assignmentId, status);
        }

        public async Task<IActionResult> Reply(int id, string comment)
        {
            try
            {
                var assignment = await _databaseDriver.GetAssignmentById(id);
                var newComment = new Comment()
                {
                    CommentID = random.Next(10000),
                    CommentContent = comment,
                    CreationTime = DateTime.Now,
                };

                await _databaseDriver.AddNewComment(id, newComment);

                return RedirectToAction("Task", new { id = id });
            }
            catch (Exception ex)
            {
                return View("../Shared/Error", new ErrorViewModel { Message = "Comment cannot be added" + ex.StackTrace});
            }
        }

    }
}