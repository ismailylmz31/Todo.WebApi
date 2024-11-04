using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Repository.Context;
using Todo.Repository.Repository.Abstract;
using Todo.Repository.Repository.Concretes;

namespace Todo.Repository
{
    public static class RepositoryDependencies
    {

        public static IServiceCollection AddRepositoryDepencdencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICategoryRepository, EfCategoryRepository>();
            services.AddScoped<ITodoRepository, EfTodoRepository>();
            services.AddDbContext<BaseDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("SqlConnection")));
            return services;
        }
    }
}
