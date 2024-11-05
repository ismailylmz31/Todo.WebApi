using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Todo.Core.Entities;
using Todo.Core.ExceptionHandler;
using Todo.Models.Entities;
using Todo.Models.Todos;
using Todo.Repository.Repository.Abstract;
using Todo.Service.Abstract;
using Todo.Service.Constant;
using Todo.Service.Rules;

namespace Todo.Service.Concretes
{
    public sealed class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;
        private readonly TodoBusinessRules _businessRules;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public TodoService(ITodoRepository todoRepository, IMapper mapper, TodoBusinessRules businessRules, IHttpContextAccessor httpContextAccessor)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
            _businessRules = businessRules;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<ReturnModel<TodoResponseDto>> Add(CreateTodoRequestDto dto, string userId)
        {
            Todo.Models.Entities.Todo createdTodo = _mapper.Map<Todo.Models.Entities.Todo>(dto);
            createdTodo.Id = Guid.NewGuid();
            createdTodo.UserId = userId;

            Todo.Models.Entities.Todo todo = _todoRepository.Add(createdTodo);



            TodoResponseDto response = _mapper.Map<TodoResponseDto>(todo);

            return new ReturnModel<TodoResponseDto>
            {
                Data = response,
                Message = "Yapılacaklar listesi eklendi.",
                Status = 200,
                Success = true
            };
        }

        public ReturnModel<string> Delete(Guid id)
        {
            _businessRules.TodoIsPresent(id);



            Todo.Models.Entities.Todo? todo = _todoRepository.GetById(id);
            Todo.Models.Entities.Todo deletedTodo = _todoRepository.Delete(todo);

            return new ReturnModel<string>
            {
                Data = $"Todo Başlığı : {deletedTodo.Title}",
                Message = Messages.TodoDeletedMessage,
                Status = 204,
                Success = true
            };
        }

        public ReturnModel<List<TodoResponseDto>> GetAll()
        {
            var todos = _todoRepository.GetAll();
            List<TodoResponseDto> responses = _mapper.Map<List<TodoResponseDto>>(todos);
            return new ReturnModel<List<TodoResponseDto>>
            {
                Data = responses,
                Message = string.Empty,
                Status = 200,
                Success = true
            };
        }

        public ReturnModel<List<TodoResponseDto>> GetAllByAuthorId(string userId)
        {
            List< Todo.Models.Entities.Todo> todos = _todoRepository.GetAll(p => p.UserId == userId );
            List<TodoResponseDto> responses = _mapper.Map<List<TodoResponseDto>>(todos);

            return new ReturnModel<List<TodoResponseDto>>
            {
                Data = responses,
                Message = $"todolar listelendi : Yazar Id: {userId}",
                Status = 200,
                Success = true
            };

        }

        public ReturnModel<List<TodoResponseDto>> GetAllByCategoryId(int id)
        {
            List<Todo.Models.Entities.Todo> todos = _todoRepository.GetAll(x => x.CategoryId == id);
            List<TodoResponseDto> responses = _mapper.Map<List<TodoResponseDto>>(todos);
            return new ReturnModel<List<TodoResponseDto>>
            {
                Data = responses,
                Message = $"Kategori Id sine göre todolar listelendi : Kategori Id: {id}",
                Status = 200,
                Success = true
            };
        }

        public ReturnModel<List<TodoResponseDto>> GetAllByTitleContains(string text)
        {
            var todos = _todoRepository.GetAll(x => x.Title.Contains(text));
            var responses = _mapper.Map<List<TodoResponseDto>>(todos);
            return new ReturnModel<List<TodoResponseDto>>
            {
                Data = responses,
                Message = string.Empty,
                Status = 200,
                Success = true
            };
        }

        public ReturnModel<TodoResponseDto> GetById(Guid id)
        {
            try
            {
                _businessRules.TodoIsPresent(id);

                var post = _todoRepository.GetById(id);
                var response = _mapper.Map<TodoResponseDto>(post);
                return new ReturnModel<TodoResponseDto>
                {
                    Data = response,
                    Message = "İlgili post gösterildi",
                    Status = 200,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return ExceptionHandler<TodoResponseDto>.HandleException(ex);
            }

        }

        public ReturnModel<TodoResponseDto> Update(UpdateTodoRequestDto dto)
        {

            _businessRules.TodoIsPresent(dto.Id);

            Todo.Models.Entities.Todo todo = _todoRepository.GetById(dto.Id);

            todo.Title = dto.Title;
            todo.Description = dto.Description;
            todo.Completed = dto.Completed;

            _todoRepository.Update(todo);

            TodoResponseDto response = _mapper.Map<TodoResponseDto>(todo);

            return new ReturnModel<TodoResponseDto>
            {
                Data = response,
                Message = "Todo Güncellendi.",
                Status = 200,
                Success = true
            };

        }


        public async Task<ReturnModel<List<TodoResponseDto>>> GetCompletedTodos()
        {
            var todos = await _todoRepository.GetCompletedTodos();
            var response = _mapper.Map<List<TodoResponseDto>>(todos);
            return new ReturnModel<List<TodoResponseDto>>
            {
                Data = response,
                Message = "Tamamlanmış todos listelendi",
                Status = 200,
                Success = true
            };
        }

        public async Task<ReturnModel<List<TodoResponseDto>>> GetOverdueTodos()
        {
            var todos = await _todoRepository.GetOverdueTodos();
            var response = _mapper.Map<List<TodoResponseDto>>(todos);
            return new ReturnModel<List<TodoResponseDto>>
            {
                Data = response,
                Message = "Süresi geçmiş todos listelendi",
                Status = 200,
                Success = true
            };
        }

        public async Task<ReturnModel<List<TodoResponseDto>>> GetTodosByPriority(Priority priority)
        {
            var todos = await _todoRepository.GetTodosByPriority(priority);
            var response = _mapper.Map<List<TodoResponseDto>>(todos);
            return new ReturnModel<List<TodoResponseDto>>
            {
                Data = response,
                Message = $"Öncelik seviyesi '{priority}' olan todos listelendi",
                Status = 200,
                Success = true
            };
        }
        //// Kullanıcıya ait yapılacak işleri filtreleyen metot
        //public ReturnModel<List<TodoResponseDto>> GetUserTodos(string userId)
        //{
        //    var todos = _todoRepository.GetAll(todo => todo.UserId == userId); // Kullanıcı kimliğine göre filtreleme
        //    var response = _mapper.Map<List<TodoResponseDto>>(todos);

        //    return new ReturnModel<List<TodoResponseDto>>
        //    {
        //        Data = response,
        //        Success = true,
        //        Status = 200,
        //        Message = $"User ID {userId} için yapılacak işler listelendi."
        //    };
        //}
        public ReturnModel<List<TodoResponseDto>> GetUserTodos()
        {
            // Token’dan userId’yi doğrudan serviste alıyoruz
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return new ReturnModel<List<TodoResponseDto>>
                {
                    Success = false,
                    Status = 401,
                    Message = "Kullanıcı kimliği bulunamadı."
                };
            }

            // Kullanıcı kimliğine göre yapılacak işleri filtreliyoruz
            var todos = _todoRepository.GetAll(todo => todo.UserId == userId);
            var response = _mapper.Map<List<TodoResponseDto>>(todos);

            return new ReturnModel<List<TodoResponseDto>>
            {
                Data = response,
                Success = true,
                Status = 200,
                Message = $"User ID {userId} için yapılacak işler listelendi."
            };
        }



        public ReturnModel<List<TodoResponseDto>> GetUserTodosByCompletionStatus(bool completed)
        {
            // Token’dan userId’yi doğrudan serviste alıyoruz
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return new ReturnModel<List<TodoResponseDto>>
                {
                    Success = false,
                    Status = 401,
                    Message = "Kullanıcı kimliği bulunamadı."
                };
            }

            // Kullanıcı kimliğine ve tamamlanma durumuna göre yapılacak işleri filtreliyoruz
            var todos = _todoRepository.GetAll(todo => todo.UserId == userId && todo.Completed == completed);
            var response = _mapper.Map<List<TodoResponseDto>>(todos);

            return new ReturnModel<List<TodoResponseDto>>
            {
                Data = response,
                Success = true,
                Status = 200,
                Message = $"User ID {userId} için {(completed ? "tamamlanmış" : "tamamlanmamış")} işler listelendi."
            };
        }
    }
}
