using System.Collections.Generic;
using System.Data.Common;

namespace NetCoreData
{
    public interface ISqlHelper<T> where T : class
    {
        void AddModel(T entity);

        IEnumerable<T> GetList(string sql, params DbParameter[] parmeters);

        T GetModel(string sql, params DbParameter[] parmeters);

        T GetModel(object key);
    }
}
