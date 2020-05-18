using Microsoft.EntityFrameworkCore;
using project_management_system.Context;
using project_management_system.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_system.Services
{
    public class DatabaseDriver : IDatabaseDriver
    {
        private readonly MyContext _context;

        public DatabaseDriver(MyContext userContext)
        {
            _context = userContext;
        }

        // Assignments.
        public async Task InsertAssignment(Assignment task)
        {
            _context.Assignments.Add(task);
            await _context.SaveChangesAsync();
        }
        
        public async Task DeleteAssignment(int assignmentId)
        {
            var assignment = await _context.Assignments.FirstOrDefaultAsync(t => t.AssignmentID == assignmentId);
            _context.Assignments.Remove(assignment);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Assignment>> GetAllAssignments()
        {
            return await _context.Assignments.ToListAsync();
        }

        public async Task<Assignment> GetAssignmentById(int assignmentId)
        {
            return await _context.Assignments.FirstOrDefaultAsync(t => t.AssignmentID == assignmentId);
        }

        public async Task<List<Assignment>> GetAssignmentsByStatus(string status)
        {
            return await _context.Assignments.Where(t => t.Status == status).ToListAsync();
        }

        // Users.
        public async Task InsertUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUser(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(usr => usr.Username == username);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserById(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(t => t.UserID == userId);
        }
    }
}
