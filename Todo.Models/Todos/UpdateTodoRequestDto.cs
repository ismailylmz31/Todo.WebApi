namespace Todo.Models.Todos;

public sealed record UpdateTodoRequestDto(Guid Id,string Title,string Description, bool Completed);
