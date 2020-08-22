using AutoMapper;
using ITAcademy.TaskTwo.Data.Interfaces;
using ITAcademy.TaskTwo.Data.Models;
using ITAcademy.TaskTwo.Logic.Models.PositionDTO;
using ITAcademy.TaskTwo.Logic.Profiles;
using ITAcademy.TaskTwo.Logic.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ITAcademy.TaskTwo.Tests
{
    public class PositionServiceTests
    {
        [Theory]
        [InlineData("Вахтер")]
        [InlineData("вахтер")]
        [InlineData("Директор")]
        [InlineData("Электрик")]
        public void ExistsName_ReturnsTrueResult(string testName)
        {
            var mockMap = new Mock<IMapper>();

            var mockRep = new Mock<IPositionRepository>();
            mockRep.Setup(repo => repo.ExistsName(testName)).Returns(
                GetTestPositions().Any(p => p.Name.Equals(testName, StringComparison.OrdinalIgnoreCase)));
            var mockUnit = new Mock<IUnitOfWork>();
            mockUnit.Setup(unit => unit.PositionRepo).Returns(mockRep.Object);

            var service = new PositionService(mockUnit.Object, mockMap.Object);

            // Act
            var result = service.ExistsName(testName);

            // Assert
            Assert.IsType<bool>(result);
            Assert.True(result);
        }

        [Theory]
        [InlineData("Вахтерша")]
        [InlineData("Директ")]
        [InlineData("Илектрик")]
        public void ExistsName_ReturnsFalseResult(string testName)
        {
            var mockMap = new Mock<IMapper>();

            var mockRep = new Mock<IPositionRepository>();
            mockRep.Setup(repo => repo.ExistsName(testName)).Returns(
                GetTestPositions().Any(p => p.Name == testName));
            var mockUnit = new Mock<IUnitOfWork>();
            mockUnit.Setup(unit => unit.PositionRepo).Returns(mockRep.Object);

            var service = new PositionService(mockUnit.Object, mockMap.Object);

            // Act
            var result = service.ExistsName(testName);

            // Assert
            Assert.IsType<bool>(result);
            Assert.False(result);
        }

        [Fact]
        public async Task GetEmployeesOfPosition_ReturnsPositionWithEmployees()
        {
            var testId = 1;
            var model = GetTestPositions().FirstOrDefault(p => p.Id == testId);
            var mockMap = new MapperConfiguration(cfg => cfg.AddProfiles(
                new List<Profile>() { new EmployeeDtoProfile(), new PositionDtoProfile() }));
            var mapper = mockMap.CreateMapper();

            var mockRep = new Mock<IEmployeeRepository>();
            mockRep.Setup(repo => repo.GetAllAsync()).ReturnsAsync(GetTestEmployees());
            var mockUnit = new Mock<IUnitOfWork>();
            mockUnit.Setup(unit => unit.EmployeeRepo).Returns(mockRep.Object);

            var service = new PositionService(mockUnit.Object, mapper);

            // Act
            var result = await service.GetEmployeesOfPositionAsync(model);

            // Assert
            Assert.IsType<PositionWithEmployees>(result);
            Assert.Equal(testId, result.Id);
            Assert.Equal("Вахтер", result.Name);
            Assert.Equal(3, result.MaxNumber);
        }

        [Fact]
        public async Task GetAllPositionsWithEmployees_ReturnsPositionsWithEmployees()
        {
            var mockMap = new MapperConfiguration(cfg => cfg.AddProfiles(
                new List<Profile>() { new EmployeeDtoProfile(), new PositionDtoProfile() }));
            var mapper = mockMap.CreateMapper();

            var mockRep = new Mock<IPositionRepository>();
            mockRep.Setup(repo => repo.GetAllPositionsWithEmployeesAsync()).ReturnsAsync(
                GetTestPositions());
            var mockUnit = new Mock<IUnitOfWork>();
            mockUnit.Setup(unit => unit.PositionRepo).Returns(mockRep.Object);

            var service = new PositionService(mockUnit.Object, mapper);

            // Act
            var result = await service.GetAllPositionsWithEmployeesAsync();

            // Assert
            Assert.Equal(3, result.Count());
        }

        [Theory]
        [InlineData("Вахтер")]
        [InlineData("Директор")]
        [InlineData("Электрик")]
        public async Task GetPositionWithEmployees_ReturnsCorrectObjectTypeAndNameProperty(string testName)
        {
            var mockMap = new MapperConfiguration(cfg => cfg.AddProfiles(
                new List<Profile>() { new EmployeeDtoProfile(), new PositionDtoProfile() }));
            var mapper = mockMap.CreateMapper();

            var mockRep = new Mock<IPositionRepository>();
            mockRep.Setup(repo => repo.GetPositionWithEmployeesAsync(testName)).ReturnsAsync(
                GetTestPositions().FirstOrDefault(p => p.Name == testName));
           
            var mockUnit = new Mock<IUnitOfWork>();
            mockUnit.Setup(unit => unit.PositionRepo).Returns(mockRep.Object);

            var service = new PositionService(mockUnit.Object, mapper);

            // Act
            var result = await service.GetPositionWithEmployeesAsync(testName);

            // Assert
            Assert.IsType<PositionWithEmployeesDto>(result);
            Assert.Equal(testName, result.Name);
        }

        [Fact]
        public async Task GetPositionWithEmployees_ReturnsPositionWithEmployees()
        {
            var testName = "Вахтер";
            var mockMap = new MapperConfiguration(cfg => cfg.AddProfiles(
                new List<Profile>() { new EmployeeDtoProfile(), new PositionDtoProfile() }));
            var mapper = mockMap.CreateMapper();

            var mockRep = new Mock<IPositionRepository>();
            mockRep.Setup(repo => repo.GetPositionWithEmployeesAsync(testName)).ReturnsAsync(
                GetTestPositions().FirstOrDefault(p => p.Name == testName));
            var mockUnit = new Mock<IUnitOfWork>();
            mockUnit.Setup(unit => unit.PositionRepo).Returns(mockRep.Object);

            var service = new PositionService(mockUnit.Object, mapper);

            // Act
            var result = await service.GetPositionWithEmployeesAsync(testName);

            // Assert            
            Assert.Equal(1, result.NumberOfVacancies);
            Assert.Equal("Иванов", result.Employees.FirstOrDefault(aed => aed.Id == 2).SurName);
            Assert.Equal("Иван", result.Employees.FirstOrDefault(aed => aed.Id == 2).FirstName);
            Assert.Equal("Иванович", result.Employees.FirstOrDefault(aed => aed.Id == 2).SecondName);
            Assert.Equal("ivan@yandex.ru", result.Employees.FirstOrDefault(aed => aed.Id == 2).Email);
            Assert.Contains("+375331450000", result.Employees.FirstOrDefault(aed => aed.Id == 2).Phones);
            Assert.Contains("+79167882020", result.Employees.FirstOrDefault(aed => aed.Id == 2).Phones);
            Assert.Contains("Русский язык", result.Employees.FirstOrDefault(aed => aed.Id == 2).Subjects);
            Assert.Equal("Зужик", result.Employees.FirstOrDefault(aed => aed.Id == 3).SurName);
            Assert.Equal("Марфа", result.Employees.FirstOrDefault(ep => ep.Id == 3).FirstName);
            Assert.Equal("Гавриловна", result.Employees.FirstOrDefault(ep => ep.Id == 3).SecondName);
            Assert.Equal("zuwik@gmail.com", result.Employees.FirstOrDefault(ep => ep.Id == 3).Email);
            Assert.Contains("+375291234567", result.Employees.FirstOrDefault(aed => aed.Id == 3).Phones);
            Assert.Contains("Трудовое обучение", result.Employees.FirstOrDefault(aed => aed.Id == 3).Subjects);
            Assert.Contains("Физкультура", result.Employees.FirstOrDefault(aed => aed.Id == 3).Subjects);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task GetDetails_ReturnsCorrectObjectTypeAndIdProperty(int testId)
        {
            var mockMap = new Mock<IMapper>();
            var mockRep = new Mock<IPositionRepository>();
            mockRep.Setup(repo => repo.GetDetailsAsync(testId)).ReturnsAsync(
                GetTestPositions().FirstOrDefault(p => p.Id == testId));
            var mockUnit = new Mock<IUnitOfWork>();
            mockUnit.Setup(unit => unit.PositionRepo).Returns(mockRep.Object);

            var service = new PositionService(mockUnit.Object, mockMap.Object);

            // Act
            var result = await service.GetDetailsAsync(testId);

            // Assert
            Assert.IsType<Position>(result);
            Assert.Equal(testId, result.Id);
        }

        [Fact]
        public async Task GetDetails_ReturnsPositionWithEmployees()
        {
            var testId = 1;
            var mockMap = new Mock<IMapper>();
            var mockRep = new Mock<IPositionRepository>();
            mockRep.Setup(repo => repo.GetDetailsAsync(testId)).ReturnsAsync(
                GetTestPositions().FirstOrDefault(p => p.Id == testId));
            var mockUnit = new Mock<IUnitOfWork>();
            mockUnit.Setup(unit => unit.PositionRepo).Returns(mockRep.Object);

            var service = new PositionService(mockUnit.Object, mockMap.Object);

            // Act
            var result = await service.GetDetailsAsync(testId);

            // Assert
            Assert.IsType<Position>(result);
            Assert.Equal(testId, result.Id);
            Assert.Equal("Вахтер", result.Name);
            Assert.Equal(3, result.MaxNumber);
            Assert.Equal("Иванов", result.Appointments.FirstOrDefault(ep => ep.EmployeeId == 2).Employee.SurName);
            Assert.Equal("Иван", result.Appointments.FirstOrDefault(ep => ep.EmployeeId == 2).Employee.FirstName);
            Assert.Equal("Иванович", result.Appointments.FirstOrDefault(ep => ep.EmployeeId == 2).Employee.SecondName);
            Assert.Equal("ivan@yandex.ru", result.Appointments.FirstOrDefault(ep => ep.EmployeeId == 2).Employee.Email);
            Assert.Equal("Зужик", result.Appointments.FirstOrDefault(ep => ep.EmployeeId == 3).Employee.SurName);
            Assert.Equal("Марфа", result.Appointments.FirstOrDefault(ep => ep.EmployeeId == 3).Employee.FirstName);
            Assert.Equal("Гавриловна", result.Appointments.FirstOrDefault(ep => ep.EmployeeId == 3).Employee.SecondName);
            Assert.Equal("zuwik@gmail.com", result.Appointments.FirstOrDefault(ep => ep.EmployeeId == 3).Employee.Email);
        }

        private List<Position> GetTestPositions()
        {
            var positions = new List<Position>()
            {
                new Position
                {
                    Id = 1,
                    Name = "Вахтер",
                    MaxNumber = 3,
                    Appointments = new List<EmployeePosition>()
                    {
                        new EmployeePosition { PositionId = 1, EmployeeId = 2, Employee = new Employee
                        {
                            Id = 2,
                            SurName = "Иванов",
                            FirstName = "Иван",
                            SecondName = "Иванович",
                            Email = "ivan@yandex.ru",
                            Phones = new List<Phone>()
                            {
                                new Phone { Id = 1, Number = "+375331450000", EmployeeId = 2 },
                                new Phone { Id = 2, Number = "+79167882020", EmployeeId = 2 }
                            },
                            Assignments = new List<EmployeeSubject>()
                            {
                                new EmployeeSubject { EmployeeId = 2, SubjectId = 1, Subject = new Subject()
                                {
                                    Id = 1, Name = "Русский язык"
                                }}
                            }
                        }},
                        new EmployeePosition { PositionId = 1, EmployeeId = 3, Employee = new Employee
                        {
                            Id = 3,
                            SurName = "Зужик",
                            FirstName = "Марфа",
                            SecondName = "Гавриловна",
                            Email = "zuwik@gmail.com",
                            Phones = new List<Phone>()
                            {
                                new Phone { Id = 3, Number = "+375291234567", EmployeeId = 3 }
                            },
                            Assignments = new List<EmployeeSubject>()
                            {
                                new EmployeeSubject { EmployeeId = 3, SubjectId = 2, Subject = new Subject()
                                {
                                    Id = 2, Name = "Трудовое обучение"
                                }},
                                new EmployeeSubject { EmployeeId = 3, SubjectId = 3, Subject = new Subject()
                                {
                                    Id = 3, Name = "Физкультура"
                                }}
                            }
                        }}
                    }
                },
                new Position
                {
                    Id = 2,
                    Name = "Директор",
                    MaxNumber = 1,
                    Appointments = new List<EmployeePosition>()
                },
                new Position
                {
                    Id = 3,
                    Name = "Электрик",
                    MaxNumber = 1,
                    Appointments = new List<EmployeePosition>()
                }
            };
            return positions;
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
                },
                new Employee
                {
                    Id = 2,
                    SurName = "Иванов",
                    FirstName = "Иван",
                    SecondName = "Иванович",
                    Email = "ivan@tut.by",
                },
                new Employee
                {
                    Id = 3,
                    SurName = "Зужик",
                    FirstName = "Марфа",
                    SecondName = "Гавриловна",
                    Email = "zuwik@gmail.com",
                },                
            };
            return employees;
        }
    }
}