using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using NetCoreData.Models;

namespace NetCoreData.Dapper
{
    public interface IDapperHelper<T> where T : class
    {
        IDbConnection Connection { get; }

        IDbTransaction DbTransaction { get; }

        #region CRUD

        void AddModel(T entity, IDbTransaction dbTrans=null);

        void AddModels(IEnumerable<T> entities, IDbTransaction dbTrans = null);

        void DeleteModel(T entity, IDbTransaction dbTrans = null);

        void DeleteModel(object key, IDbTransaction dbTrans = null);

        void ModifyModel(T entity, IDbTransaction dbTrans = null);

        void ModifyModels(IEnumerable<T> entities, IDbTransaction dbTrans = null);

        T GetModel(object key);

        T GetModelBySql(string sql, object parmeters = null);

        IEnumerable<T> GetList(string sql, object parmeters=null);

        #endregion

        #region query

        PageDataView<T> GetPageData<T>(PageCriteria criteria, object param = null);

        long GetCount(string sql, object parmeters = null);

        #endregion


    }
}
