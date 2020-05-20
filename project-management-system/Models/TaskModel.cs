using Microsoft.Extensions.Logging;
using project_management_system.Context;
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

        public async Task<List<Assignment>> GetTasksByStatus(string status)
        {
            return await _databaseDriver.GetAssignmentsByStatus(status);
            //return _databaseDriver.GetAssignmentsByStatus(status).Result;
        }

    }

}
