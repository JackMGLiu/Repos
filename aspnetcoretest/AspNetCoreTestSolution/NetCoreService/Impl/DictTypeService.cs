using System;
using NetCoreModel;
using NetCoreRepository.Interface;
using NetCoreService.Interface;

namespace NetCoreService.Impl
{
    public class DictTypeService: IDictTypeService
    {
        protected IDictTypeRepository _dictTypeRepository;

        public DictTypeService(IDictTypeRepository dictTypeRepository)
        {
            this._dictTypeRepository = dictTypeRepository;
        }

        public bool AddDictType(DictType model)
        {
            bool flag = false;
            try
            {
                model.DoCreate();
                _dictTypeRepository.AddModel(model);
                flag = true;
            }
            catch (Exception ex)
            {
                flag = false;
            }
            return flag;
        }

        public bool EditDictType(DictType model)
        {
            bool flag = false;
            try
            {
                model.ModifyTime = DateTime.Now;
                _dictTypeRepository.ModifyModel(model);
                flag = true;
            }
            catch (Exception ex)
            {
                flag = false;
            }
            return flag;
        }

        public DictType GetDictTypeByKey(string key)
        {
            try
            {
                if (!string.IsNullOrEmpty(key))
                {
                    return _dictTypeRepository.GetModel(key);
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
