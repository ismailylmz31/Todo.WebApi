using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Core.Repository;
using Todo.Models.Entities;
using Todo.Repository.Context;
using Todo.Repository.Repository.Abstract;

namespace Todo.Repository.Repository.Concretes
{
    public class EfCategoryRepository : EfRepositoryBase<BaseDbContext, Category, int>, ICategoryRepository
    {

        public EfCategoryRepository(BaseDbContext context) : base(context)
        {

        }
    }
}
