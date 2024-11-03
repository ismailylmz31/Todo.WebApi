using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
    }
}
