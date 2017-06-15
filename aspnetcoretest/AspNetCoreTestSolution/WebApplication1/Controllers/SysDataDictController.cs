using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NetCoreModel;
using NetCoreService.DTO;
using NetCoreService.DTO.FormatModel;
using NetCoreService.Interface;
using NLog;

namespace WebApplication1.Controllers
{
    public class SysDataDictController : Controller
    {
        private List<DictTypeModel> dictTypeData = null;

        protected Logger _log;

        protected IMapper _mapper { get; set; }

        protected IDictTypeService _dictTypeService;

        protected IDictDetailService _dictDetailService;

        public SysDataDictController(IMapper mapper, IDictTypeService dictTypeService, IDictDetailService dictDetailService)
        {
            dictTypeData = new List<DictTypeModel>();
            this._log = LogManager.GetCurrentClassLogger();
            this._mapper = mapper;
            this._dictTypeService = dictTypeService;
            this._dictDetailService = dictDetailService;
        }


        [Route("dict/list")]
        public IActionResult Index()
        {
            return View();
        }

        #region 数据字典类型

        [Route("dicttype/form")]
        public IActionResult EditTypeForm()
        {
            return View();
        }

        [HttpPost("dicttype/savedata")]
        public IActionResult EditTypeForm(string key, DictType model)
        {
            try
            {
                if (string.IsNullOrEmpty(key))
                {
                    if (model != null)
                    {
                        model.CreateUser = "测试人员";
                        var res = _dictTypeService.AddDictType(model);
                        if (res)
                        {
                            var json = new { type = 1, data = "", msg = "添加完成！", backurl = "" };
                            return Json(json);
                        }
                        else
                        {
                            var json = new { type = 0, data = "", msg = "添加失败！", backurl = "" };
                            return Json(json);
                        }
                    }
                    else
                    {
                        var json = new { type = 2, data = "", msg = "请填写完整数据！", backurl = "" };
                        return Json(json);
                    }
                }
                else
                {
                    var currentmodel = _dictTypeService.GetDictTypeByKey(key);
                    currentmodel.ParentId = model.ParentId;
                    currentmodel.DictTypeCode = model.DictTypeCode;
                    currentmodel.DictTypeName = model.DictTypeName;
                    currentmodel.IsNav = model.IsNav;
                    currentmodel.IsLast = model.IsLast;
                    currentmodel.SortCode = model.SortCode;
                    currentmodel.Status = model.Status;
                    currentmodel.Description = model.Description;
                    currentmodel.ModifyUser = "测试修改人员";
                    var res = _dictTypeService.EditDictType(currentmodel);
                    if (res)
                    {
                        var json = new { type = 1, data = "", msg = "编辑完成！", backurl = "" };
                        return Json(json);
                    }
                    else
                    {
                        var json = new { type = 0, data = "", msg = "编辑失败！", backurl = "" };
                        return Json(json);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex, ex.Message);
                throw;
            }
        }

        [HttpGet("dicttype/gettypetree")]
        public IActionResult GetTypeTreeData()
        {
            var data = _dictTypeService.GerDictTypeList().ToList();
            var res = GetTreeData("0", data);
            GetDictTypeTree(1, res, ref dictTypeData);
            return Json(dictTypeData);
        }

        [Route("dicttype/getlist")]
        public IActionResult GetDictTypes()
        {
            var data=_dictTypeService.GerDictTypeList().Where(t=>t.Status==1).Select(s => new TreeModel
            {
                id = s.DictTypeId,
                code = s.DictTypeCode,
                parentid = s.ParentId,
                name = s.DictTypeName,
                isnav = s.IsNav,
                islast = s.IsLast
            });
            //var data = DataTypeList().Where(dt => dt.IsNav == 1).OrderBy(d => d.SortCode).Select(s => new TreeModel
            //{
            //    id = s.DictTypeId,
            //    code = s.DictTypeCode,
            //    parentid = s.ParentId,
            //    name = s.DictTypeName,
            //    isnav = s.IsNav,
            //    islast = s.IsLast
            //});
            return Json(data);
        }

        #endregion

        #region 数据字典明细

        [Route("dict/form")]
        public IActionResult EditForm()
        {
            return View();
        }

        [HttpPost("dict/savedata")]
        public IActionResult EditForm(string key, DictDetail model)
        {
            try
            {
                if (string.IsNullOrEmpty(key))
                {
                    if (model != null)
                    {
                        model.CreateUser = "测试人员";
                        var res = _dictDetailService.AddDictDetail(model);
                        if (res)
                        {
                            var json = new { type = 1, data = "", msg = "添加完成！", backurl = "" };
                            return Json(json);
                        }
                        else
                        {
                            var json = new { type = 0, data = "", msg = "添加失败！", backurl = "" };
                            return Json(json);
                        }
                    }
                    else
                    {
                        var json = new { type = 2, data = "", msg = "请填写完整数据！", backurl = "" };
                        return Json(json);
                    }
                }
                else
                {
                    var currentmodel = _dictDetailService.GetDictDetailByKey(key);
                    currentmodel.ParentId = model.ParentId;
                    currentmodel.DictTypeId = model.DictTypeId;
                    currentmodel.DictTypeCode = model.DictTypeCode;
                    currentmodel.ItemName = model.ItemName;
                    currentmodel.ItemCode = model.ItemCode;
                    currentmodel.ItemValue = model.ItemValue;
                    currentmodel.IsDefault = model.IsDefault;
                    currentmodel.SortCode = model.SortCode;
                    currentmodel.Status = model.Status;
                    currentmodel.Description = model.Description;
                    currentmodel.ModifyUser = "测试修改人员";
                    var res = _dictDetailService.EditDictDetail(currentmodel);
                    if (res)
                    {
                        var json = new { type = 1, data = "", msg = "编辑完成！", backurl = "" };
                        return Json(json);
                    }
                    else
                    {
                        var json = new { type = 0, data = "", msg = "编辑失败！", backurl = "" };
                        return Json(json);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex, ex.Message);
                throw;
            }
        }

        [HttpGet("dict/getlist")]
        public IActionResult GetDicts(string code)
        {
            var data = _dictDetailService.GerDictDetailList(code);
            return Json(data);
        }

        [HttpGet("dict/getdict")]
        public IActionResult GetModelByKey(string key)
        {
            try
            {
                var model = _dictDetailService.GetDictDetailByKey(key);
                var data = _mapper.Map<DictDetailViewModel>(model);
                return Json(data);
            }
            catch (Exception ex)
            {
                _log.Error(ex, ex.Message);
                throw;
            }
        }

        #endregion

        #region 递归树形结构

        public void GetDictTypeTree(int length, List<DictTypeModel> data, ref List<DictTypeModel> resultData)
        {
            string prefix = string.Empty;
            if (length > 1)
            {
                string nbsp = "&nbsp;";
                for (var i = 0; i < length; i++)
                {
                    nbsp += "&nbsp;&nbsp;";
                }
                prefix = nbsp + "|";
            }
            else
            {
                prefix = "|";
            }
            for (var i = 0; i < length; i++)
            {
                prefix += "-";
            }
            DictTypeModel model;
            foreach (var item in data)
            {
                model = new DictTypeModel();
                model.DictTypeId = item.DictTypeId;
                model.ParentId = item.ParentId;
                model.DictTypeCode = item.DictTypeCode;
                model.DictTypeName = prefix + item.DictTypeName;
                model.IsNav = item.IsNav;
                model.IsLast = item.IsLast;
                model.Status = item.Status;
                model.SortCode = item.SortCode;
                model.IsDelete = item.IsDelete;
                model.Description = item.Description;
                model.CreateUser = item.CreateUser;
                model.CreateTime = item.CreateTime;
                model.ModifyUser = item.ModifyUser;
                model.ModifyTime = item.ModifyTime;
                resultData.Add(model);
                if (item.Children.Any())
                {
                    GetDictTypeTree(length + 1, item.Children, ref resultData);
                }
            }
        }

        public List<DictTypeModel> GetTreeData(string pid, List<DictType> dictTypes)
        {
            List<DictTypeModel> result = new List<DictTypeModel>();
            foreach (var dicttype in dictTypes)
            {
                DictTypeModel node = new DictTypeModel();
                if (!string.IsNullOrEmpty(dicttype.DictTypeId) && dicttype.ParentId == pid)
                {
                    node.DictTypeId = dicttype.DictTypeId;
                    node.ParentId = dicttype.ParentId;
                    node.DictTypeCode = dicttype.DictTypeCode;
                    node.DictTypeName = dicttype.DictTypeName;
                    node.IsNav = dicttype.IsNav;
                    node.IsLast = dicttype.IsLast;
                    node.Status = dicttype.Status;
                    node.SortCode = dicttype.SortCode;
                    node.IsDelete = dicttype.IsDelete;
                    node.Description = dicttype.Description;
                    node.CreateUser = dicttype.CreateUser;
                    node.CreateTime = dicttype.CreateTime;
                    node.ModifyUser = dicttype.ModifyUser;
                    node.ModifyTime = dicttype.ModifyTime;
                    List<DictTypeModel> children = GetTreeData(dicttype.DictTypeId, dictTypes);
                    if (null != children && children.Any())
                    {
                        node.Children = children;
                    }
                    result.Add(node);
                }
            }
            return result;
        }

        #endregion

    }
}