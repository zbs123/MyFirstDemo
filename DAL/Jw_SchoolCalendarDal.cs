using Entity.Table;
using Entity.ViewModel;
using IDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Jw_SchoolCalendarDal : BaseDAL<jw_schoolcalendar>,IJw_SchoolCalendarDal
    {
        public Jw_SchoolCalendarDal():base()
        {
            this.tableName = "jw_schoolcalendar";
            this.keyName = "jw_id";
        }

       
        public List<CalendarManag> GetList(string anywhere)
        {
            string sql =string.Format("SELECT u.ri_id,u.RI_UserId,u.RI_UserName,u.RI_RealName,jc.* from ri_user u LEFT JOIN jw_calendar jc ON u.RI_UserName=jc.jw_UserName "+anywhere);
           
            try
            {
                return ConvertToModel<CalendarManag>(new DBHelper().ExecuteQuery(sql));
            }
            catch (Exception ex)
            {
                new DBHelper().Log(sql);

                throw ex;
            }

        }
        public string GetUserid(string tel)
        {
            if (string.IsNullOrEmpty(tel))
            {
                return "";
            }
            string sql = "select ri_userid from ri_user where ri_idnumber='" + tel + "'";
            try
            {
                DataTable dt = new DBHelper().ExecuteQuery(sql);
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0][0].ToString();
                }
                return "";
            }
            catch (Exception ex)
            {
                new DBHelper().Log(sql);
                throw ex;
            }
        }
    }
}
