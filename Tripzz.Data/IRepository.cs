using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using Tripzz.Entity;

namespace Tripzz.Data
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();
        T Get(int id);
        bool Insert(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        DbCommand GetDbConnectionCommand();
        DbDataReader ExecuteStoredProcedure(string storedProcedure, List<SqlParameter> parameters);
    }
}
