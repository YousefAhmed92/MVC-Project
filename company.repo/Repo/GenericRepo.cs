using company.repo.Interface;
using Company.Data.Contexts;
using Company.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace company.repo.Repo
{
    public class GenericRepo<T> : IGenericRepo<T> where T : BaseEntity
    {
        private readonly CompanyDBContext _context;

        public GenericRepo(CompanyDBContext context)
        {
            _context = context;
        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            //_context.SaveChanges();

        }        
       
        public void Delete(T entity)
        {
            _context.Remove(entity);
            //_context.SaveChanges();

        }

        public IEnumerable<T> GetAll()
            => _context.Set<T>().ToList();

        public T GetById(int id)
            => _context.Set<T>().Find(id);

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            //_context.SaveChanges();
        }
    }

}
