using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    public class CalendarManag
    {
        public Int64 ri_id { get; set; }
        public string ri_realname { get; set; }
        public string ri_username { get; set; }
        public string  ri_userid { get; set; }
        public string jw_content { get; set; }
        public string  jw_schoolyear { get; set; }
        public string jw_semester { get; set; }
        public int? jw_month { get; set; }
        public int? jw_week { get; set; }
        public string jw_content_view { get; set; }
    }
}
