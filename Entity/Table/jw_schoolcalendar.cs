using Entity.MyAttribute;
using System;
using System.Linq;
using System.Text;

namespace Entity.Table
{
    public class jw_schoolcalendar
    {
        [PrimaryKey]
        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int jw_id {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string jw_schoolid {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string jw_schoolYear {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string jw_semester {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? jw_month {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? jw_day {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string jw_note {get;set;}

        /// <summary>
        /// Desc:0正常1补课2放假3特殊 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string jw_contentType {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? jw_startTime {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? jw_endTime {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? jw_createTime {get;set;}
        public string jw_contentDesc { get; set; }
        public string jw_type { get; set; }
        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string jw_delflag {get;set;}
        [DBIgnore]
        public string strStartTime
        {
            get
            {
                if (this.jw_startTime != null)
                return this.jw_startTime.Value.ToString("yyyy-MM-dd");
                return "";
            }
        }
        [DBIgnore]
        public dynamic strEndTime
        {
            get
            {
                if(this.jw_endTime!=null)
                return this.jw_endTime.Value.ToString("yyyy-MM-dd");
                return "";
            }
        }

    }
}
