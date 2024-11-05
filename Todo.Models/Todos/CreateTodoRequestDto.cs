namespace Todo.Models.Todos;

public sealed record CreateTodoRequestDto(string Title, string Description, int CategoryId, string UserId);


