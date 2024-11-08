﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Core.Entities;
using Todo.Models.Entities;
using Todo.Models.Todos;

namespace Todo.Service.Abstract
{
    public interface ITodoService
    {
        Task<ReturnModel<TodoResponseDto>> Add(CreateTodoRequestDto dto, string userId);
        ReturnModel<List<TodoResponseDto>> GetAll();
        ReturnModel<TodoResponseDto> GetById(Guid id);

        ReturnModel<TodoResponseDto> Update(UpdateTodoRequestDto dto);

        ReturnModel<string> Delete(Guid id);

        ReturnModel<List<TodoResponseDto>> GetAllByCategoryId(int id);
        ReturnModel<List<TodoResponseDto>> GetAllByAuthorId(string authorId);

        ReturnModel<List<TodoResponseDto>> GetAllByTitleContains(string text);

        Task<ReturnModel<List<TodoResponseDto>>> GetCompletedTodos();
        Task<ReturnModel<List<TodoResponseDto>>> GetOverdueTodos();
        Task<ReturnModel<List<TodoResponseDto>>> GetTodosByPriority(Priority priority);
        //ReturnModel<List<TodoResponseDto>> GetUserTodos(string userId);
        ReturnModel<List<TodoResponseDto>> GetUserTodos();

        ReturnModel<List<TodoResponseDto>> GetUserTodosByCompletionStatus(bool completed);
    }
}
