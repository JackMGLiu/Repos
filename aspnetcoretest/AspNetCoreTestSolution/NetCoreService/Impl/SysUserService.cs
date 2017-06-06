using System;
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
                _sysUserRepository.AddModel(model);
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
