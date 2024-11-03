using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Models.Todos;

namespace Todo.Service.Validations.Todos
{
    public class UpdateTodoRequestDtoValidator : AbstractValidator<UpdateTodoRequestDto>
    {
        public UpdateTodoRequestDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Todo Başlığı boş olamaz.")
        .Length(2, 50).WithMessage("Todo Başlığı Minimum 2 max 50 karakterli olmalıdır.");


            RuleFor(x => x.Description).NotEmpty().WithMessage("Todo İçeriği boş olamaz.");
        }
    }
}
