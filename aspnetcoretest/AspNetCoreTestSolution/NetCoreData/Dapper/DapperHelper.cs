using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;
using NetCoreData.Models;

namespace NetCoreData.Dapper
{
    public class DapperHelper<T> : IDapperHelper<T> where T : class
    {
        private readonly DbConnection _conn;

        public DapperHelper(DbConnection conn)
        {
            _conn = conn;
        }

        public IDbConnection Connection
        {
            get
            {
                if (_conn.State == ConnectionState.Closed)
                {
                    _conn.Open();
                }
                if (_conn.State == ConnectionState.Broken)
                {
                    _conn.Close();
                    _conn.Open();
                }
                return _conn;
            }
        }

        public IDbTransaction DbTransaction
        {
            get
            {
                return Connection.BeginTransaction();
            }
        }

        public IEnumerable<T> GetList(string sql, params DbParameter[] parmeters)
        {
            try
            {
                var data = Connection.Query<T>(sql, parmeters);
                return data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public T GetModelBySql(string sql, params DbParameter[] parmeters)
        {
            try
            {
                var model = _conn.QuerySingle<T>(sql, parmeters);
                return model;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public T GetModel(object key)
        {
            try
            {
                var model = _conn.Get<T>(key);
                return model;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void AddModel(T entity, IDbTransaction dbTrans = null)
        {
            try
            {
                if (dbTrans == null)
                {
                    Connection.Insert(entity);
                }
                else
                {
                    try
                    {
                        Connection.Insert(entity, dbTrans);
                        dbTrans.Commit();
                    }
                    catch (Exception ex)
                    {
                        dbTrans.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void AddModels(IEnumerable<T> entities, IDbTransaction dbTrans = null)
        {
            try
            {
                if (dbTrans == null)
                {
                    if (entities.Any())
                    {
                        foreach (var entity in entities)
                        {
                            _conn.Insert(entity);
                        }
                    }
                }
                else
                {
                    try
                    {
                        if (entities.Any())
                        {
                            foreach (var entity in entities)
                            {
                                _conn.Insert(entity, dbTrans);
                            }
                            dbTrans.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        dbTrans.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void DeleteModel(T entity, IDbTransaction dbTrans = null)
        {
            try
            {
                if (dbTrans == null)
                {
                    if (entity != null)
                    {
                        _conn.Delete(entity);
                    }
                }
                else
                {
                    try
                    {
                        if (entity != null)
                        {
                            _conn.Delete(entity, dbTrans);
                            dbTrans.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        dbTrans.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void DeleteModel(object key, IDbTransaction dbTrans = null)
        {
            try
            {
                if (dbTrans == null)
                {
                    if (!string.IsNullOrEmpty(key.ToString()))
                    {
                        var currentmodel = _conn.Get<T>(key);
                        _conn.Delete(currentmodel);
                    }
                }
                else
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(key.ToString()))
                        {
                            var currentmodel = _conn.Get<T>(key);
                            _conn.Delete(currentmodel, dbTrans);
                            dbTrans.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        dbTrans.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void ModifyModel(T entity, IDbTransaction dbTrans = null)
        {
            try
            {
                if (dbTrans == null)
                {
                    if (entity != null)
                    {
                        _conn.Update(entity);
                    }
                }
                else
                {
                    try
                    {
                        if (entity != null)
                        {
                            _conn.Update(entity, dbTrans);
                            dbTrans.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        dbTrans.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void ModifyModels(IEnumerable<T> entities, IDbTransaction dbTrans = null)
        {
            try
            {
                if (dbTrans == null)
                {
                    if (entities.Any())
                    {
                        foreach (var entity in entities)
                        {
                            _conn.Update(entity);
                        }
                    }
                }
                else
                {
                    try
                    {
                        if (entities.Any())
                        {
                            foreach (var entity in entities)
                            {
                                _conn.Update(entity, dbTrans);
                            }
                            dbTrans.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        dbTrans.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public long GetCount(string sql, params DbParameter[] parmeters)
        {
            try
            {
                long res = Connection.ExecuteScalar<long>(sql, parmeters);
                return res;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public PageDataView<T> GetPageData<T>(PageCriteria criteria, object param = null)
        {
            try
            {
                var p = new DynamicParameters();
                string proName = "ProcGetPageData";
                p.Add("TableName", criteria.TableName);
                p.Add("PrimaryKey", criteria.PrimaryKey);
                p.Add("Fields", criteria.Fields);
                p.Add("Condition", criteria.Condition);
                p.Add("CurrentPage", criteria.CurrentPage);
                p.Add("PageSize", criteria.PageSize);
                p.Add("Sort", criteria.Sort);
                p.Add("RecordCount", dbType: DbType.Int32, direction: ParameterDirection.Output);
                //_conn.Open();
                var pageData = new PageDataView<T>();
                pageData.Items = _conn.Query<T>(proName, p, commandType: CommandType.StoredProcedure).ToList();
                //_conn.Close();
                pageData.TotalNum = p.Get<int>("RecordCount");
                pageData.TotalPageCount = Convert.ToInt32(Math.Ceiling(pageData.TotalNum * 1.0 / criteria.PageSize));
                pageData.CurrentPage = criteria.CurrentPage > pageData.TotalPageCount ? pageData.TotalPageCount : criteria.CurrentPage;
                return pageData;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
