using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using project_management_system.Context;
using project_management_system.Enums;
using project_management_system.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_system.Models
{
    public class TaskModel
    {
        private readonly IDatabaseDriver _databaseDriver;

        public TaskModel()
        {

        }
        public TaskModel(IDatabaseDriver databaseDriver)
        {
            _databaseDriver = databaseDriver;
        }

        public Assignment Assignment { get; set; } = null!;

        public async Task<List<Assignment>> GetTasksByStatus(Status status)
        {
            return await _databaseDriver.GetAssignmentsByStatus(status);
        }

        public async Task<List<string>> GetAllAssignees(int assignmentId)
        {
            var assigneeUsernames = new List<string>();
            var assignees = await _databaseDriver.GetUsersBelongingToAssignment(assignmentId);
            foreach (var assignee in assignees)
            {
                assigneeUsernames.Add(assignee.Username);
            }

            return assigneeUsernames;
        }
        public async Task<List<string>> GetAllUsers()
        {
            var usernames = new List<string>();
            var users = await _databaseDriver.GetAllUsers();
            foreach (var user in users)
            {
                usernames.Add(user.Username);
            }

            return usernames;
        }

        public async Task<List<Comment>> GetAllComments(int assignmentId)
        {
            var comments = await _databaseDriver.GetAllAssignmentComments(assignmentId);

            return comments;
        }

        public async Task<List<SelectListItem>> GetAvailableProjects()
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

    }

}
