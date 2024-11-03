using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Models.Entities;

namespace Todo.Repository.Configurations
{
    public class TodoConfigurations : IEntityTypeConfiguration<Todo.Models.Entities.Todo>
    {
        public void Configure(EntityTypeBuilder<Todo.Models.Entities.Todo> builder)
        {
            builder.ToTable("Todos").HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("TodoId");
            builder.Property(c => c.CreatedTime).HasColumnName("CreatedTime");
            builder.Property(c => c.UpdatedTime).HasColumnName("UpdatedTime");
            builder.Property(c => c.Title).HasColumnName("Title");
            builder.Property(c => c.Description).HasColumnName("Content");
            builder.Property(c => c.UserId).HasColumnName("UserId");
            builder.Property(c => c.CategoryId).HasColumnName("CategoryId");


            builder.HasOne(x => x.Category).WithMany(x => x.Todos).HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(X => X.User).WithMany(x => x.Todos).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
            


            builder.Navigation(x => x.Category).AutoInclude();
            builder.Navigation(x => x.User).AutoInclude();
            
        }
    }
}
