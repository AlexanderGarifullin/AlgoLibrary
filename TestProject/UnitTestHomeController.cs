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
    }
}
