using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Todo.Core.Entities;
using Todo.Models.Todos;
using Todo.Repository.Repository.Abstract;
using Todo.Service.Concretes;
using Todo.Service.Rules;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Todo.Tests
{
    [TestFixture]
    public class TodoServiceTests
    {
        private Mock<ITodoRepository> _mockRepository;
        private Mock<IMapper> _mockMapper;
        private Mock<TodoBusinessRules> _mockBusinessRules;
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private TodoService _todoService;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<ITodoRepository>();
            _mockMapper = new Mock<IMapper>();

            // Mock ITodoRepository to pass to TodoBusinessRules
            var mockTodoRepository = new Mock<ITodoRepository>();
            _mockBusinessRules = new Mock<TodoBusinessRules>(mockTodoRepository.Object);

            // IHttpContextAccessor mock'u ve userId ayarlarý
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            // Test için userId'yi içeren bir ClaimsPrincipal oluþturuyoruz
            var userId = "test-user-id";
            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, userId) };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext { User = claimsPrincipal };
            _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

            // Mock'larý kullanarak TodoService'i oluþturuyoruz
            _todoService = new TodoService(_mockRepository.Object, _mockMapper.Object, _mockBusinessRules.Object, _httpContextAccessorMock.Object);
        }

        [Test]
        public async Task Add_ShouldReturnSuccess_WhenTodoIsAdded()
        {
            // Arrange
            var dto = new CreateTodoRequestDto("Test Todo", "Test Description", 1, "");
            var userId = "test-user-id";
            var todoEntity = new Todo.Models.Entities.Todo { Id = Guid.NewGuid(), Title = dto.Title, Description = dto.Description, UserId = userId };
            var todoResponse = new TodoResponseDto { id = todoEntity.Id, Title = todoEntity.Title, Description = todoEntity.Description };

            _mockMapper.Setup(m => m.Map<Todo.Models.Entities.Todo>(dto)).Returns(todoEntity);
            _mockRepository.Setup(r => r.Add(It.IsAny<Todo.Models.Entities.Todo>())).Returns(todoEntity);
            _mockMapper.Setup(m => m.Map<TodoResponseDto>(todoEntity)).Returns(todoResponse);

            // Act
            var result = await _todoService.Add(dto, userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.Status);
            Assert.IsTrue(result.Success);
            Assert.AreEqual("Test Todo", result.Data.Title);
            Assert.AreEqual("Test Description", result.Data.Description);
        }

        [Test]
        public void Delete_ShouldReturnSuccess_WhenTodoIsDeleted()
        {
            // Arrange
            var todoId = Guid.NewGuid();
            var todoEntity = new Todo.Models.Entities.Todo { Id = todoId, Title = "Test Todo" };

            _mockBusinessRules.Setup(br => br.TodoIsPresent(todoId)).Verifiable();
            _mockRepository.Setup(r => r.GetById(todoId)).Returns(todoEntity);
            _mockRepository.Setup(r => r.Delete(It.IsAny<Todo.Models.Entities.Todo>())).Returns(todoEntity);

            // Act
            var result = _todoService.Delete(todoId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(204, result.Status);
            Assert.IsTrue(result.Success);
            Assert.AreEqual($"Todo Baþlýðý : {todoEntity.Title}", result.Data);
        }

        [Test]
        public void GetAll_ShouldReturnAllTodos()
        {
            // Arrange
            var todos = new List<Todo.Models.Entities.Todo>
            {
                new Todo.Models.Entities.Todo { Id = Guid.NewGuid(), Title = "Todo 1" },
                new Todo.Models.Entities.Todo { Id = Guid.NewGuid(), Title = "Todo 2" }
            };
            var responseDtos = new List<TodoResponseDto>
            {
                new TodoResponseDto { id = todos[0].Id, Title = todos[0].Title },
                new TodoResponseDto { id = todos[1].Id, Title = todos[1].Title }
            };

            _mockRepository.Setup(r => r.GetAll(It.IsAny<Expression<Func<Todo.Models.Entities.Todo, bool>>>()))
                           .Returns(todos);
            _mockMapper.Setup(m => m.Map<List<TodoResponseDto>>(todos)).Returns(responseDtos);

            // Act
            var result = _todoService.GetAll();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.AreEqual(200, result.Status);
            Assert.That(result.Success, Is.True);
            Assert.AreEqual(2, result.Data.Count);
        }


        [Test]
        public void GetUserTodos_ShouldReturnTodosForUser_WhenUserIdExists()
        {
            // Arrange
            var userId = "test-user-id";
            var todos = new List<Todo.Models.Entities.Todo>
            {
                new Todo.Models.Entities.Todo { Id = Guid.NewGuid(), Title = "Test Todo 1", UserId = userId },
                new Todo.Models.Entities.Todo { Id = Guid.NewGuid(), Title = "Test Todo 2", UserId = userId }
            };
            var responseDtos = new List<TodoResponseDto>
            {
                new TodoResponseDto { id = todos[0].Id, Title = todos[0].Title },
                new TodoResponseDto { id = todos[1].Id, Title = todos[1].Title }
            };

            // Repository ve Mapper mock ayarlarý
            _mockRepository.Setup(r => r.GetAll(It.IsAny<Expression<Func<Todo.Models.Entities.Todo, bool>>>()))
                           .Returns(todos);
            _mockMapper.Setup(m => m.Map<List<TodoResponseDto>>(todos)).Returns(responseDtos);

            // Act
            var result = _todoService.GetUserTodos();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success);
            Assert.AreEqual(200, result.Status);
            Assert.AreEqual(2, result.Data.Count);
            Assert.AreEqual("Test Todo 1", result.Data[0].Title);
            Assert.AreEqual("Test Todo 2", result.Data[1].Title);
        }

        [Test]
        public void GetUserTodos_ShouldReturnUnauthorized_WhenUserIdIsNull()
        {
            // Arrange
            _httpContextAccessorMock.Setup(x => x.HttpContext).Returns((HttpContext)null); // userId null olacak þekilde ayarladýk

            // Act
            var result = _todoService.GetUserTodos();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(401, result.Status);
            Assert.AreEqual("Kullanýcý kimliði bulunamadý.", result.Message);
        }

    }
}
