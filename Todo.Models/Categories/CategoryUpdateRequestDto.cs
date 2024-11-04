using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Models.Categories
{
    public sealed record CategoryUpdateRequestDto(int Id, string Name);
}
