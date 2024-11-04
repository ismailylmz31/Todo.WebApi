using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Core.Exceptions;
using Todo.Repository.Repository.Abstract;
using Todo.Service.Constant;

namespace Todo.Service.Rules
{
    public class TodoBusinessRules
    {
        private readonly ITodoRepository _todoRepository;

        public TodoBusinessRules(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public virtual bool TodoIsPresent(Guid id)
        {
            var todo = _todoRepository.GetById(id);
            if (todo is null)
            {
                throw new NotFoundException(Messages.TodoIsNotPresentMessage(id));
            }

            return true;
        }
    }

}
