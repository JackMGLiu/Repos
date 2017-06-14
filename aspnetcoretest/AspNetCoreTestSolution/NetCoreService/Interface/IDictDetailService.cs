using System.Collections.Generic;
using NetCoreModel;

namespace NetCoreService.Interface
{
    public interface IDictDetailService
    {
        /// <summary>
        /// 新增字典明细
        /// </summary>
        /// <param name="model">字典明细实体信息</param>
        /// <returns></returns>
        bool AddDictDetail(DictDetail model);

        /// <summary>
        /// 编辑字典明细
        /// </summary>
        /// <param name="model">字典明细实体信息</param>
        /// <returns></returns>
        bool EditDictDetail(DictDetail model);

        /// <summary>
        /// 根据主键获取字典明细实体
        /// </summary>
        /// <param name="key">主键</param>
        /// <returns></returns>
        DictDetail GetDictDetailByKey(string key);

        /// <summary>
        /// 根据类型编码获取字典明细信息集合
        /// </summary>
        /// <param name="typecode">类型编码</param>
        /// <returns></returns>
        IEnumerable<DictDetail> GerDictDetailList(string typecode);
    }
}