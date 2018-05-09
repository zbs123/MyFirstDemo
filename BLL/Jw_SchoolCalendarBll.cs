using Entity.Table;
using Entity.ViewModel;
using IBLL;
using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
   public class Jw_SchoolCalendarBll : BaseBll<jw_schoolcalendar>,IJw_SchoolCalendarBll
    {
        private IJw_SchoolCalendarDal _dal;
        public Jw_SchoolCalendarBll(IJw_SchoolCalendarDal dal) : base(dal)
        {
            _dal = dal;
        }
        public string GetUserid(string tel)
        {
            return _dal.GetUserid(tel);
        }
       
        public List<CalendarManag> GetList(string anywhere)
        {
            return _dal.GetList(anywhere);
        }
    }
}
