using Entity.Table;
using Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
   public interface IJw_calendarBll : IBaseBll<Jw_calendar>
    {
        string GetUserid(string tel);
      
        List<CalendarManag> GetList(string anywhere);
    }
}
