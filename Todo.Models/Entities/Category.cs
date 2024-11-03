using Todo.Core.Entities;

namespace Todo.Models.Entities
{
    public sealed class Category : Entity<int>
    {
        
        public string Name { get; set; }
        public List<Todo> Todos { get; set; }

    }
}