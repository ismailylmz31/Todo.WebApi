﻿using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Todo.Core.Tokens.Configuration;

public static class SecurityKeyHelper
{
    public static SecurityKey GetSecurityKey(string securityKey)
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey)); 
    }
}
