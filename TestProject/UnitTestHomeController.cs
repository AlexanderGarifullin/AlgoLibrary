using AlgoLibrary.Controllers;
using AlgoLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Text;
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
    public class AuthorisationControllerTests
    {
        [Fact]
        public void AuthorisationController_Authorisation_ReturnsView()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var controller = new AuthorisationController(context);

                // Act
                var result = controller.Authorisation("") as ViewResult;

                // Assert
                Assert.NotNull(result);
                Assert.Equal("Authorisation", result.ViewName);
            }
        }

        [Fact]
        public void DeleteSessionParameters_RemovesSessionParameters()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var controller = new AuthorisationController(context);

                var sessionMock = new Mock<ISession>();

                var contextMock = new Mock<HttpContext>();
                contextMock.Setup(c => c.Session).Returns(sessionMock.Object);

                controller.ControllerContext = new ControllerContext
                {
                    HttpContext = contextMock.Object
                };

                // Act
                controller.DeleteSessionParameters();

                // Assert
                sessionMock.Verify(s => s.Set("UserName", It.IsAny<byte[]>()), Times.Once);
                sessionMock.Verify(s => s.Set("UserId", It.IsAny<byte[]>()), Times.Once);
                sessionMock.Verify(s => s.Set("UserRole", Encoding.UTF8.GetBytes(UserRole.User.ToString())), Times.Once);
            }
        }

        [Fact]
        public void Logout_Returns_Redirect_To_Authorisation()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var controller = new AuthorisationController(context);

                var sessionMock = new Mock<ISession>();

                var contextMock = new Mock<HttpContext>();
                contextMock.Setup(c => c.Session).Returns(sessionMock.Object);

                controller.ControllerContext = new ControllerContext
                {
                    HttpContext = contextMock.Object
                };

                // Act
                var result = controller.Logout() as RedirectToActionResult;

                // Assert
                Assert.NotNull(result);
                Assert.Equal("Authorisation", result.ActionName);

                sessionMock.Verify(s => s.Set("UserName", It.IsAny<byte[]>()), Times.Once);
                sessionMock.Verify(s => s.Set("UserId", It.IsAny<byte[]>()), Times.Once);
                sessionMock.Verify(s => s.Set("UserRole", Encoding.UTF8.GetBytes(UserRole.User.ToString())), Times.Once);
            }
        }

        [Fact]
        public void Login_With_Valid_Credentials_Redirects_To_Index()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Создание и заполнение тестовых данных
            using (var context = new AppDbContext(options))
            {
                context.User.Add(new UserModel { UserId = 1, Login = "testuser", Password = Hashing.EncryptPassword("testpassword"), Role = "User" });
                context.SaveChanges();
            }

            var controller = new AuthorisationController(new AppDbContext(options));

            var sessionMock = new Mock<ISession>();

            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(c => c.Session).Returns(sessionMock.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = contextMock.Object
            };

            // Act
            var result = controller.Login("testuser", "testpassword") as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
        }

        [Fact]
        public void Login_With_Invalid_Credentials_Redirects_To_Authorisation_With_Error_Message()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var controller = new AuthorisationController(new AppDbContext(options));

            var sessionMock = new Mock<ISession>();

            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(c => c.Session).Returns(sessionMock.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = contextMock.Object
            };

            // Act
            var result = controller.Login("invaliduser", "invalidpassword") as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Authorisation", result.ActionName);
            Assert.Equal("Ошибка: неверный логин или пароль!", result.RouteValues["msg"]);
        }
    }
}
