using NetCoreData.Models;
using NetCoreModel;

namespace NetCoreService.Interface
{
    public interface ISysUserService
    {
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="model">用户实体信息</param>
        /// <returns></returns>
        bool AddUser(SysUser model);

        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="model">用户实体信息</param>
        /// <returns></returns>
        bool EditUser(SysUser model);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="key">用户主键</param>
        /// <returns></returns>
        bool DeleteUser(string key);

        /// <summary>
        /// 根据主键获取用户实体
        /// </summary>
        /// <param name="key">主键</param>
        /// <returns></returns>
        SysUser GetSysUserByKey(string key);

        /// <summary>
        /// 获取用户分页信息
        /// </summary>
        /// <param name="username"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PageDataView<SysUser> GetPageList(string username, int page, int pageSize);
    }
}