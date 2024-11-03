using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Todo.Service.Abstract;
using Todo.Service.Concretes;
using Todo.Service.Rules;

namespace Todo.Service
{
    public static class ServiceDependencies
    {
        public static IServiceCollection AddServiceDependenies(this IServiceCollection services)
        {
            services.AddScoped<ITodoService, TodoService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IJwtService, JwtService>();
            
            services.AddScoped<TodoBusinessRules>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            


            return services;
        }
    }
}
