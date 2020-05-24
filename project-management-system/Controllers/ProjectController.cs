using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using project_management_system.Context;
using project_management_system.Interfaces;
using project_management_system.Models;

namespace project_management_system.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ILogger<ProjectController> _logger;
        private readonly IDatabaseDriver _databaseDriver;
        private static readonly Random random = new Random();

        public ProjectController(ILogger<ProjectController> logger, IDatabaseDriver databaseDriver)
        {
            _logger = logger;
            _databaseDriver = databaseDriver;
        }
        public IActionResult Projects()
        {
            var model = new ProjectModel(_databaseDriver);
            return View("../Home/Projects", model);
        }

        public async Task<IActionResult> Project(int id)
        {
            try
            {
                var project = await _databaseDriver.GetProjectById(id);
                var projectModel = new ProjectModel(_databaseDriver);
                projectModel.Project = project;
                return View("../Home/Project", projectModel);
            }
            catch
            {
                return View("../Shared/Error", new ErrorViewModel { Message = "Project cannot be opened" });
            }
        }

        public async Task<IActionResult> UpdateProjectStartDate(int id, DateTime startDate)
        {
            try
            {
                await _databaseDriver.UpdateProjectStartDate(id, startDate);
                return RedirectToAction("Projects");
            }
            catch
            {
                return View("../Shared/Error", new ErrorViewModel { Message = "Start date cannot be updated" });
            }
        }

        public async Task<IActionResult> UpdateProjectEndDate(int id, DateTime endDate)
        {
            try
            {
                await _databaseDriver.UpdateProjectEndDate(id, endDate);
                return RedirectToAction("Projects");
            }
            catch
            {
                return View("../Shared/Error", new ErrorViewModel { Message = "Start date cannot be updated" });
            }
        }
        public async Task<IActionResult> CreateProject(ProjectModel projectModel)
        {
            try
            {
                Project project = projectModel.Project;
                project.ProjectID = random.Next(10000);
                await _databaseDriver.InsertProject(project);
                return RedirectToAction("Projects");
            }
            catch
            {
                return View("../Shared/Error", new ErrorViewModel { Message = "Project cannot be created" });
            }
        }
    }
}