
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Todo.Core.Entities;

namespace Todo.Core.Repository;

public interface IRepository<TEntity,TId> where TEntity: Entity<TId>,new()
{
    TEntity Add(TEntity entity);
    TEntity Update(TEntity entity);
    TEntity Delete(TEntity entity);

    TEntity? GetById(TId id);
    List<TEntity> GetAll(Expression<Func<TEntity,bool>>? filter = null);

}
