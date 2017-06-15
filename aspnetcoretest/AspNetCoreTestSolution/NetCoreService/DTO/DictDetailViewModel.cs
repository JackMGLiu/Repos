using System;

namespace NetCoreService.DTO
{
    public class DictDetailViewModel
    {
        public string DictDetailId { get; set; }
        public string DictTypeId { get; set; }
        public string DictTypeCode { get; set; }
        public string ParentId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemValue { get; set; }
        public int? IsDefault { get; set; }
        public int? SortCode { get; set; }
        public int Status { get; set; }
        public int IsDelete { get; set; }
        public string Description { get; set; }
        public DateTime? CreateTime { get; set; }
        public string CreateUser { get; set; }
        public DateTime? ModifyTime { get; set; }
        public string ModifyUser { get; set; }
    }
}
