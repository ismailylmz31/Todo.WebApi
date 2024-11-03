using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Models.Todos;

public sealed record TodoResponseDto {
    
    public Guid id { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public  string AuthorUserName { get; init; }
    public string CategoryName { get; init; }
    public bool Completed { get; init; }
}
