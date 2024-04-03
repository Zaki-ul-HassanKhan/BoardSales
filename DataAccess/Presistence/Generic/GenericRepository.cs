using DataAccess.Context;
using Domain.EntityModels;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Presistence.Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly BoardSalesDbContext _boardSalesDbContext;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(BoardSalesDbContext boardSalesDbContext)
        {
            this._boardSalesDbContext = boardSalesDbContext;
            _dbSet = this._boardSalesDbContext.Set<T>();
        }
        public void Add(T entity)
        {
          _dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            return _boardSalesDbContext.Set<T>();
        }

        public T GetById(int id)
        {
           return _dbSet.Find(id);
        }

        public void Update(T entity)
        {
           _dbSet.Attach(entity);
            _boardSalesDbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
