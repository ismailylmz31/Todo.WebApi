using Microsoft.AspNetCore.Identity;

namespace Todo.Models.Entities
{
    public sealed class User : IdentityUser
    {

        public DateTime BirthDate { get; set; }

        public List<Todo> Todos { get; set; }

        

    }
}