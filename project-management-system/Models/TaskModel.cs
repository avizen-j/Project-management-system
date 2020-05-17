using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_system.Models
{
    public class TaskModel
    {
        public int TaskID { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public List<UserModel> watchers { get; set; } 

        public List<String> statuses = new List<string>()
        {
            {"Backlog"},
            {"ToDo"},
            {"InProgress"},
            {"Done"},
        };

        public TaskModel()
        {

        }

        public TaskModel(int taskId, string taskName, string taskDescription, string priority, string status)
        {
            this.TaskID = taskId;
            this.TaskName = taskName;
            this.TaskDescription = taskDescription;
            this.Priority = priority;
            this.Status= status;
            //getTaskInfoById(taskId);
        }

        public Dictionary<string, string> tempTask = new Dictionary<string, string>()
        {
            {"taskId", "001"},
            {"task_description", "Create something ril special"}
        };

        public void AddTask()
        {
            // TODO - insert into tasks (...) values ...
            throw new NotImplementedException();
        }

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
