using System;
using System.Collections.Generic;
using NetCoreModel;
using NetCoreRepository.Interface;
using NetCoreService.Interface;

namespace NetCoreService.Impl
{
    public class SysMenuService : ISysMenuService
    {
        protected ISysMenuRepository _sysMenuRepository;

        public SysMenuService(ISysMenuRepository sysMenuRepository)
        {
            this._sysMenuRepository = sysMenuRepository;
        }

        public bool AddMenu(SysMenu model)
        {
            bool flag = false;
            try
            {
                model.DoCreate();
                _sysMenuRepository.AddModel(model);
                flag = true;
            }
            catch (Exception ex)
            {
                flag = false;
            }
            return flag;
        }

        public bool EditMenu(SysMenu model)
        {
            bool flag = false;
            try
            {
                model.DoModify(model.MenuId);
                _sysMenuRepository.ModifyModel(model);
                flag = true;
            }
            catch (Exception ex)
            {
                flag = false;
            }
            return flag;
        }

        public bool DeleteMenu(string key)
        {
            throw new System.NotImplementedException();
        }

        public SysMenu GetSysMenuByKey(string key)
        {
            try
            {
                if (!string.IsNullOrEmpty(key))
                {
                    return _sysMenuRepository.GetModel(key);
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

        public IEnumerable<SysMenu> GerMenuList()
        {
            try
            {
                string sql = "select * from SysMenu where IsDelete=0 order by SortCode asc";
                return _sysMenuRepository.GetList(sql,null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}