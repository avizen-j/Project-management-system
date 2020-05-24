using Microsoft.AspNetCore.Mvc.Rendering;
using project_management_system.Context;
using project_management_system.Enums;
using project_management_system.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_system.Models
{
    public class ProjectModel
    {
        private readonly IDatabaseDriver _databaseDriver;

        public Project Project { get; set; }

        public ProjectModel()
        {

        }
        public ProjectModel(IDatabaseDriver databaseDriver)
        {
            _databaseDriver = databaseDriver;
        }

        public async Task<List<Project>> GetAvailableProjects()
        {
            return await _databaseDriver.GetAvailableProjects();
        }

        public async Task<List<SelectListItem>> GetAvailableProjectsSelect()
        {
            var projects = await _databaseDriver.GetAvailableProjects();
            var projectIds = new List<int>();

            List<SelectListItem> item = projects.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.ProjectName,
                    Value = a.ProjectID.ToString(),
                    Selected = true
                };
            });

            return item;
        }

        public async Task<List<Assignment>> GetAssignmentsByProjectId(int projectId)
        {
            return await _databaseDriver.GetAssignmentsByProjectId(projectId);
        }

        public async Task<List<User>> GetUsersByProjectId(int projectId)
        {
            return await _databaseDriver.GetUsersByProjectId(projectId);
        }

        public int CountAssignmentsByStatusAndPriority(List<Assignment> assignments, Status status, Priority priority)
        {
            return assignments.Where(t => t.Status == status && t.Priority == priority).Count();
        }

        public int CountDoneAssignments(List<Assignment> assignments)
        {
            return assignments.Where(t => t.Status == Status.Done).Count();
        }

        public int CountAllAssignments(List<Assignment> assignments)
        {
            return assignments.Count();
        }
    }
}
