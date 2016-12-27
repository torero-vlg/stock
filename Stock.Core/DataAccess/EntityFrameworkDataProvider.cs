﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Stock.Core.DataAccess
{
    public class EntityFrameworkDataProvider : DbContext, IDataProvider
    {
        public List<T> Select<T>() where T : class
        {
            return Set<T>().ToList();
        }

        public List<T> Where<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return Set<T>().Where(expression).ToList();
        }

        public T SingleOrDefault<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return Set<T>().Where(expression).SingleOrDefault();
        }

        public T Create<T>(T entity) where T : class
        {
            Set<T>().Add(entity);

            SaveChanges();

            return entity;
        }

        public T Update<T>(T entity) where T : class
        {
            Set<T>().Attach(entity);
            Entry(entity).State = EntityState.Modified;

            SaveChanges();

            return entity;
        }

        public void Delete<T>(T entity) where T : class
        {
            Set<T>().Attach(entity);
            Set<T>().Remove(entity);
            SaveChanges();
        }
    }
}