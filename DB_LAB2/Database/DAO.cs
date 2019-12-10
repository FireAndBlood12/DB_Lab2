using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB_LAB2.Database
{
    public abstract class DAO<T>
    {
        protected DBConnection dbconnection;

        public DAO(DBConnection db)
        {
            dbconnection = db;
        }

        public abstract long Create(T entity);
        public abstract T GetById(long id);
        public abstract List<T> GetList(int page);
        public abstract void Update(T entity);
        public abstract void Delete(long id);
        public abstract long Count();
    }
}
