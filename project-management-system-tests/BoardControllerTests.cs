using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using project_management_system.Controllers;
using project_management_system.Interfaces;
using project_management_system.Models;
using project_management_system.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace project_management_system_tests
{
    public class BoardControllerTests
    {
        private BoardController _controller;
        private IDatabaseDriver _driver;

        [SetUp]
        public void SetUp()
        {
            _driver = Substitute.For<IDatabaseDriver>();
            var logger = Substitute.For<ILogger<BoardController>>();
            _controller = new BoardController(logger, _driver);
        }

        [Test]
        public async Task Index_returns_index_with_task_model()
        {
            var result = _controller.Index();
            var viewResult = result as ViewResult;

            var okResult = result.Should().BeOfType<ViewResult>().Subject;
            var modelResult = okResult.Model.Should().BeAssignableTo<TaskModel>().Subject;
            viewResult.ViewName.Should().Be("../Home/Board");
        }

        [Test]
        public async Task ProgressAnalysis_returns_progress_analysis_with_project_model()
        {
            _driver.GetProjectById(default).ReturnsForAnyArgs(new project_management_system.Context.Project());

            var result = await _controller.ProgressAnalysis(123);
            var viewResult = result as ViewResult;

            var okResult = result.Should().BeOfType<ViewResult>().Subject;
            var modelResult = okResult.Model.Should().BeAssignableTo<ProjectModel>().Subject;
            viewResult.ViewName.Should().Be("../Home/ProgressAnalysis");
        }

        [Test]
        public async Task UpdateAssignmentStartDate_redirects_to_index_action()
        {
            var result = await _controller.UpdateAssignmentStartDate(123, DateTime.Now) as IActionResult;

            var redirectToActionResult = result.Should().BeOfType<RedirectToActionResult>();
            redirectToActionResult.Subject.ActionName.Should().Be("Index");
        }

        [Test]
        public async Task UpdateAssignmentStartDate_returns_error_view()
        {
            _driver.UpdateAssignmentStartDate(default, default).ThrowsForAnyArgs(new Exception("TestException message"));

            var result = await _controller.UpdateAssignmentStartDate(123, DateTime.Now);
            var viewResult = result as ViewResult;


            var okResult = result.Should().BeOfType<ViewResult>().Subject;
            var modelResult = okResult.Model.Should().BeAssignableTo<ErrorViewModel>().Subject;
            viewResult.ViewName.Should().Be("../Shared/Error");
        }
    }
}
