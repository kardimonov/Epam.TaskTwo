using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ITAcademy.TaskTwo.Data.Models;
using ITAcademy.TaskTwo.Logic.Interfaces;
using ITAcademy.TaskTwo.Logic.Models.SubjectDTO;
using ITAcademy.TaskTwo.Logic.Profiles;
using ITAcademy.TaskTwo.Web.Controllers;
using ITAcademy.TaskTwo.Web.Profiles;
using ITAcademy.TaskTwo.Web.ViewModels.SubjectVM;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ITAcademy.TaskTwo.Tests
{
    public class SubjectControllerTests
    {
        [Fact]
        public void VerifyName_ViewResultsNotNull()
        {
            var testName = "Химия";
            var mockDeco = new Mock<ISubjectDecorator>();
            var mockServ = new Mock<ISubjectService>();
            var mockMap = new Mock<IMapper>();
            mockServ.Setup(serv => serv.ExistsName(testName)).Returns(
                GetTestSubjects().Any(s => s.Name == testName));

            var controller = new SubjectController(mockMap.Object, mockDeco.Object, mockServ.Object);

            // Act
            var result = controller.VerifyName(testName) as JsonResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Search_ViewResultsNotNull()
        {
            var testString = "Химия";
            var mockDeco = new Mock<ISubjectDecorator>();
            var mockServ = new Mock<ISubjectService>();
            var mockMap = new MapperConfiguration(cfg => cfg.AddProfile(new SubjectProfile()));
            var mapper = mockMap.CreateMapper();

            mockDeco.Setup(deco => deco.GetAllAsync()).ReturnsAsync(GetTestSubjects());

            var controller = new SubjectController(mapper, mockDeco.Object, mockServ.Object);

            // Act
            var result = await controller.Search(testString) as JsonResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void EditAssignedEmployees_ReturnsModelWhenModelStateIsInvalid()
        {
            var mockDeco = new Mock<ISubjectDecorator>();
            var mockServ = new Mock<ISubjectService>();
            var mockMap = new MapperConfiguration(cfg => cfg.AddProfile(new SubjectProfile()));
            var mapper = mockMap.CreateMapper();

            var controller = new SubjectController(mapper, mockDeco.Object, mockServ.Object);
            controller.ModelState.AddModelError("Name", "Required");
            var updatedSubject = new SubjectWithEmployees() { Id = 1, Name = "Химия" };

            // Act
            var result = controller.EditAssignedEmployees(updatedSubject);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(updatedSubject, viewResult?.Model);
        }

        [Fact]
        public void EditAssignedEmployees_ReturnsRedirection()
        {
            var mockDeco = new Mock<ISubjectDecorator>();
            var mockServ = new Mock<ISubjectService>();
            var mockMap = new MapperConfiguration(cfg => cfg.AddProfile(new SubjectProfile()));
            var mapper = mockMap.CreateMapper();

            var controller = new SubjectController(mapper, mockDeco.Object, mockServ.Object);
            var updatedSubject = new SubjectWithEmployees() { Id = 1, Name = "Химия" };

            // Act
            var result = controller.EditAssignedEmployees(updatedSubject);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
                
        [Fact]
        public async Task EditAssignedEmployees_ReturnsNotFoundWhenSubjectNotFound()
        {
            var testId = 1;
            var mockDeco = new Mock<ISubjectDecorator>();
            var mockServ = new Mock<ISubjectService>();
            var mockMap = new Mock<IMapper>();

            mockDeco.Setup(deco => deco.GetAsync(testId)).ReturnsAsync((Subject)null);
            var controller = new SubjectController(mockMap.Object, mockDeco.Object, mockServ.Object);

            // Act
            var result = await controller.EditAssignedEmployees(testId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNotFoundWhenIdIsNull()
        {
            var mockDeco = new Mock<ISubjectDecorator>();
            var mockServ = new Mock<ISubjectService>();
            var mockMap = new Mock<IMapper>();
            var controller = new SubjectController(mockMap.Object, mockDeco.Object, mockServ.Object);

            // Act
            var result = await controller.Delete(null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsRedirection()
        {
            var testId = 2;
            var mockDeco = new Mock<ISubjectDecorator>();
            var mockServ = new Mock<ISubjectService>();
            var mockMap = new Mock<IMapper>();

            var controller = new SubjectController(mockMap.Object, mockDeco.Object, mockServ.Object);

            // Act
            var result = await controller.Delete(testId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
        
        [Fact]
        public async Task ConfirmDelete_ReturnsNotFoundWhenIdIsNull()
        {
            var mockDeco = new Mock<ISubjectDecorator>();
            var mockServ = new Mock<ISubjectService>();
            var mockMap = new Mock<IMapper>();
            var controller = new SubjectController(mockMap.Object, mockDeco.Object, mockServ.Object);

            // Act
            var result = await controller.ConfirmDelete(null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task ConfirmDelete_ReturnsNotFoundWhenUserNotFound()
        {
            var testId = 1;
            var mockDeco = new Mock<ISubjectDecorator>();
            var mockServ = new Mock<ISubjectService>();
            var mockMap = new Mock<IMapper>();

            mockDeco.Setup(deco => deco.GetAsync(testId)).ReturnsAsync((Subject)null);
            var controller = new SubjectController(mockMap.Object, mockDeco.Object, mockServ.Object);

            // Act
            var result = await controller.ConfirmDelete(testId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task ConfirmDelete_ReturnsViewResultWithSubject()
        {
            int testId = 2;
            var mockDeco = new Mock<ISubjectDecorator>();
            var mockServ = new Mock<ISubjectService>();
            var mockMap = new MapperConfiguration(cfg => cfg.AddProfile(new SubjectProfile()));
            var mapper = mockMap.CreateMapper();

            mockDeco.Setup(deco => deco.GetAsync(testId)).ReturnsAsync(
                GetTestSubjects().FirstOrDefault(s => s.Id == testId));
            var controller = new SubjectController(mapper, mockDeco.Object, mockServ.Object);

            // Act
            var result = await controller.ConfirmDelete(testId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<SubjectDelete>(viewResult.ViewData.Model);
            Assert.Equal("Химия", model.Name);
            Assert.Equal(testId, model.Id);
        }

        [Fact]
        public void Edit_ReturnsModelWhenModelStateIsInvalid()
        {
            var mockDeco = new Mock<ISubjectDecorator>();
            var mockServ = new Mock<ISubjectService>();
            var mockMap = new MapperConfiguration(cfg => cfg.AddProfile(new SubjectProfile()));
            var mapper = mockMap.CreateMapper();

            var controller = new SubjectController(mapper, mockDeco.Object, mockServ.Object);
            controller.ModelState.AddModelError("Name", "Required");
            var updatedSubject = new SubjectEdit() { Id = 1, Name = "Химия" };

            // Act
            var result = controller.Edit(updatedSubject);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(updatedSubject, viewResult?.Model);
        }

        [Fact]
        public void Edit_ReturnsRedirection()
        {
            var mockDeco = new Mock<ISubjectDecorator>();
            var mockServ = new Mock<ISubjectService>();
            var mockMap = new MapperConfiguration(cfg => cfg.AddProfile(new SubjectProfile()));
            var mapper = mockMap.CreateMapper();

            var controller = new SubjectController(mapper, mockDeco.Object, mockServ.Object);
            var updatedSubject = new SubjectEdit() { Id = 1, Name = "Химия" };

            // Act
            var result = controller.Edit(updatedSubject);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Edit_ReturnsViewResultWithSubject()
        {
            int testId = 2;
            var mockDeco = new Mock<ISubjectDecorator>();
            var mockServ = new Mock<ISubjectService>();
            var mockMap = new MapperConfiguration(cfg => cfg.AddProfile(new SubjectProfile()));
            var mapper = mockMap.CreateMapper();

            mockDeco.Setup(deco => deco.GetAsync(testId)).ReturnsAsync(
                GetTestSubjects().FirstOrDefault(s => s.Id == testId));
            var controller = new SubjectController(mapper, mockDeco.Object, mockServ.Object);

            // Act
            var result = await controller.Edit(testId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<SubjectEdit>(viewResult.ViewData.Model);
            Assert.Equal("Химия", model.Name);
            Assert.Equal(testId, model.Id);
        }

        [Fact]
        public async Task Edit_ReturnsNotFoundWhenSubjectNotFound()
        {
            var testId = 1;
            var mockDeco = new Mock<ISubjectDecorator>();
            var mockServ = new Mock<ISubjectService>();
            var mockMap = new Mock<IMapper>();

            mockDeco.Setup(deco => deco.GetAsync(testId)).ReturnsAsync((Subject)null);
            var controller = new SubjectController(mockMap.Object, mockDeco.Object, mockServ.Object);

            // Act
            var result = await controller.Edit(testId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsViewResultWithSubject()
        {
            int testId = 2;
            var mockDeco = new Mock<ISubjectDecorator>();
            var mockServ = new Mock<ISubjectService>();
            var mockMap = new MapperConfiguration(cfg => cfg.AddProfile(new SubjectProfile()));
            var mapper = mockMap.CreateMapper();

            mockServ.Setup(serv => serv.GetDetailsAsync(testId)).ReturnsAsync(
                GetTestSubjects().FirstOrDefault(s => s.Id == testId));
            var controller = new SubjectController(mapper, mockDeco.Object, mockServ.Object);

            // Act
            var result = await controller.Details(testId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<SubjectDetails>(viewResult.ViewData.Model);
            Assert.Equal("Химия", model.Name);
            Assert.Equal(testId, model.Id);
        }

        [Fact]
        public async Task Details_ReturnsNotFoundWhenSubjectNotFound()
        {
            var testId = 1;
            var mockDeco = new Mock<ISubjectDecorator>();
            var mockServ = new Mock<ISubjectService>();
            var mockMap = new Mock<IMapper>();

            mockServ.Setup(serv => serv.GetDetailsAsync(testId)).ReturnsAsync((Subject)null);
            var controller = new SubjectController(mockMap.Object, mockDeco.Object, mockServ.Object);

            // Act
            var result = await controller.Details(testId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsNotFoundWhenIdIsNull()
        {
            var mockDeco = new Mock<ISubjectDecorator>();
            var mockServ = new Mock<ISubjectService>();
            var mockMap = new Mock<IMapper>();
            var controller = new SubjectController(mockMap.Object, mockDeco.Object, mockServ.Object);

            // Act
            var result = await controller.Details(null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsModelWhenModelStateIsInvalid()
        {
            var mockDeco = new Mock<ISubjectDecorator>();
            var mockServ = new Mock<ISubjectService>();
            var mockMap = new MapperConfiguration(cfg => cfg.AddProfile(new SubjectProfile()));
            var mapper = mockMap.CreateMapper();

            var controller = new SubjectController(mapper, mockDeco.Object, mockServ.Object);
            controller.ModelState.AddModelError("Name", "Required");
            var newSubject = new SubjectCreate();

            // Act
            var result = await controller.Create(newSubject);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(newSubject, viewResult?.Model);
        }

        [Fact]
        public async Task Create_ReturnsRedirection()
        {
            var mockDeco = new Mock<ISubjectDecorator>();
            var mockServ = new Mock<ISubjectService>();
            var mockMap = new MapperConfiguration(cfg => cfg.AddProfile(new SubjectProfile()));
            var mapper = mockMap.CreateMapper();

            var controller = new SubjectController(mapper, mockDeco.Object, mockServ.Object);
            var newSubject = new SubjectCreate() { Name = "Химия" };

            // Act
            var result = await controller.Create(newSubject);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void Create_ViewResultsNotNull()
        {
            var mockDeco = new Mock<ISubjectDecorator>();
            var mockServ = new Mock<ISubjectService>();
            var mockMap = new Mock<IMapper>();
            var controller = new SubjectController(mockMap.Object, mockDeco.Object, mockServ.Object);

            // Act
            var result = controller.Create() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Index_ReturnsViewResultWithList()
        {
            var mockDeco = new Mock<ISubjectDecorator>();
            var mockServ = new Mock<ISubjectService>();
            var mockMap = new MapperConfiguration(cfg => cfg.AddProfile(new SubjectProfile()));
            var mapper = mockMap.CreateMapper();

            mockDeco.Setup(deco => deco.GetAllAsync()).ReturnsAsync(GetTestSubjects());
            var controller = new SubjectController(mapper, mockDeco.Object, mockServ.Object);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<SubjectIndex>>(viewResult.ViewData.Model);
            Assert.Equal(4, model.Count());
        }

        private List<Subject> GetTestSubjects()
        {
            var subjects = new List<Subject>()
            {
                new Subject { Id = 1, Name = "Физика" },
                new Subject { Id = 2, Name = "Химия" },
                new Subject { Id = 3, Name = "Математика" },
                new Subject { Id = 4, Name = "Русский язык" }
            };
            return subjects;
        }
    }
}