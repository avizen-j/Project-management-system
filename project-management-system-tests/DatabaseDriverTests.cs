using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NUnit.Framework;
using project_management_system.Context;
using project_management_system.Enums;
using project_management_system.Services;
using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_system_tests
{
    public class DatabaseDriverTests
    {
        private DbContextOptions<MyContext> _dbOptions;
        private MyContext _myContext;
        private DatabaseDriver _driver;

        [SetUp]
        public void Setup()
        {
            _dbOptions = new DbContextOptionsBuilder<MyContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
            _myContext = new MyContext(_dbOptions);
            _myContext.Database.EnsureCreated();
            _driver = new DatabaseDriver(_myContext);
        }

        // Assignments.

        [Test]
        public async Task InsertAssignment_inserts_assignment_to_db()
        {
            var expectedAssignment = new Assignment()
            {
                AssignmentID = 111,
                AssignmentDescription = "Test description",
                AssignmentName = "First assignment",
                Priority = Priority.Minor,
                Status = Status.InProgress,
            };

            await _driver.InsertAssignment(expectedAssignment);

            var actualAssignment = await _myContext.Assignments.FirstOrDefaultAsync(r => r.AssignmentID == 111);
            actualAssignment.AssignmentID.Should().Be(expectedAssignment.AssignmentID);
            actualAssignment.AssignmentDescription.Should().Be(expectedAssignment.AssignmentDescription);
            actualAssignment.AssignmentName.Should().Be(expectedAssignment.AssignmentName);
            actualAssignment.Priority.Should().Be(expectedAssignment.Priority);
            actualAssignment.Status.Should().Be(expectedAssignment.Status);
        }

        [Test]
        public async Task DeleteAssignment_deletes_assignment_from_db()
        {
            var expectedAssignment = new Assignment()
            {
                AssignmentID = 111,
                AssignmentDescription = "Test description",
                AssignmentName = "First assignment",
                Priority = Priority.Minor,
                Status = Status.InProgress,
            };

            _myContext.Assignments.Add(expectedAssignment);
            await _myContext.SaveChangesAsync();
            await _driver.DeleteAssignment(expectedAssignment.AssignmentID);

            var result = await _myContext.Assignments.FirstOrDefaultAsync(r => r.AssignmentID == expectedAssignment.AssignmentID);
            result.Should().Be(default);
        }

        [Test]
        public async Task UpdateAssignmentStatus_updates_assignment_status_with_new_one()
        {
            var newStatus = Status.InProgress;
            var assignment = new Assignment()
            {
                AssignmentID = 111,
                AssignmentDescription = "Test description",
                AssignmentName = "First assignment",
                Priority = Priority.Minor,
                Status = Status.ToDo,
            };
            _myContext.Assignments.Add(assignment);
            await _myContext.SaveChangesAsync();

            var oldStatus = await _myContext.Assignments.Where(r => r.AssignmentID == assignment.AssignmentID).Select(t => t.Status).SingleOrDefaultAsync();
            await _driver.UpdateAssignmentStatus(assignment.AssignmentID, newStatus);
            var updatedStatus = await _myContext.Assignments.Where(r => r.AssignmentID == assignment.AssignmentID).Select(t => t.Status).SingleOrDefaultAsync();

            updatedStatus.Should().Be(newStatus);
        }

        [Test]
        public async Task UpdateAssignmentPriority_updates_assignment_priority_with_new_one()
        {
            var newPriority = Priority.Critical;
            var assignment = new Assignment()
            {
                AssignmentID = 111,
                AssignmentDescription = "Test description",
                AssignmentName = "First assignment",
                Priority = Priority.Minor,
                Status = Status.ToDo,
            };
            _myContext.Assignments.Add(assignment);
            await _myContext.SaveChangesAsync();

            var oldPriority = await _myContext.Assignments.Where(r => r.AssignmentID == assignment.AssignmentID).Select(t => t.Priority).SingleOrDefaultAsync();
            await _driver.UpdateAssignmentPriority(assignment.AssignmentID, newPriority);
            var updatedPriority = await _myContext.Assignments.Where(r => r.AssignmentID == assignment.AssignmentID).Select(t => t.Priority).SingleOrDefaultAsync();

            updatedPriority.Should().Be(newPriority);
        }

        [Test]
        public async Task UpdateAssignmentStartDate_updates_assignment_start_date_with_new_one()
        {
            var newDate = DateTime.Now.AddDays(2);
            var assignment = new Assignment()
            {
                AssignmentID = 111,
                AssignmentDescription = "Test description",
                AssignmentName = "First assignment",
                Priority = Priority.Minor,
                Status = Status.ToDo,
                StartDate = DateTime.Now,
            };
            _myContext.Assignments.Add(assignment);
            await _myContext.SaveChangesAsync();

            var oldDate = await _myContext.Assignments.Where(r => r.AssignmentID == assignment.AssignmentID).Select(t => t.StartDate).SingleOrDefaultAsync();
            await _driver.UpdateAssignmentStartDate(assignment.AssignmentID, newDate);
            var updatedDate = await _myContext.Assignments.Where(r => r.AssignmentID == assignment.AssignmentID).Select(t => t.StartDate).SingleOrDefaultAsync();

            updatedDate.Should().Be(newDate);
        }

        [Test]
        public async Task UpdateAssignmentStartDate_updates_assignment_end_date_with_new_one()
        {
            var newDate = DateTime.Now.AddDays(6);
            var assignment = new Assignment()
            {
                AssignmentID = 111,
                AssignmentDescription = "Test description",
                AssignmentName = "First assignment",
                Priority = Priority.Minor,
                Status = Status.ToDo,
                EndDate = DateTime.Now,
            };
            _myContext.Assignments.Add(assignment);
            await _myContext.SaveChangesAsync();

            var oldDate = await _myContext.Assignments.Where(r => r.AssignmentID == assignment.AssignmentID).Select(t => t.EndDate).SingleOrDefaultAsync();
            await _driver.UpdateAssignmentEndDate(assignment.AssignmentID, newDate);
            var updatedDate = await _myContext.Assignments.Where(r => r.AssignmentID == assignment.AssignmentID).Select(t => t.EndDate).SingleOrDefaultAsync();

            updatedDate.Should().Be(newDate);
        }

        [Test]
        public async Task GetAssigmentById_gets_assignment_by_id()
        {
            var assignment = new Assignment()
            {
                AssignmentID = 111,
                AssignmentDescription = "Test description",
                AssignmentName = "First assignment",
                Priority = Priority.Minor,
                Status = Status.ToDo,
                EndDate = DateTime.Now,
            };

            await _driver.InsertAssignment(assignment);

            var actualAssignment = await _driver.GetAssignmentById(111);
            actualAssignment.Should().Be(assignment);
        }

        [Test]
        public async Task GetAssignmentsByStatus_gets_assignment_by_status()
        {
            var assignment = new Assignment()
            {
                AssignmentID = 111,
                AssignmentDescription = "Test description",
                AssignmentName = "First assignment",
                Priority = Priority.Minor,
                Status = Status.ToDo,
                EndDate = DateTime.Now,
            };

            await _driver.InsertAssignment(assignment);

            var actualAssignmentList = await _driver.GetAssignmentsByStatus(Status.ToDo);

            actualAssignmentList.Should().NotBeNullOrEmpty();
            actualAssignmentList[0].Should().Be(assignment);
        }


        [Test]
        public async Task GetAllAssignments_gets_all_assignments()
        {
            var assignment = new Assignment()
            {
                AssignmentID = 111,
                AssignmentDescription = "Test description",
                AssignmentName = "First assignment",
                Priority = Priority.Minor,
                Status = Status.ToDo,
                EndDate = DateTime.Now,
            };

            var assignment2 = new Assignment()
            {
                AssignmentID = 222,
                AssignmentDescription = "Test description",
                AssignmentName = "Second assignment",
                Priority = Priority.Major,
                Status = Status.InProgress,
                EndDate = DateTime.Now,
            };

            await _driver.InsertAssignment(assignment);
            await _driver.InsertAssignment(assignment2);

            var actualAssignmentList = await _driver.GetAllAssignments();

            actualAssignmentList.Should().NotBeNullOrEmpty();
            actualAssignmentList.Count().Should().Be(2);
        }

        // Users. 
        [Test]
        public async Task InsertUser_inserts_user_to_db()
        {
            var expectedUser = new User()
            {
                UserID = 111,
                Username = "TestUser",
                Password = "Plaintextpassword",
                RegistrationDate = DateTime.Now,
            };

            await _driver.InsertUser(expectedUser);

            var actualUser = await _myContext.Users.FirstOrDefaultAsync(r => r.UserID == 111);
            actualUser.UserID.Should().Be(expectedUser.UserID);
            actualUser.Username.Should().Be(expectedUser.Username);
            actualUser.Password.Should().Be(expectedUser.Password);
            actualUser.RegistrationDate.Should().Be(expectedUser.RegistrationDate);
        }

        [Test]
        public async Task InsertUser_throws_if_same_user_id_is_inserted()
        {
            var expectedUser = new User()
            {
                UserID = 111,
                Username = "TestUser",
                Password = "Plaintextpassword",
                RegistrationDate = DateTime.Now,
            };

            var duplicateUser = new User()
            {
                UserID = 111,
                Username = "Different",
                Password = "Plaintextpassword",
                RegistrationDate = DateTime.Now,
            };

            await _driver.InsertUser(expectedUser);

            await _driver.Awaiting(s => s.InsertUser(duplicateUser)).Should().ThrowAsync<InvalidOperationException>();
        }

        [Test]
        public async Task DeleteUser_deletes_user_from_db()
        {
            var expectedUser = new User()
            {
                UserID = 111,
                Username = "TestUser",
                Password = "Plaintextpassword",
                RegistrationDate = DateTime.Now,
            };

            _myContext.Users.Add(expectedUser);
            await _myContext.SaveChangesAsync();
            await _driver.DeleteUser(expectedUser.Username);

            var result = await _myContext.Users.FirstOrDefaultAsync(r => r.UserID == expectedUser.UserID);
            result.Should().Be(default);
        }

        // Links.

        [Test]
        public async Task LinkUserAssignment_links_user_with_several_assignments()
        {
            var user = new User()
            {
                UserID = 22,
                Username = "TestUser",
                Password = "Plaintextpassword",
                RegistrationDate = DateTime.Now,
            };
            var assignment1 = new Assignment()
            {
                AssignmentID = 111,
                AssignmentDescription = "Test description",
                AssignmentName = "First assignment",
                Priority = Priority.Minor,
                Status = Status.ToDo,
            };
            var assignment2 = new Assignment()
            {
                AssignmentID = 112,
                AssignmentDescription = "Test description 2",
                AssignmentName = "Second assignment",
                Priority = Priority.Major,
                Status = Status.ToDo,
            };
            _myContext.Users.Add(user);
            _myContext.Assignments.Add(assignment1);
            _myContext.Assignments.Add(assignment2);
            await _myContext.SaveChangesAsync();

            await _driver.LinkUserAssignment(assignment1.AssignmentID, user.UserID);
            await _driver.LinkUserAssignment(assignment2.AssignmentID, user.UserID);

            var result = await _myContext.Assignments.Where(item => item.AssignmentUsers.Any(j => j.UserID == user.UserID))
                                                     .ToListAsync();
            result.Count.Should().Be(2);
            result[0].Should().Be(assignment1);
            result[1].Should().Be(assignment2);
        }

        [Test]
        public async Task LinkUserAssignment_links_assignment_with_several_users()
        {
            var user1 = new User()
            {
                UserID = 22,
                Username = "TestUser1",
                Password = "Plaintextpassword31",
                RegistrationDate = DateTime.Now,
            };
            var user2 = new User()
            {
                UserID = 23,
                Username = "TestUser2",
                Password = "Plaintextpassword123",
                RegistrationDate = DateTime.Now,
            };
            var user3 = new User()
            {
                UserID = 24,
                Username = "TestUser3",
                Password = "Plaintextpassword124",
                RegistrationDate = DateTime.Now,
            };
            var assignment1 = new Assignment()
            {
                AssignmentID = 111,
                AssignmentDescription = "Test description",
                AssignmentName = "First assignment",
                Priority = Priority.Minor,
                Status = Status.ToDo,
            };

            _myContext.Users.Add(user1);
            _myContext.Users.Add(user2);
            _myContext.Users.Add(user3);
            _myContext.Assignments.Add(assignment1);
            await _myContext.SaveChangesAsync();

            await _driver.LinkUserAssignment(assignment1.AssignmentID, user1.UserID);
            await _driver.LinkUserAssignment(assignment1.AssignmentID, user2.UserID);
            await _driver.LinkUserAssignment(assignment1.AssignmentID, user3.UserID);

            var result = await _myContext.Users.Where(item => item.AssignmentUsers.Any(j => j.AssignmentID == assignment1.AssignmentID))
                                               .ToListAsync();
            result.Count.Should().Be(3);
            result[0].Should().Be(user1);
            result[1].Should().Be(user2);
            result[2].Should().Be(user3);
        }

        // Projects.
        [Test]
        public async Task InsertProject_inserts_new_project()
        {
            var project = new Project()
            {
                ProjectID = 3123,
                ProjectName = "testProject",
                ProjectDescription = "For those who want to try something new",
                CreationDate = DateTime.Now,
                StartDate = DateTime.Now.AddDays(-3),
                EndDate = DateTime.Now.AddDays(10),
            };

            await _driver.InsertProject(project);
            var actualProject = await _myContext.Projects.Where(item => project.ProjectID == project.ProjectID).ToListAsync();

            actualProject.Count.Should().Be(1);
            actualProject[0].ProjectName.Should().Be(project.ProjectName);
            actualProject[0].ProjectDescription.Should().Be(project.ProjectDescription);
            actualProject[0].CreationDate.Should().Be(project.CreationDate);
            actualProject[0].StartDate.Should().Be(project.StartDate);
            actualProject[0].EndDate.Should().Be(project.EndDate);
        }

        [Test]
        public async Task InsertProject_throws_when_inserts_same_record()
        {
            var project1 = new Project()
            {
                ProjectID = 3123,
                ProjectName = "testProject",
                ProjectDescription = "For those who want to try something new",
                CreationDate = DateTime.Now,
                StartDate = DateTime.Now.AddDays(-3),
                EndDate = DateTime.Now.AddDays(10),
            };

            var duplicateProjectId = new Project()
            {
                ProjectID = 3123,
                ProjectName = "different",
                ProjectDescription = "For those who want to try something new",
                CreationDate = DateTime.Now,
                StartDate = DateTime.Now.AddDays(2),
                EndDate = DateTime.Now.AddDays(5),
            };

            await _driver.InsertProject(project1);

            await _driver.Awaiting(s => s.InsertProject(duplicateProjectId)).Should().ThrowAsync<InvalidOperationException>();
        }

        [Test]
        public async Task GetAvailableProjects_returns_list_of_projects()
        {
            var project1 = new Project()
            {
                ProjectID = 3123,
                ProjectName = "testProject",
                ProjectDescription = "For those who want to try something new",
                CreationDate = DateTime.Now,
                StartDate = DateTime.Now.AddDays(-3),
                EndDate = DateTime.Now.AddDays(10),
            };
            var project2 = new Project()
            {
                ProjectID = 1424,
                ProjectName = "testProject2",
                ProjectDescription = "For those ...",
                CreationDate = DateTime.Now,
                StartDate = DateTime.Now.AddDays(-4),
                EndDate = DateTime.Now.AddDays(12),
            };
            var project3 = new Project()
            {
                ProjectID = 12441,
                ProjectName = "testProject3",
                ProjectDescription = "Testing",
                CreationDate = DateTime.Now,
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(12),
            };

            await _driver.InsertProject(project1);
            await _driver.InsertProject(project2);
            await _driver.InsertProject(project3);
            var projects = await _driver.GetAvailableProjects();

            projects.Count.Should().Be(3);
            projects[0].ProjectName.Should().Be(project1.ProjectName);
            projects[0].ProjectDescription.Should().Be(project1.ProjectDescription);
            projects[0].CreationDate.Should().Be(project1.CreationDate);
            projects[0].StartDate.Should().Be(project1.StartDate);
            projects[0].EndDate.Should().Be(project1.EndDate);
            projects[1].ProjectName.Should().Be(project2.ProjectName);
            projects[1].ProjectDescription.Should().Be(project2.ProjectDescription);
            projects[1].CreationDate.Should().Be(project2.CreationDate);
            projects[1].StartDate.Should().Be(project2.StartDate);
            projects[1].EndDate.Should().Be(project2.EndDate);
            projects[2].ProjectName.Should().Be(project3.ProjectName);
            projects[2].ProjectDescription.Should().Be(project3.ProjectDescription);
            projects[2].CreationDate.Should().Be(project3.CreationDate);
            projects[2].StartDate.Should().Be(project3.StartDate);
            projects[2].EndDate.Should().Be(project3.EndDate);
        }

        [Test]
        public async Task UpdateAssignmentProject_updates_assignment_with_new_project()
        {
            var oldProject = new Project()
            {
                ProjectID = 3123,
                ProjectName = "testProject",
                ProjectDescription = "For those who want to try something new",
                CreationDate = DateTime.Now,
                StartDate = DateTime.Now.AddDays(-3),
                EndDate = DateTime.Now.AddDays(10),
            };

            var assignment = new Assignment()
            {
                AssignmentID = 112,
                AssignmentDescription = "Test description 2",
                AssignmentName = "Second assignment",
                Priority = Priority.Major,
                Status = Status.ToDo,
                Project = oldProject,
            };

            var newProject = new Project()
            {
                ProjectID = 1424,
                ProjectName = "testProject123",
                ProjectDescription = "For those ...",
                CreationDate = DateTime.Now,
                StartDate = DateTime.Now.AddDays(-4),
                EndDate = DateTime.Now.AddDays(12),
            };

            await _driver.InsertProject(oldProject);
            await _driver.InsertProject(newProject);
            await _driver.InsertAssignment(assignment);
            var beforeUpdate = (await _myContext.Assignments.FirstOrDefaultAsync(t => t.AssignmentID == assignment.AssignmentID)).ProjectID;
            await _driver.UpdateAssignmentProject(assignment.AssignmentID, newProject.ProjectID);
            var afterUpdate = await _myContext.Assignments.FirstOrDefaultAsync(t => t.AssignmentID == assignment.AssignmentID);

            beforeUpdate.Should().Be(oldProject.ProjectID);
            afterUpdate.ProjectID.Should().Be(newProject.ProjectID);
        }
    }
}