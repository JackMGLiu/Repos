using System;
using System.Collections.Generic;
using NetCoreModel;
using NetCoreRepository.Interface;
using NetCoreService.Interface;

namespace NetCoreService.Impl
{
    public class DictDetailService : IDictDetailService
    {
        protected IDictDetailRepository _dictDetailRepository;

        public DictDetailService(IDictDetailRepository dictDetailRepository)
        {
            this._dictDetailRepository = dictDetailRepository;
        }

        public bool AddDictDetail(DictDetail model)
        {
            bool flag = false;
            try
            {
                model.DoCreate();
                _dictDetailRepository.AddModel(model);
                flag = true;
            }
            catch (Exception ex)
            {
                flag = false;
            }
            return flag;
        }

        public bool EditDictDetail(DictDetail model)
        {
            bool flag = false;
            try
            {
                model.ModifyTime = DateTime.Now;
                _dictDetailRepository.ModifyModel(model);
                flag = true;
            }
            catch (Exception ex)
            {
                flag = false;
            }
            return flag;
        }

        public DictDetail GetDictDetailByKey(string key)
        {
            try
            {
                if (!string.IsNullOrEmpty(key))
                {
                    return _dictDetailRepository.GetModel(key);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据类型编码获取字典明细信息集合
        /// </summary>
        /// <param name="typecode">类型编码</param>
        /// <returns></returns>
        public IEnumerable<DictDetail> GerDictDetailList(string typecode)
        {
            try
            {
                if (!string.IsNullOrEmpty(typecode))
                {
                    string sql = "select * from DictDetail where DictTypeCode=@TypeCode and IsDelete=0 order by SortCode";

                    var data = _dictDetailRepository.GetList(sql, new { TypeCode = typecode });
                    return data;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}