using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NetCoreModel;
using NetCoreService.DTO.FormatModel;
using NetCoreService.Interface;
using NLog;

namespace WebApplication1.Controllers
{
    public class SysDataDictController : Controller
    {
        protected Logger _log;

        protected IMapper _mapper { get; set; }

        protected IDictTypeService _dictTypeService;

        public SysDataDictController(IMapper mapper, IDictTypeService dictTypeService)
        {
            _log = LogManager.GetCurrentClassLogger();
            _mapper = mapper;
            _dictTypeService = dictTypeService;
        }


        [Route("dict/list")]
        public IActionResult Index()
        {
            return View();
        }

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
                        model.CreateUser = "������Ա";
                        var res = _dictTypeService.AddDictType(model);
                        if (res)
                        {
                            var json = new { type = 1, data = "", msg = "�����ɣ�", backurl = "" };
                            return Json(json);
                        }
                        else
                        {
                            var json = new { type = 0, data = "", msg = "���ʧ�ܣ�", backurl = "" };
                            return Json(json);
                        }
                    }
                    else
                    {
                        var json = new { type = 2, data = "", msg = "����д�������ݣ�", backurl = "" };
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
                    currentmodel.ModifyUser = "�����޸���Ա";
                    var res = _dictTypeService.EditDictType(currentmodel);
                    if (res)
                    {
                        var json = new { type = 1, data = "", msg = "�༭��ɣ�", backurl = "" };
                        return Json(json);
                    }
                    else
                    {
                        var json = new { type = 0, data = "", msg = "�༭ʧ�ܣ�", backurl = "" };
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

        [Route("dict/getlist")]
        public IActionResult GetDictTypes()
        {
            var data = DataTypeList().Where(dt => dt.IsNav == 1).OrderBy(d => d.SortCode).Select(s => new TreeModel
            {
                id = s.DictTypeId,
                code = s.DictTypeCode,
                parentid = s.ParentId,
                name = s.DictTypeName,
                isnav = s.IsNav,
                islast = s.IsLast
            });
            return Json(data);
        }

        #region demo data

        private List<DictType> DataTypeList()
        {
            List<DictType> list = new List<DictType>
            {
                new DictType
                {
                    DictTypeId = "1",
                    ParentId = "0",
                    DictTypeCode = "Basics",
                    DictTypeName = "��������",
                    IsNav = 1,
                    IsLast = 0,
                    SortCode = 0
                },
                new DictType
                {
                    DictTypeId = "2",
                    ParentId = "0",
                    DictTypeCode = "SysDict",
                    DictTypeName = "ϵͳ����",
                    IsNav = 1,
                    IsLast = 0,
                    SortCode = 1
                },
                new DictType
                {
                    DictTypeId = "11",
                    ParentId = "1",
                    DictTypeCode = "Gender",
                    DictTypeName = "�Ա�",
                    IsNav = 1,
                    IsLast = 1,
                    SortCode = 0
                },
                new DictType
                {
                    DictTypeId = "111",
                    ParentId = "11",
                    DictTypeCode = "man",
                    DictTypeName = "��",
                    IsNav = 0,
                    IsLast = 1,
                    SortCode = 1
                },
                new DictType
                {
                    DictTypeId = "112",
                    ParentId = "11",
                    DictTypeCode = "woman",
                    DictTypeName = "Ů",
                    IsNav = 0,
                    IsLast = 1,
                    SortCode = 2
                },
                new DictType
                {
                    DictTypeId = "12",
                    ParentId = "1",
                    DictTypeCode = "education",
                    DictTypeName = "ѧ��",
                    IsNav = 1,
                    IsLast = 1,
                    SortCode = 0
                },
                new DictType
                {
                    DictTypeId = "121",
                    ParentId = "12",
                    DictTypeCode = "xx",
                    DictTypeName = "Сѧ",
                    IsNav = 0,
                    IsLast = 1,
                    SortCode = 1
                },
                new DictType
                {
                    DictTypeId = "122",
                    ParentId = "12",
                    DictTypeCode = "cz",
                    DictTypeName = "����",
                    IsNav = 0,
                    IsLast = 1,
                    SortCode = 2
                },
                new DictType
                {
                    DictTypeId = "123",
                    ParentId = "12",
                    DictTypeCode = "gz",
                    DictTypeName = "����",
                    IsNav = 0,
                    IsLast = 1,
                    SortCode = 3
                },
                new DictType
                {
                    DictTypeId = "124",
                    ParentId = "12",
                    DictTypeCode = "dx",
                    DictTypeName = "��ѧ",
                    IsNav = 0,
                    IsLast = 1,
                    SortCode = 4
                },
                new DictType
                {
                    DictTypeId = "21",
                    ParentId = "2",
                    DictTypeCode = "status",
                    DictTypeName = "״̬",
                    IsNav = 1,
                    IsLast = 1,
                    SortCode = 0
                },
                new DictType
                {
                    DictTypeId = "211",
                    ParentId = "21",
                    DictTypeCode = "enable",
                    DictTypeName = "����",
                    IsNav = 0,
                    IsLast = 1,
                    SortCode = 1
                },
                new DictType
                {
                    DictTypeId = "212",
                    ParentId = "21",
                    DictTypeCode = "unenable",
                    DictTypeName = "����",
                    IsNav = 0,
                    IsLast = 1,
                    SortCode = 2
                }
            };
            return list;
        }

        #endregion
    }
}