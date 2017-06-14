using System;
using Dapper.Contrib.Extensions;

namespace NetCoreModel
{
    [Table("DictDetail")]
    public class DictDetail : BaseEntity
    {
        [ExplicitKey]
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

        #region 扩展操作

        /// <summary>
        /// 新增调用
        /// </summary>
        public override void DoCreate()
        {
            this.DictDetailId = Guid.NewGuid().ToString();
            this.CreateTime = DateTime.Now;
            this.ParentId = "0";
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
            this.DictDetailId = keyValue;
            this.ModifyTime = DateTime.Now;
            //this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            //this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }

        #endregion
    }
}