using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Todo.Models.Entities;
using Todo.Models.Todos;
using Todo.Service.Abstract;

namespace Todo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController(ITodoService _todoService) : ControllerBase
    {

        [HttpGet("getall")]
        [Authorize(Roles = "User")]
        public IActionResult GetAll()
        {
            var result = _todoService.GetAll();
            return Ok(result);
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] CreateTodoRequestDto dto)
        {

            // kullanıcının tokenden id alanının alınması.
            string userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var result = _todoService.Add(dto, userId);
            return Ok(result);
        }

        [HttpGet("getbyid/{id}")]
        public IActionResult GetById([FromRoute] Guid id)
        {

            var result = _todoService.GetById(id);
            return Ok(result);
        }

        [HttpPut("Update")]
        public IActionResult Update([FromBody] UpdateTodoRequestDto dto)
        {
            var result = _todoService.Update(dto);
            return Ok(result);
        }

        [HttpGet("getallbycategoryid")]
        public IActionResult GetAllByCategoryId(int id)
        {
            var result = _todoService.GetAllByCategoryId(id);
            return Ok(result);
        }

        [HttpGet("getallbyauthorid")]
        public IActionResult GetAllByAuthorId(string id)
        {

            var result = _todoService.GetAllByAuthorId(id);
            return Ok(result);
        }

        [HttpGet("getallbytitlecontains")]
        public IActionResult GetAllByTitleContains(string text)
        {
            var result = _todoService.GetAllByTitleContains(text);
            return Ok(result);
        }

        [HttpGet("completed")]
        public async Task<IActionResult> GetCompletedTodos()
        {
            var result = await _todoService.GetCompletedTodos();
            return Ok(result);
        }

        [HttpGet("overdue")]
        public async Task<IActionResult> GetOverdueTodos()
        {
            var result = await _todoService.GetOverdueTodos();
            return Ok(result);
        }

        [HttpGet("priority")]
        public async Task<IActionResult> GetTodosByPriority([FromQuery] Priority priority)
        {
            var result = await _todoService.GetTodosByPriority(priority);
            return Ok(result);
        }

        //// authorize property kullanmadan yapılan deneme işlemi
        //// Kullanıcının yapılacak işlerini döndüren endpoint
        //[HttpGet("mytodos")]
        //public IActionResult GetUserTodos()
        //{
        //    // JWT token üzerinden kullanıcının ID'sini alıyoruz
        //    var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        //    if (userId == null)
        //    {
        //        return Unauthorized(new { message = "Kullanıcı kimliği bulunamadı." });
        //    }

        //    // Kullanıcı kimliğine göre yapılacak işleri alıyoruz
        //    var result = _todoService.GetUserTodos(userId);

        //    return Ok(result);
        //}
        [HttpGet("mytodos")]
        public IActionResult GetUserTodos()
        {
            var result = _todoService.GetUserTodos();
            return Ok(result);
        }
    }
}
