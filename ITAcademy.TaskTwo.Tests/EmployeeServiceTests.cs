using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ITAcademy.TaskTwo.Data.Interfaces;
using ITAcademy.TaskTwo.Data.Models;
using ITAcademy.TaskTwo.Logic.Profiles;
using ITAcademy.TaskTwo.Logic.Services;
using Moq;
using Xunit;

namespace ITAcademy.TaskTwo.Tests
{
    public class EmployeeServiceTests
    {
        [Fact]
        public async Task GetEmployeesWithPhones_ReturnsListOfEmployees()
        {
            var mockMap = new Mock<IMapper>();
            var mockRep = new Mock<IEmployeeRepository>();
            var mockUnit = new Mock<IUnitOfWork>();
            mockRep.Setup(repo => repo.GetEmployeesWithPhonesAsync()).ReturnsAsync(GetTestEmployees());            
            mockUnit.Setup(unit => unit.EmployeeRepo).Returns(mockRep.Object);

            var service = new EmployeeService(mockUnit.Object, mockMap.Object);

            // Act
            var result = await service.GetEmployeesWithPhonesAsync();

            // Assert
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async Task GetItemWithAllLists_ReturnsEmployee()
        {
            int testId = 3;                     
            var mockMap = new MapperConfiguration(cfg => cfg.AddProfile(new EmployeeDtoProfile()));
            var mapper = mockMap.CreateMapper();

            var mockRep = new Mock<IEmployeeRepository>();
            mockRep.Setup(repo => repo.GetItemWithAllListsAsync(testId)).ReturnsAsync(
                GetTestEmployees().FirstOrDefault(e => e.Id == testId));
            
            var mockUnit = new Mock<IUnitOfWork>();
            mockUnit.Setup(unit => unit.EmployeeRepo).Returns(mockRep.Object);

            var service = new EmployeeService(mockUnit.Object, mapper);

            // Act
            var result = await service.GetItemWithAllListsAsync(testId);

            // Assert
            Assert.Equal(testId, result.Id);
            Assert.Equal("Иванов", result.SurName);
            Assert.Equal("Иван", result.FirstName);
            Assert.Equal("Иванович", result.SecondName);
            Assert.Equal("ivan@tut.by", result.Email);
            Assert.Contains("+375331450000", result.Phones);
            Assert.Contains("+79167882020", result.Phones);
            Assert.Contains("Математика", result.Assignments);
            Assert.Contains("Директор", result.Appointments);
        }

        [Fact]
        public async Task GetAllItemsWithAllLists_ReturnsAllEmployees()
        {
            var mockMap = new MapperConfiguration(cfg => cfg.AddProfile(new EmployeeDtoProfile()));
            var mapper = mockMap.CreateMapper();

            var mockRep = new Mock<IEmployeeRepository>();
            mockRep.Setup(repo => repo.GetAllItemsWithAllListsAsync()).ReturnsAsync(GetTestEmployees());
            
            var mockUnit = new Mock<IUnitOfWork>();
            mockUnit.Setup(unit => unit.EmployeeRepo).Returns(mockRep.Object);

            var service = new EmployeeService(mockUnit.Object, mapper);

            // Act
            var result = await service.GetAllItemsWithAllListsAsync();

            // Assert
            Assert.Equal(3, result.Count());
        }

        private List<Employee> GetTestEmployees()
        {
            var employees = new List<Employee>()
            {
                new Employee 
                { 
                    Id = 1, 
                    SurName = "Петров", 
                    FirstName = "Петр", 
                    SecondName = "Петрович", 
                    Email = "big_boss@yandex.ru",
                    Assignments = new List<EmployeeSubject>(),
                    Appointments = new List<EmployeePosition>(),
                    Phones = new List<Phone>()
                    {
                        new Phone { Id = 1, Number = "+375291235566", EmployeeId = 1 }
                    }
                    
                },
                new Employee 
                { 
                    Id = 2, 
                    SurName = "Зужик", 
                    FirstName = "Марфа", 
                    SecondName = "Гавриловна", 
                    Email = "zuwik@gmail.com",
                    Assignments = new List<EmployeeSubject>(),
                    Appointments = new List<EmployeePosition>(),
                    Phones = new List<Phone>()
                    {
                        new Phone { Id = 2, Number = "+375441562589", EmployeeId = 2 }
                    }
                },
                new Employee 
                { 
                    Id = 3, 
                    SurName = "Иванов", 
                    FirstName = "Иван", 
                    SecondName = "Иванович", 
                    Email = "ivan@tut.by",
                    Assignments = new List<EmployeeSubject>()
                    {
                        new EmployeeSubject
                        {
                            EmployeeId = 3,
                            SubjectId = 3,
                            Subject = new Subject { Id = 3, Name = "Математика" }
                        },
                    },
                    Appointments = new List<EmployeePosition>()
                    {
                        new EmployeePosition
                        {
                            EmployeeId = 3,
                            PositionId = 1,
                            Position = new Position { Id = 1, Name = "Директор", MaxNumber = 1 }
                        },
                    },
                    Phones = new List<Phone>()
                    {
                        new Phone { Id = 3, Number = "+375331450000", EmployeeId = 3 },
                        new Phone { Id = 4, Number = "+79167882020", EmployeeId = 3 }
                    }
                }
            };
            return employees;
        }
    }
}