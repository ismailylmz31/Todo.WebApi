using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Models.Users;

public sealed record RegisterRequestDto(
    string Name,
    string Lastname,
    string Username,
    string Email,
    DateTime BirthDate,
    string Password
    );
