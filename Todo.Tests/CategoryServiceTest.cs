using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Todo.Core.Exceptions;
using Todo.Models.Categories;
using Todo.Models.Entities;
using Todo.Repository.Repository.Abstract;
using Todo.Service.Concretes;
using AutoMapper;

namespace Todo.Tests
{
    [TestFixture]
    public class CategoryServiceTests
    {
        private Mock<ICategoryRepository> _mockCategoryRepository;
        private Mock<IMapper> _mockMapper;
        private CategoryService _categoryService;

        [SetUp]
        public void Setup()
        {
            _mockCategoryRepository = new Mock<ICategoryRepository>();
            _mockMapper = new Mock<IMapper>();
            _categoryService = new CategoryService(_mockCategoryRepository.Object, _mockMapper.Object);
        }

        [Test]
        public void Add_ShouldReturnSuccess_WhenCategoryIsAdded()
        {
            // Arrange
            var dto = new CategoryAddRequestDto("Test Category");
            var category = new Category { Id = 1, Name = dto.Name };

            _mockMapper.Setup(m => m.Map<Category>(dto)).Returns(category);
            _mockCategoryRepository.Setup(r => r.Add(It.IsAny<Category>())).Verifiable();

            // Act
            var result = _categoryService.Add(dto);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Status, Is.EqualTo(201));
            Assert.That(result.Success, Is.True);
            Assert.That(result.Message, Is.EqualTo("Kategori Eklendi."));
        }

        [Test]
        public void Delete_ShouldReturnSuccess_WhenCategoryIsDeleted()
        {
            // Arrange
            var categoryId = 1;
            var category = new Category { Id = categoryId, Name = "Test Category" };

            _mockCategoryRepository.Setup(r => r.GetById(categoryId)).Returns(category);
            _mockCategoryRepository.Setup(r => r.Delete(It.IsAny<Category>())).Verifiable();

            // Act
            var result = _categoryService.Delete(categoryId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Status, Is.EqualTo(200));
            Assert.That(result.Success, Is.True);
            Assert.That(result.Message, Is.EqualTo("Kategori silindi."));
        }

        [Test]
        public void GetAllCategories_ShouldReturnAllCategories()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Category 1" },
                new Category { Id = 2, Name = "Category 2" }
            };
            var responseDtos = new List<CategoryResponseDto>
            {
                new CategoryResponseDto { Id = categories[0].Id, Name = categories[0].Name },
                new CategoryResponseDto { Id = categories[1].Id, Name = categories[1].Name }
            };

            _mockCategoryRepository.Setup(r => r.GetAll(It.IsAny<Expression<Func<Category, bool>>>()))
                        .Returns(categories);


            _mockMapper.Setup(m => m.Map<List<CategoryResponseDto>>(categories)).Returns(responseDtos);

            // Act
            var result = _categoryService.GetAllCategories();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Status, Is.EqualTo(200));
            Assert.That(result.Success, Is.True);
            Assert.That(result.Data.Count, Is.EqualTo(2));
        }

        [Test]
        public void GetById_ShouldReturnCategory_WhenCategoryExists()
        {
            // Arrange
            var categoryId = 1;
            var category = new Category { Id = categoryId, Name = "Test Category" };
            var responseDto = new CategoryResponseDto { Id = categoryId, Name = category.Name };

            _mockCategoryRepository.Setup(r => r.GetById(categoryId)).Returns(category);
            _mockMapper.Setup(m => m.Map<CategoryResponseDto>(category)).Returns(responseDto);

            // Act
            var result = _categoryService.GetById(categoryId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Status, Is.EqualTo(200));
            Assert.That(result.Success, Is.True);
            Assert.That(result.Data.Name, Is.EqualTo("Test Category"));
        }

        [Test]
        public void GetById_ShouldThrowNotFoundException_WhenCategoryDoesNotExist()
        {
            // Arrange
            var categoryId = 1;
            _mockCategoryRepository.Setup(r => r.GetById(categoryId)).Returns((Category)null);

            // Act & Assert
            var ex = Assert.Throws<NotFoundException>(() => _categoryService.GetById(categoryId));
            Assert.That(ex.Message, Is.EqualTo("İlgili id ye ait Kategori bulunamadı: " + categoryId));
        }

        [Test]
        public void Update_ShouldReturnSuccess_WhenCategoryIsUpdated()
        {
            // Arrange
            var dto = new CategoryUpdateRequestDto(1, "Updated Category");
            var category = new Category { Id = dto.Id, Name = "Old Category" };

            _mockCategoryRepository.Setup(r => r.GetById(dto.Id)).Returns(category);
            _mockCategoryRepository.Setup(r => r.Update(It.IsAny<Category>())).Verifiable();

            // Act
            var result = _categoryService.Update(dto);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Status, Is.EqualTo(200));
            Assert.That(result.Success, Is.True);
            Assert.That(result.Message, Is.EqualTo("Kategori Güncellendi."));
        }

    }
}
