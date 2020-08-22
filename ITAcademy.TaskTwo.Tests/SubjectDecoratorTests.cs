using AutoMapper;
using ITAcademy.TaskTwo.Data.Interfaces;
using ITAcademy.TaskTwo.Data.Models;
using ITAcademy.TaskTwo.Logic.Decorators;
using ITAcademy.TaskTwo.Logic.Hubs;
using Microsoft.AspNetCore.SignalR;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ITAcademy.TaskTwo.Tests
{
    public class SubjectDecoratorTests
    {
        [Fact]
        public async Task GetAsync_ReturnsCorrectSubject()
        {
            int testId = 2;
            var mockDb = new Mock<IApplicationContext>();
            var mockRep = new Mock<ISubjectRepository>();
            var mockHub = new Mock<IHubContext<SignalHub>>();
            var mockUnit = new Mock<IUnitOfWork>();
            var mockMap = new Mock<IMapper>();

            mockRep.Setup(repo => repo.GetAsync(testId)).ReturnsAsync(
                GetTestSubjects().FirstOrDefault(s => s.Id == testId));
            var decor = new SubjectDecorator(
                mockDb.Object, mockRep.Object, mockHub.Object, mockUnit.Object, mockMap.Object);

            // Act
            var result = await decor.GetAsync(testId);

            // Assert
            Assert.Equal("Химия", result.Name);
            Assert.Equal(testId, result.Id);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAListOfSubjects()
        {
            var mockDb = new Mock<IApplicationContext>();
            var mockRep = new Mock<ISubjectRepository>();
            var mockHub = new Mock<IHubContext<SignalHub>>();
            var mockUnit = new Mock<IUnitOfWork>();
            var mockMap = new Mock<IMapper>();

            mockRep.Setup(repo => repo.GetAllAsync()).ReturnsAsync(GetTestSubjects());
            var decor = new SubjectDecorator(
                mockDb.Object, mockRep.Object, mockHub.Object, mockUnit.Object, mockMap.Object);

            // Act
            var result = await decor.GetAllAsync();

            // Assert
            Assert.Equal(4, result.Count());
        }

        private List<Subject> GetTestSubjects()
        {
            var subjects = new List<Subject>
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