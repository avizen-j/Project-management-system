using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NUnit.Framework;
using project_management_system.Context;
using project_management_system.Services;
using System;
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

        [Test]
        public async Task InsertAssignment_inserts_assignment_to_db()
        {
            var expectedAssignment = new Assignment()
            {
                AssignmentID = 111,
                AssignmentDescription = "Test description",
                AssignmentName = "First assignment",
                Priority = "Minor",
                Status = "InProgress",
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
                Priority = "Minor",
                Status = "InProgress",
            };

            _myContext.Assignments.Add(expectedAssignment);
            await _myContext.SaveChangesAsync();
            await _driver.DeleteAssignment(expectedAssignment.AssignmentID);

            var result = await _myContext.Assignments.FirstOrDefaultAsync(r => r.AssignmentID == expectedAssignment.AssignmentID);
            result.Should().Be(default);
        }

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
                Priority = "Minor",
                Status = "ToDo",
            };
            var assignment2 = new Assignment()
            {
                AssignmentID = 112,
                AssignmentDescription = "Test description 2",
                AssignmentName = "Second assignment",
                Priority = "Major",
                Status = "ToDo",
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
                Priority = "Minor",
                Status = "ToDo",
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
    }
}