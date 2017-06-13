using System;
using NetCoreData.Models;
using NetCoreModel;
using NetCoreRepository.Interface;
using NetCoreService.Interface;

namespace NetCoreService.Impl
{
    public class SysUserService : ISysUserService
    {
        protected ISysUserRepository _sysUserRepository;

        public SysUserService(ISysUserRepository sysUserRepository)
        {
            this._sysUserRepository = sysUserRepository;
        }

        public bool AddUser(SysUser model)
        {
            bool flag = false;
            try
            {
                model.DoCreate();
                _sysUserRepository.AddModel(model);
                flag = true;
            }
            catch (Exception ex)
            {
                flag = false;
            }
            return flag;
        }

        public PageDataView<SysUser> GetPageList(string username, int page, int pageSize)
        {
            PageCriteria criteria = new PageCriteria();
            criteria.Condition = "IsDelete=0";
            criteria.Sort = "CreateTime";
            if (!string.IsNullOrEmpty(username))
            {
                criteria.Condition += string.Format(" and UserName like '%{0}%'", username);
            }

            //if (!string.IsNullOrEmpty(loginName))
            //    criteria.Condition += string.Format(" and LoginName like '%{0}%'", loginName);
            criteria.CurrentPage = page;
            criteria.Fields = "*";
            criteria.PageSize = pageSize;
            criteria.TableName = "SysUser";
            criteria.PrimaryKey = "UserId";
            var r = _sysUserRepository.GetPageData<SysUser>(criteria);
            return r;
        }

        /// <summary>
        /// 根据主键获取用户实体
        /// </summary>
        /// <param name="key">主键</param>
        /// <returns></returns>
        public SysUser GetSysUserByKey(string key)
        {
            try
            {
                if (!string.IsNullOrEmpty(key))
                {
                    return _sysUserRepository.GetModel(key);
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
        /// 编辑用户
        /// </summary>
        /// <param name="model">用户实体信息</param>
        /// <returns></returns>
        public bool EditUser(SysUser model)
        {
            bool flag = false;
            try
            {
                model.ModifyTime=DateTime.Now;
                _sysUserRepository.ModifyModel(model);
                flag = true;
            }
            catch (Exception ex)
            {
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="key">用户主键</param>
        /// <returns></returns>
        public bool DeleteUser(string key)
        {
            bool flag = false;
            try
            {
                _sysUserRepository.DeleteModel(key);
                flag = true;
            }
            catch (Exception ex)
            {
                flag = false;
            }
            return flag;
        }
    }
}
