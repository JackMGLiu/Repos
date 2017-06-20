using System;
using Dapper.Contrib.Extensions;

namespace NetCoreModel
{
    [Table("NewsInfo")]
    public class NewsInfo : BaseEntity
    {
        /// <summary>
        /// 主键编号
        /// </summary>
        public string NewsId { get; set; }

        /// <summary>
        /// 主标题
        /// </summary>
        public string NewsTitle { get; set; }

        /// <summary>
        /// 副标题
        /// </summary>
        public string SubTitle { get; set; }

        /// <summary>
        /// 新闻类别
        /// </summary>
        public string NewsTypeId { get; set; }

        /// <summary>
        /// 是否有标题图 1：有，0：无
        /// </summary>
        public int? IsPic { get; set; }

        /// <summary>
        /// 标题图路径
        /// </summary>
        public string PicUrl { get; set; }

        /// <summary>
        /// 新闻内容
        /// </summary>
        public string PushContent { get; set; }

        /// <summary>
        /// 非HTML新闻内容
        /// </summary>
        public string PushNoHtmlContent { get; set; }

        /// <summary>
        /// 是否定时推送
        /// </summary>
        public int? IsTimingPush { get; set; }

        /// <summary>
        /// 推送时间
        /// </summary>
        public DateTime? PushTime { get; set; }

        /// <summary>
        /// 是否开启提醒
        /// </summary>
        public int? IsRemind { get; set; }

        /// <summary>
        /// 提醒时间
        /// </summary>
        public DateTime? RemindTime { get; set; }

        /// <summary>
        /// 新闻有效开始时间
        /// </summary>
        public DateTime? EnableStartTime { get; set; }

        /// <summary>
        /// 新闻有效结束时间
        /// </summary>
        public DateTime? EnableEndTime { get; set; }

        /// <summary>
        /// 是否头条
        /// </summary>
        public int? IsStick { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public int? IsHeadLine { get; set; }

        /// <summary>
        /// 消息级别 1 一般 2 重要 3 紧急 
        /// </summary>
        public int? InfoLevel { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        public string KeyWords { get; set; }

        /// <summary>
        /// 新闻发布时间
        /// </summary>
        public DateTime? PublishTime { get; set; }

        public int NewsStatus { get; set; }

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
            this.NewsId = Guid.NewGuid().ToString();
            this.CreateTime = DateTime.Now;
            //this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            //this.CreateUserName = OperatorProvider.Provider.Current().UserName;
            this.IsDelete = 0;
            this.IsTimingPush = 0;
            this.IsStick = 0;
            //this.Status = 1;
        }

        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void DoModify(string keyValue)
        {
            this.NewsId = keyValue;
            this.ModifyTime = DateTime.Now;
            //this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            //this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }

        #endregion 

     }
}
