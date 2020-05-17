using project_management_system.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_system.Interfaces
{
    public interface IDatabaseDriver
    {
        public Task InsertUser(User user);
        public Task InsertTask(UserTask task);
        public Task<UserTask> GetTask(int taskid);
        public Task DeleteUser(string username);
        public Task<bool> CheckIfExists(string username, string password);
    }
}
