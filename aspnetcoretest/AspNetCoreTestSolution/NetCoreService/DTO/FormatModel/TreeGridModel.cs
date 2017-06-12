using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreService.DTO.FormatModel
{
    public class TreeGridModel
    {
        public TreeGridModel()
        {
            Children = new List<TreeGridModel>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public List<TreeGridModel> Children { get; set; }
    }
}
