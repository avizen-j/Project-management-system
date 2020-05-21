﻿using Microsoft.EntityFrameworkCore;
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

        public async Task UpdateAssignmentStatus(int assignmentId, string newStatus)
        {
            var assignment = await _context.Assignments.FirstOrDefaultAsync(t => t.AssignmentID == assignmentId);
            if (assignment != default)
            {
                assignment.Status = newStatus;
                await _context.SaveChangesAsync();
            }
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

        public async Task<User> GetUserByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(t => t.Username == username);
        }

        public async Task<List<string>> GetUserStartingWithTerm(string term)
        {
            return await _context.Users.Where(t => t.Username.StartsWith(term)).Select(t => t.Username).ToListAsync();
        }

        // Link user and task(assignment).
        public async Task LinkUserAssignment(int assignmentId, int userId)
        {
            var assignment = await GetAssignmentById(assignmentId);
            var user = await GetUserById(userId);
            var link = new AssignmentUser()
            {
                Assignment = assignment,
                User = user,
            };

            _context.Add(link);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetUsersBelongingToAssignment(int assignmentId)
        {
            return await _context.Users.Where(item => item.AssignmentUsers.Any(j => j.AssignmentID == assignmentId))
                                       .ToListAsync();
        }

        public async Task<List<Assignment>> GetAssignmentsBelongingToUser(int userId)
        {
            return await _context.Assignments.Where(item => item.AssignmentUsers.Any(j => j.UserID == userId))
                                             .ToListAsync();
        }

        public async Task RemoveUserFromAssignment(int assignmentId, int userId)
        {
            var assignment = await _context.Assignments.Include(a => a.AssignmentUsers).FirstOrDefaultAsync(item => item.AssignmentID == assignmentId);
            if (assignment != default)
            {
                var assignmentUser = assignment.AssignmentUsers.FirstOrDefault(item => item.UserID == userId);
                assignment.AssignmentUsers.Remove(assignmentUser);
                await _context.SaveChangesAsync();
            }
        }
    }
}
