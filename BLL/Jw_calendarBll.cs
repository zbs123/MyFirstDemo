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
   public class Jw_calendarBll : BaseBll<Jw_calendar>,IJw_calendarBll
    {
        private IJw_calendarDal _dal;
        public Jw_calendarBll(IJw_calendarDal dal) : base(dal)
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
