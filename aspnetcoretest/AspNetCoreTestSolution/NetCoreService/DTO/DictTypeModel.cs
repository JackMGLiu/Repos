using System;
using System.Collections.Generic;

namespace NetCoreService.DTO
{
    public class DictTypeModel
    {
        public string DictTypeId { get; set; }
        public string ParentId { get; set; }
        public string DictTypeCode { get; set; }
        public string DictTypeName { get; set; }
        public int? IsNav { get; set; }
        public int? IsLast { get; set; }
        public int? SortCode { get; set; }
        public int Status { get; set; }
        public int IsDelete { get; set; }
        public string Description { get; set; }
        public DateTime? CreateTime { get; set; }
        public string CreateUser { get; set; }
        public DateTime? ModifyTime { get; set; }
        public string ModifyUser { get; set; }
        public List<DictTypeModel> Children { get; set; }

        public DictTypeModel()
        {
            Children = new List<DictTypeModel>();
        }
    }
}
