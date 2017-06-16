using System;
using AutoMapper;
using NetCoreData.Models;
using NetCoreModel;
using NetCoreRepository.Interface;
using NetCoreService.DTO;
using NetCoreService.Interface;

namespace NetCoreService.Impl
{
    public class SysUserService : ISysUserService
    {
        protected ISysUserRepository _sysUserRepository;
        protected IMapper _mapper { get; set; }

        public SysUserService(IMapper mapper, ISysUserRepository sysUserRepository)
        {
            this._mapper = mapper;
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
                model.ModifyTime = DateTime.Now;
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

        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="userpass">密码</param>
        /// <param name="msg">返回信息</param>
        /// <param name="viewModel">返回实体类</param>
        /// <returns></returns>
        public bool UserLogin(string username, string userpass, out string msg, out SysUserViewModel viewModel)
        {
            bool flag = false;
            try
            {
                string sql = "select count(1) from SysUser where UserName=@UserName and IsDelete=0";
                var count = _sysUserRepository.GetCount(sql, new { UserName = username });
                if (count > 0)
                {
                    string selectsql = "select * from SysUser where UserName=@UserName and IsDelete=0";
                    var currentmodel = _sysUserRepository.GetModelBySql(sql, new { UserName = username });
                    if (currentmodel.Status != 1)
                    {
                        viewModel = null;
                        msg = "该用户已被禁用！";
                        flag = false;
                    }
                    else
                    {
                        if (currentmodel.PassWord == userpass)
                        {
                            viewModel = _mapper.Map<SysUserViewModel>(currentmodel); ;
                            msg = "登陆成功，正在跳转页面...";
                            flag = true;
                        }
                        else
                        {
                            viewModel = null;
                            msg = "账号或密码错误！";
                            flag = false;
                        }
                    }
                }
                else
                {
                    viewModel = null;
                    msg = "该用户不存在或者已被禁用！";
                    flag = false;
                }
            }
            catch (Exception ex)
            {
                viewModel = null;
                msg = "系统错误!" + ex.Message;
                flag = false;
            }
            return flag;
        }
    }
}
