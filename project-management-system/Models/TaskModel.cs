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
        public TaskModel() { }
        public TaskModel(IDatabaseDriver databaseDriver)
        {
            _databaseDriver = databaseDriver;
        }

        //public TaskModel(IDatabaseDriver databaseDriver)
        //{
        //    _databaseDriver = databaseDriver;
        //}
        public Assignment Assignment { get; set; } = null!;

        public async Task<List<Assignment>> GetTasksByStatus(Status status)
        {
            return await _databaseDriver.GetAssignmentsByStatus(status);
            //return _databaseDriver.GetAssignmentsByStatus(status).Result;
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

    }

}
