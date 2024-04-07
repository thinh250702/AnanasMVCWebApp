using AnanasMVCWebApp.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AnanasMVCWebApp.Repositories {
    public class GenericRepository<T> : IRepository<T> where T : class {
        protected readonly DataContext _context;
        public GenericRepository(DataContext context) {
            _context = context;
        }

        public IEnumerable<T> GetAll() {
            return _context.Set<T>();
        }

        public T GetById(int id) {
            return _context.Set<T>().Find(id);
        }

        public void Insert(T entity) {
            _context.Set<T>().Add(entity);
        }

        public void Save() {
            _context.SaveChanges();
        }

        public void Update(T entity) {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
