namespace NetCoreModel
{
    public class SysMenu
    {
        public string MenuId { get; set; }

        public string ParentId { get; set; }

        public string MenuName { get; set; }

        public string LinkUrl { get; set; }

        public string Icon { get; set; }

        public int IsHeader { get; set; }

        public int? SortCode { get; set; }
    }
}