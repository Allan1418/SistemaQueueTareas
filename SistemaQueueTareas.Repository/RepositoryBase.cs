namespace SistemaQueueTareas.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SistemaQueueTareas.Data;

    //This class applied the Single Responsability Principle
    public interface IRepositoryBase<T> where T : class
    {
        IEnumerable<T> GetAll();//take all the registers
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
        void Save();
    }
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly SistemaQueueTareasContext _context;
        protected readonly DbSet<T> _dbSet;

        //create constructors
        public RepositoryBase()
        {
            _context = new SistemaQueueTareasContext();
            _dbSet = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
            Save();
        }
        public void Update(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
                _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            Save();
        }

        public void Delete(int id)
        {
            T entityToDelete = _dbSet.Find(id);
            if (entityToDelete != null)
            {
                _dbSet.Remove(entityToDelete);
                Save();
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
