using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Core.Repository;
using Todo.Models.Entities;

namespace Todo.Repository.Repository.Abstract
{
    public interface ITodoRepository : IRepository<Todo.Models.Entities.Todo, Guid>
    {
    }
}
