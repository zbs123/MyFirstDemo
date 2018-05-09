using Entity.Table;
using Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
   public interface IJw_SchoolCalendarBll : IBaseBll<jw_schoolcalendar>
    {
        string GetUserid(string tel);
      
        List<CalendarManag> GetList(string anywhere);
    }
}
