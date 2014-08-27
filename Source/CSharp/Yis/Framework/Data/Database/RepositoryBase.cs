using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yis.Framework.Data.Database
{
    public abstract class RepositoryBase : IRepository
    {
        private IDbConnection _connection;
 
        public RepositoryBase(IDataContext dataContext)
        {
            if (dataContext == null)
            {
                throw new ArgumentNullException("pas de context donné");
            }

            DataContext = ((DataContextBase) dataContext);
 
        }

        /// <summary>
        /// Obtenir la connection
        /// </summary>
        protected virtual IDbConnection Connection
        {
            get { return DataContext.Connection; }
        }

        protected DataContextBase DataContext
        { get; private set; }
    }

    public abstract class RepositoryBase<TEntity> : RepositoryBase, IRepository<TEntity> where TEntity : class
    {

        public RepositoryBase(IDataContext dataContext)
            : base(dataContext)
        {

        }

        #region "Méthodes utilitaires"

        protected TEntity ExecuteSingle(CommandType commandType, string commandText)
        {
            return ExecuteSingle(commandType, commandText, null);
        }

        protected TEntity ExecuteSingle(CommandType commandType, string commandText, ICollection<IDataParameter> parameters)
        {
            IDbCommand command = Connection.CreateCommand();
            command.Connection = Connection;
            command.CommandText = commandText;
            command.CommandType = commandType;

            if (DataContext.IsInTransaction)
            {
                command.Transaction = DataContext.Transaction;
            }

            if (parameters != null)
            {
                foreach (IDataParameter param in parameters)
                {
                    command.Parameters.Add(param);
                }
            }

            try
            {
                if (!DataContext.IsInTransaction)
                {
                    Connection.Open();
                }

                using (IDataReader reader = command.ExecuteReader())
                {
                    try
                    {
                        return MapSingle(reader);

                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        reader.Close();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (!DataContext.IsInTransaction)
                {
                    Connection.Close();
                }
            }
        }

        protected IList<TEntity> ExecuteCollection(CommandType commandType, string commandText)
        {
            return ExecuteCollection(commandType, commandText, null);
        }

        protected IList<TEntity> ExecuteCollection(CommandType commandType, string commandText, ICollection<IDataParameter> parameters)
        {
            IDbCommand command = Connection.CreateCommand();
            command.Connection = Connection;
            command.CommandText = commandText;
            command.CommandType = commandType;

            if (DataContext.IsInTransaction)
            {
                command.Transaction = DataContext.Transaction;
            }

            if (parameters != null)
            {
                foreach (IDataParameter param in parameters)
                {
                    command.Parameters.Add(param);
                }
            }

            try
            {
                if (!DataContext.IsInTransaction)
                {
                    Connection.Open();
                }

                using (IDataReader reader = command.ExecuteReader())
                {
                    try
                    {
                        return MapCollection(reader);

                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        reader.Close();
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (!DataContext.IsInTransaction)
                {
                    Connection.Close();
                }
            }
        }

        protected bool ExecuteWrite(CommandType commandType, string commandText)
        {
            return ExecuteWrite(commandType, commandText, null);
        }

        protected bool ExecuteWrite(CommandType commandType, string commandText, ICollection<IDataParameter> parameters)
        {
            IDbCommand command = Connection.CreateCommand();
            command.Connection = Connection;
            command.CommandText = commandText;
            command.CommandType = commandType;

            if (DataContext.IsInTransaction)
            {
                command.Transaction = DataContext.Transaction;
            }

            if (parameters != null)
            {
                foreach (IDataParameter param in parameters)
                {
                    command.Parameters.Add(param);
                }
            }

            try
            {
                if (!DataContext.IsInTransaction)
                {
                    Connection.Open();
                }

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (!DataContext.IsInTransaction)
                {
                    Connection.Close();
                }
            }

            return true;
        }

        #endregion

        #region "Méthode de Mapping (Fonction abstraite)"

        protected abstract TEntity Map(IDataRecord record);

        protected TEntity MapSingle(IDataReader reader)
        {
            TEntity item = null;

            while (reader.Read())
            {
                item = Map(reader);
            }

            return item;
        }

        protected virtual IList<TEntity> MapCollection(IDataReader reader)
        {
            IList<TEntity> collection = new List<TEntity>();

            while (reader.Read())
            {
                try
                {
                    collection.Add(Map(reader));

                }
                catch (Exception)
                {
                    throw;
                }
            }

            return collection;
        }

        #endregion

        #region "Implémentaion IRepository"

        public virtual IList<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public virtual void Add(TEntity Entity)
        {
            throw new NotImplementedException();
        }


        public virtual void Update(TEntity Entity)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(TEntity Entity)
        {
            throw new NotImplementedException();
        }

        #endregion

        public IQueryable<TEntity> GetQuery()
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> GetQuery(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public TEntity Single(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate = null)
        {
            throw new NotImplementedException();
        }

        public TEntity SingleOrDefault(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate = null)
        {
            throw new NotImplementedException();
        }

        public TEntity First(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate = null)
        {
            throw new NotImplementedException();
        }

        public TEntity FirstOrDefault(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate = null)
        {
            throw new NotImplementedException();
        }

        public TEntity Create()
        {
            throw new NotImplementedException();
        }

        public void Delete(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> Find(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        //IQueryable<TEntity> IRepository<TEntity>.GetAll()
        //{
        //    throw new NotImplementedException();
        //}

        public int Count(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate = null)
        {
            throw new NotImplementedException();
        }


        IEnumerable<TEntity> IRepository<TEntity>.GetAll()
        {
            throw new NotImplementedException();
        }
    }

}
