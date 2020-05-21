﻿using project_management_system.Context;
using project_management_system.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_system.Interfaces
{
    public interface IDatabaseDriver
    {
        // Assignments.
        public Task InsertAssignment(Assignment task);
        public Task DeleteAssignment(int assignmentId);
        public Task<List<Assignment>> GetAllAssignments();
        public Task<Assignment> GetAssignmentById(int assignmentId);
        public Task<List<Assignment>> GetAssignmentsByStatus(Status status);
        public Task UpdateAssignmentStatus(int assignmentId, Status newStatus);
        public Task UpdateAssignmentPriority(int assignmentId, Priority newPriority);
        public Task UpdateAssignmentStartDate(int assignmentId, DateTime startDate);
        public Task UpdateAssignmentEndDate(int assignmentId, DateTime endDate);


        // Users.
        public Task InsertUser(User user);
        public Task DeleteUser(string username);
        public Task<List<User>> GetAllUsers();
        public Task<User> GetUserById(int userId);
        public Task<User> GetUserByUsername(string username);
        public Task<List<string>> GetUserStartingWithTerm(string term);

        // Links.
        public Task LinkUserAssignment(int assignmentId, int userId);
        public Task<List<Assignment>> GetAssignmentsBelongingToUser(int userId);
        public Task<List<User>> GetUsersBelongingToAssignment(int assignmentId);
        public Task RemoveUserFromAssignment(int assignmentId, int userId);
    }
}
