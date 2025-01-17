﻿using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace DedtechChallenge.Repositories.Interfaces
{
    public interface IGenericRepository<T, Pk> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAllAndSortBy(Expression<Func<T, object>> sortBy, string order = "asc");
        Task<T> GetByIdAsync(Pk id);
        IQueryable<T> Find(Expression<Func<T, bool>> expression);
        Task<EntityEntry> SaveAsync(T entity);
        EntityEntry Update(T entity);
        void Delete(T entity);
        bool Exists(Expression<Func<T, bool>> expression);
    }
}
