using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Todo.Models.Entities;



namespace Todo.Repository.Context
{
    public class BaseDbContext : IdentityDbContext<User, IdentityRole, string>
    {

        public BaseDbContext(DbContextOptions opt) : base(opt)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("SqlConnection", options =>
        //        options.EnableRetryOnFailure(
        //            maxRetryCount: 5, // Yeniden deneme sayısı
        //            maxRetryDelay: TimeSpan.FromSeconds(10), // Denemeler arasındaki bekleme süresi
        //            errorNumbersToAdd: null // Özel hata numaraları yoksa null bırakabilirsiniz
        //        ));
        //}

        public DbSet<Todo.Models.Entities.Todo> todos { get; set; }  

        public DbSet<Todo.Models.Entities.Category> Categories { get; set; }

        

    }
}
