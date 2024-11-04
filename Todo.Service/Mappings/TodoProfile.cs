using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Models.Entities;
using Todo.Models.Todos;

namespace Todo.Service.Mappings
{
    public class TodoProfile : Profile
    {
        public TodoProfile()
        {
            CreateMap<CreateTodoRequestDto, Todo.Models.Entities.Todo>();
            CreateMap<Todo.Models.Entities.Todo, TodoResponseDto>();
            CreateMap<UpdateTodoRequestDto, Todo.Models.Entities.Todo>();
        }
    }
}
