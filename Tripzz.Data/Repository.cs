using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tripzz.Entity;
using System.Data.SqlClient;
using System.Collections;
using System.Data;

namespace Tripzz.Data
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationContext context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;

        public Repository(ApplicationContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }
        public IQueryable<T> GetAll()
        {
            return entities.AsQueryable();

        }

        public T Get(int id)
        {
            //return await entities.SingleOrDefaultAsync(s => s.Id == id);
            return entities.SingleOrDefault(s => s.Id == id);
        }

        public bool Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            if (context.SaveChanges() > 0) { return true; }
            return false;
        }

        public bool Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            if (context.SaveChanges() > 0) { return true; }
            return false;
        }

        public bool Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            if (context.SaveChanges() > 0) { return true; }
            return false;
        }

        public virtual IQueryable<T> Table
        {
            get
            {
                return this.Entities;
            }
        }

        private DbSet<T> Entities
        {
            get
            {
                if (entities == null)
                {
                    entities = context.Set<T>();
                }
                return entities;
            }
        }

        public DbCommand GetDbConnectionCommand() => context.Database.GetDbConnection().CreateCommand();

        public DbDataReader ExecuteStoredProcedure(string storedProcedure,
             List<SqlParameter> parameters)
        {
            using (var cmd =
               context.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = storedProcedure;
                cmd.CommandType = CommandType.StoredProcedure;

                // set some parameters of the stored procedure
                foreach (var parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                return cmd.ExecuteReader();
                //using (var dataReader = cmd.ExecuteReader())
                //{
                //    var test = DataReaderMapper.MapToList<T>(dataReader);
                //    return test;
                //}
            }
        }

    }
}
