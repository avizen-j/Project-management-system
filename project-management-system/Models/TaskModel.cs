using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_system.Models
{
    public class TaskModel
    {
        public string TaskId { get; set; }
        public string TaskName { get; set; }

        public List<String> statuses = new List<string>()
        {
            {"Backlog"},
            {"ToDo"},
            {"InProgress"},
            {"Done"},
        };

        public Dictionary<string, string> tempTask = new Dictionary<string, string>()
        {
            {"task_name", "Create something"},
            {"task_description", "Create something ril special"}
        };

        public List<Dictionary<string, object>> GetAllTasks()
        {
            // TODO - select * from tasks;
            throw new NotImplementedException();
        }

        public List<Dictionary<string, string>> GetTasksByStatus(string status)
        {
            // TODO - select * from tasks where task_status = status;
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            list.Add(tempTask);
            return list;
        }

    }

}
