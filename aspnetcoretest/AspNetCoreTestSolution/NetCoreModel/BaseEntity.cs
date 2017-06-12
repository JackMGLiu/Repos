namespace NetCoreModel
{
    /// <summary>
    /// 实体基类
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// 新增
        /// </summary>
        public virtual void DoCreate()
        {

        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="keyValue">主键值</param>
        public virtual void DoModify(string keyValue)
        {

        }
    }
}
