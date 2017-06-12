using System;
using Dapper.Contrib.Extensions;

namespace NetCoreModel
{
    [Table("SysUser")]
    public class SysUser: BaseEntity
    {
        [ExplicitKey]
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string RealName { get; set; }
        public string NickName { get; set; }
        public string HeadImg { get; set; }
        public int? Age { get; set; }
        public int? Gender { get; set; }
        public int? Nation { get; set; }
        public DateTime? BirthDay { get; set; }
        public string CardId { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string QQ { get; set; }
        public string WeChat { get; set; }
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
            this.UserId = Guid.NewGuid().ToString();
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
            this.UserId = keyValue;
            this.ModifyTime = DateTime.Now;
            //this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            //this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }

        #endregion
    }
}