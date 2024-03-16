using AlgoLibrary.Controllers;
using AlgoLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AlgoLibrary.Tests.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public void HomeController_Index_ReturnsView()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var controller = new HomeController(context);

                // Act
                var result = controller.Index() as ViewResult;

                // Assert
                Assert.NotNull(result);
                Assert.Equal("Index", result.ViewName);
            }
        }
        [Fact]
        public void HomeController_Privacy_ReturnsView()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var controller = new HomeController(context);

                // Act
                var result = controller.Privacy() as ViewResult;

                // Assert
                Assert.NotNull(result);
                Assert.Equal("Privacy", result.ViewName);
            }
        }
    }

    public class PlanControllerTests
    {
        [Fact]
        public void PlanController_Plan_ReturnsView()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var controller = new PlanController(context);

                // Act
                var result = controller.Plan() as ViewResult;

                // Assert
                Assert.NotNull(result);
                Assert.Equal("Plan", result.ViewName);
            }
        }
    }
}
