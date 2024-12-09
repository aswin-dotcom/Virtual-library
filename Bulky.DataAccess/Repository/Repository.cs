﻿//using Book.DataAccess.Data;
//using Book.DataAccess.Repository.IRepository;
using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbset;
        //private ApplicationDbContext db;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
           this.dbset = _db.Set<T>();


        }

       
      

        public void Add(T entity)
        {
            dbset.Add(entity);
            
        }

        public T Get(Expression<Func<T,bool>> filter)
        {
            IQueryable<T> query =this.dbset;
            query = query.Where(filter);
            return query.FirstOrDefault();

        }

 

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query  = this.dbset;
            return query.ToList();
        }
        public void Remove(T entity)
        {
            dbset.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
           dbset.RemoveRange(entities);
        }

     
    }
}
