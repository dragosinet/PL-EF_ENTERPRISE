using ReusableGenericRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace DisconnectedGenericRepository
{
  public class GenericRepository<TEntity> where TEntity : class
  {
    internal DbContext _context;
    internal DbSet<TEntity> _dbSet;

    public GenericRepository(DbContext context) {
      _context = context;
      _dbSet = context.Set<TEntity>();
    }
   
    public IEnumerable<TEntity> All() {
      return _dbSet.AsNoTracking().ToList();
    }

    public IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate) {
      IEnumerable<TEntity> results = _dbSet.AsNoTracking()
        .Where(predicate).ToList();
      return results;
    }

    public TEntity FindByKey(int id) {
      Expression<Func<TEntity, bool>> lambda = Utilities.BuildLambdaForFindByKey<TEntity>(id);
      return _dbSet.AsNoTracking().SingleOrDefault(lambda);
    }

    public void Insert(TEntity entity) {
      _dbSet.Add(entity);
    }

    public void Update(TEntity entity) {
      _dbSet.Attach(entity);
      _context.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(int id) {
      var entity = FindByKey(id);
      _dbSet.Remove(entity);
    }
  }
}