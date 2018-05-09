using Entity.MyAttribute;
using System;
using System.Linq;
using System.Text;

namespace Entity.Table
{
    public class Jw_calendar
    {
        [PrimaryKey]
        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int jw_Id { get; set; }

        /// <summary>
        /// Desc:学校的guid 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string jw_SchoolId { get; set; }

        /// <summary>
        /// Desc:用户的username 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string jw_UserName { get; set; }

        /// <summary>
        /// Desc:学年 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string jw_SchoolYear { get; set; }

        /// <summary>
        /// Desc:学期 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string jw_Semester { get; set; }

        /// <summary>
        /// Desc:所在月 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? jw_Month { get; set; }

        /// <summary>
        /// Desc:所在周 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? jw_Week { get; set; }

        /// <summary>
        /// Desc:内容 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string jw_Content { get; set; }

        /// <summary>
        /// Desc:创建日期 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? jw_CreateDate { get; set; }

        /// <summary>
        /// Desc:修改日期 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? jw_UpdateDate { get; set; }

        /// <summary>
        /// Desc:删除标记，1正常， 0 已删除 
        /// Default:1 
        /// Nullable:True 
        /// </summary>
        public string jw_DelFlag { get; set; }

    }
}
