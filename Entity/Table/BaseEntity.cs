using Entity.MyAttribute;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Table
{
    /// <summary>
    /// 基类
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseEntity()
        {
            RS_Guid = Guid.NewGuid().ToString("N");
            RS_UpdateTime = DateTime.Now;
        }

        /// <summary>
        /// GUID
        /// </summary>
        public string RS_Guid { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime RS_CreateTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DBIgnore]
        public DateTime RS_UpdateTime { get; set; }

        /// <summary>
        /// 0：正常，1：已删除
        /// </summary>
        public string RS_DelFlag { get; set; }

    }
}
