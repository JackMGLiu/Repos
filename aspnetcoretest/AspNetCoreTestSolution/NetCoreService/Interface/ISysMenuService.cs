using System.Collections.Generic;
using NetCoreModel;

namespace NetCoreService.Interface
{
    public interface ISysMenuService
    {
        /// <summary>
        /// 新增目录
        /// </summary>
        /// <param name="model">目录实体信息</param>
        /// <returns></returns>
        bool AddMenu(SysMenu model);

        /// <summary>
        /// 编辑目录
        /// </summary>
        /// <param name="model">目录实体信息</param>
        /// <returns></returns>
        bool EditMenu(SysMenu model);

        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="key">目录主键</param>
        /// <returns></returns>
        bool DeleteMenu(string key);

        /// <summary>
        /// 根据主键获取目录实体
        /// </summary>
        /// <param name="key">主键</param>
        /// <returns></returns>
        SysMenu GetSysMenuByKey(string key);

        /// <summary>
        /// 获取目录信息集合
        /// </summary>
        /// <returns></returns>
        IEnumerable<SysMenu> GerMenuList();
    }
}