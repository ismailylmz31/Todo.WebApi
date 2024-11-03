using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Models.Todos;

namespace Todo.Service.Validations.Todos
{
    public class CreateTodoRequestDtoValidator : AbstractValidator<CreateTodoRequestDto>
    {
        public CreateTodoRequestDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("todo Başlığı boş olamaz.")
                .Length(2, 50).WithMessage("TOdo Başlığı Minimum 2 max 50 karakterli olmalıdır.");


            RuleFor(x => x.Description).NotEmpty().WithMessage("TOdo İçeriği boş olamaz.");
        }
    }
}
