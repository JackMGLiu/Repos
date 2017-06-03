using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Dapper;
using Dapper.Contrib.Extensions;

namespace NetCoreData
{
    public class SqlHelper<T> : ISqlHelper<T> where T : class
    {
        private readonly DbConnection _conn;

        public SqlHelper(DbConnection conn)
        {
            _conn = conn;
        }

        public void AddModel(T entity)
        {
            using (_conn)
            {
                _conn.Insert(entity);
            }
        }

        public IEnumerable<T> GetList(string sql, params DbParameter[] parmeters)
        {
            using (_conn)
            {
                var data = _conn.Query<T>(sql, parmeters);
                return data;
            }
        }

        public T GetModel(string sql, params DbParameter[] parmeters)
        {
            using (_conn)
            {
                var model = _conn.QuerySingle<T>(sql, parmeters);
                return model;
            }
        }

        public T GetModel(object key)
        {
            using (_conn)
            {
                var model = _conn.Get<T>(key);
                return model;
            }
        }
    }
}
