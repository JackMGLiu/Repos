using System.Collections.Generic;
using NetCoreModel;

namespace NetCoreService.Interface
{
    public interface IDictTypeService
    {
        /// <summary>
        /// 新增字典类型
        /// </summary>
        /// <param name="model">字典类型实体信息</param>
        /// <returns></returns>
        bool AddDictType(DictType model);

        /// <summary>
        /// 编辑字典类型
        /// </summary>
        /// <param name="model">字典类型实体信息</param>
        /// <returns></returns>
        bool EditDictType(DictType model);

        /// <summary>
        /// 根据主键获取字典类型实体
        /// </summary>
        /// <param name="key">主键</param>
        /// <returns></returns>
        DictType GetDictTypeByKey(string key);

        /// <summary>
        /// 获取字典类型信息集合
        /// </summary>
        /// <returns></returns>
        IEnumerable<DictType> GerDictTypeList();
    }
}