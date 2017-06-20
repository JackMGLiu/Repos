using System;
using Dapper.Contrib.Extensions;

namespace NetCoreModel
{
    [Table("NewsType")]
    public class NewsType : BaseEntity
    {
        [ExplicitKey]
        public string NewsTypeId { get; set; }
        public string TypeName { get; set; }
        public string TypeValue { get; set; }
        public string Img { get; set; }
        public int Status { get; set; }
        public int IsDelete { get; set; }
        public int? SortCode { get; set; }
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
            this.NewsTypeId = Guid.NewGuid().ToString();
            this.CreateTime = DateTime.Now;
            //this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            //this.CreateUserName = OperatorProvider.Provider.Current().UserName;
            this.IsDelete = 0;
            //this.Status = 1;
        }

        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void DoModify(string keyValue)
        {
            this.NewsTypeId = keyValue;
            this.ModifyTime = DateTime.Now;
            //this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            //this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }

        #endregion 
    }
}
