using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using project_management_system.Context;
using project_management_system.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_system.Services
{
    public class DatabaseDriver : IDatabaseDriver
    {
        private UserContext _userContext;

        public DatabaseDriver(UserContext userContext)
        {
            _userContext = userContext;
        }

        public async Task InsertUser(User user)
        {
            _userContext.Users.Add(user);
            _userContext.SaveChanges();
        }

        public async Task<bool> CheckIfExists(string username, string password)
        {
            return await _userContext.Users.AnyAsync(usr => usr.Username == username && usr.Password == password);
        }

        public async Task DeleteUser(string username)
        {
            var user = await _userContext.Users.FirstOrDefaultAsync(usr => usr.Username == username);
            _userContext.Users.Remove(user);
            await _userContext.SaveChangesAsync();
        }
    }
}
