using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreModel;
using NetCoreService.DTO.FormatModel;

namespace WebApplication1.Controllers
{
    public class SysDataDictController : Controller
    {
        [Route("dict/list")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("dict/getlist")]
        public IActionResult GetDictTypes()
        {
            var data = DataTypeList().Where(dt=>dt.IsNav==1).OrderBy(d => d.SortCode).Select(s => new TreeModel
            {
                id = s.DictTypeId,
                code = s.DictTypeCode,
                parentid = s.ParentId,
                name = s.DictTypeName,
                isnav = s.IsNav
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
                    SortCode = 0
                },
                new DictType
                {
                    DictTypeId = "2",
                    ParentId = "0",
                    DictTypeCode = "SysDict",
                    DictTypeName = "ϵͳ����",
                    IsNav = 1,
                    SortCode = 1
                },
                new DictType
                {
                    DictTypeId = "11",
                    ParentId = "1",
                    DictTypeCode = "Gender",
                    DictTypeName = "�Ա�",
                    IsNav = 1,
                    SortCode = 0
                },
                new DictType
                {
                    DictTypeId = "111",
                    ParentId = "11",
                    DictTypeCode = "man",
                    DictTypeName = "��",
                    IsNav = 0,
                    SortCode = 1
                },
                new DictType
                {
                    DictTypeId = "112",
                    ParentId = "11",
                    DictTypeCode = "woman",
                    DictTypeName = "Ů",
                    IsNav = 0,
                    SortCode = 2
                },
                new DictType
                {
                    DictTypeId = "12",
                    ParentId = "1",
                    DictTypeCode = "education",
                    DictTypeName = "ѧ��",
                    IsNav = 1,
                    SortCode = 0
                },
                new DictType
                {
                    DictTypeId = "121",
                    ParentId = "12",
                    DictTypeCode = "xx",
                    DictTypeName = "Сѧ",
                    IsNav = 0,
                    SortCode = 1
                },
                new DictType
                {
                    DictTypeId = "122",
                    ParentId = "12",
                    DictTypeCode = "cz",
                    DictTypeName = "����",
                    IsNav = 0,
                    SortCode = 2
                },
                new DictType
                {
                    DictTypeId = "123",
                    ParentId = "12",
                    DictTypeCode = "gz",
                    DictTypeName = "����",
                    IsNav = 0,
                    SortCode = 3
                },
                new DictType
                {
                    DictTypeId = "124",
                    ParentId = "12",
                    DictTypeCode = "dx",
                    DictTypeName = "��ѧ",
                    IsNav = 0,
                    SortCode = 4
                },
                new DictType
                {
                    DictTypeId = "21",
                    ParentId = "2",
                    DictTypeCode = "status",
                    DictTypeName = "״̬",
                    IsNav = 1,
                    SortCode = 0
                },
                new DictType
                {
                    DictTypeId = "211",
                    ParentId = "21",
                    DictTypeCode = "enable",
                    DictTypeName = "����",
                    IsNav = 0,
                    SortCode = 1
                },
                new DictType
                {
                    DictTypeId = "212",
                    ParentId = "21",
                    DictTypeCode = "unenable",
                    DictTypeName = "����",
                    IsNav = 0,
                    SortCode = 2
                }
            };
            return list;
        }

        #endregion
    }
}