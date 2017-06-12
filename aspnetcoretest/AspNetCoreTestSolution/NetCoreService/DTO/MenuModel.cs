using System;
using System.Collections.Generic;

namespace NetCoreService.DTO
{
    public class MenuModel
    {
        public string MenuId { get; set; }
        public string ParentId { get; set; }
        public string MenuName { get; set; }
        public string LinkUrl { get; set; }
        public string Icon { get; set; }
        public int IsHeader { get; set; }
        public int Status { get; set; }
        public int IsDelete { get; set; }
        public int? SortCode { get; set; }
        public string Description { get; set; }
        public DateTime? CreateTime { get; set; }
        public string CreateUser { get; set; }
        public DateTime? ModifyTime { get; set; }
        public string ModifyUser { get; set; }
        public List<MenuModel> Children { get; set; }

        public MenuModel()
        {
            Children = new List<MenuModel>();
        }
    }
}
