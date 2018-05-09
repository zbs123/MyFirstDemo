using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    public class CalendarManagView
    {
        public Int64 ri_id { get; set; }
        public string ri_realname { get; set; }
        public string jw_content { get; set; }
        public List<CalendarManag> cmlist { get; set; }

    }
}
