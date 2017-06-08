using System.Collections.Generic;

namespace NetCoreData.Models
{
    /// <summary>
    /// 分页结果类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageDataView<T>
    {
        private int _totalNum;

        public PageDataView()
        {
            this._items = new List<T>();
        }

        public int TotalNum
        {
            get { return _totalNum; }
            set { _totalNum = value; }
        }

        private IList<T> _items;
        public IList<T> Items
        {
            get { return _items; }
            set { _items = value; }
        }

        public int CurrentPage { get; set; }

        public int TotalPageCount { get; set; }
    }
}
