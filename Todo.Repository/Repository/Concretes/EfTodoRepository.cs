using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Core.Repository;
using Todo.Models.Entities;
using Todo.Repository.Context;
using Todo.Repository.Repository.Abstract;

namespace Todo.Repository.Repository.Concretes
{
    public class EfTodoRepository : EfRepositoryBase<BaseDbContext, Todo.Models.Entities.Todo, Guid>, ITodoRepository
    {
        public EfTodoRepository(BaseDbContext context) : base(context)
        {
        }

        public async Task<List<Todo.Models.Entities.Todo>> GetCompletedTodos()
        {
            return await Context.todos.Where(todo => todo.Completed).ToListAsync();
        }

        public async Task<List<Todo.Models.Entities.Todo>> GetOverdueTodos()
        {
            return await Context.todos.Where(todo => todo.EndDate < DateTime.Now && !todo.Completed).ToListAsync();
        }

        public async Task<List<Todo.Models.Entities.Todo>> GetTodosByPriority(Priority priority)
        {
            return await Context.todos.Where(todo => todo.Priority == priority).ToListAsync();
        }
    }
}
