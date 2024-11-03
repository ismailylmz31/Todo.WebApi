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
    }
}
