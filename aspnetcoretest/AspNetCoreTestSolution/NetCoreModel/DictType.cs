using System;
using Dapper.Contrib.Extensions;

namespace NetCoreModel
{
    [Table("DictType")]
    public class DictType:BaseEntity
    {
        [ExplicitKey]
        public string DictTypeId { get; set; }
        public string ParentId { get; set; }
        public string DictTypeCode { get; set; }
        public string DictTypeName { get; set; }
        public int? IsNav { get; set; }
        public int? IsLast { get; set; }
        public int? SortCode { get; set; }
        public int Status { get; set; }
        public int IsDelete { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public DateTime? CreateTime { get; set; }
        public string CreateUser { get; set; }
        public DateTime? ModifyTime { get; set; }
        public string ModifyUser { get; set; }

        #region 扩展操作

        /// <summary>
        /// 新增调用
        /// </summary>
        public override void DoCreate()
        {
            this.DictTypeId = Guid.NewGuid().ToString();
            this.CreateTime = DateTime.Now;
            this.IsDelete = 0;
            //this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            //this.CreateUserName = OperatorProvider.Provider.Current().UserName;
        }

        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void DoModify(string keyValue)
        {
            this.DictTypeId = keyValue;
            this.ModifyTime = DateTime.Now;
            //this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            //this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }

        #endregion
    }
}
