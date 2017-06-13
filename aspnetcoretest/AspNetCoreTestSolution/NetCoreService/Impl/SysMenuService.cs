using System;
using System.Collections.Generic;
using NetCoreModel;
using NetCoreRepository.Interface;
using NetCoreService.Interface;

namespace NetCoreService.Impl
{
    public class SysMenuService : ISysMenuService
    {
        protected ISysMenuRepository _SysMenuRepository;

        public SysMenuService(ISysMenuRepository sysMenuRepository)
        {
            this._SysMenuRepository = sysMenuRepository;
        }

        public bool AddMenu(SysMenu model)
        {
            throw new System.NotImplementedException();
        }

        public bool EditMenu(SysMenu model)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteMenu(string key)
        {
            throw new System.NotImplementedException();
        }

        public SysUser GetSysMenuByKey(string key)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<SysMenu> GerMenuList()
        {
            try
            {
                string sql = "select * from SysMenu where IsDelete=0 order by SortCode asc";
                return _SysMenuRepository.GetList(sql,null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}