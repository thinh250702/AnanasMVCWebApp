﻿using System.Linq.Expressions;

namespace AnanasMVCWebApp.Repositories {
    public interface IRepository<T> where T : class {
        T GetById(int id);
        IEnumerable<T> GetAll();
        void Insert(T entity);
        void InsertRange(IEnumerable<T> entities);
        void Update(T entity);
    }
}
