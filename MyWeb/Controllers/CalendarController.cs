using Entity.Table;
using Entity.ViewModel;
using IBLL;
using MyWeb.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWeb.Controllers
{
    public class CalendarController : Controller
    {
        private IJw_calendarBll _xwBll;
        private IJw_SchoolCalendarBll _jwSchoolBll;
        public CalendarController(IJw_calendarBll xwBll, IJw_SchoolCalendarBll jwSchoolBll)
        {
            _xwBll = xwBll;
            _jwSchoolBll = jwSchoolBll;
        }
        // GET: Calendar
        public ActionResult Index()
        {
            ViewBag.now = DateTime.Now.ToString("MM月dd日");
            ViewBag.week = GetWeek();
            return View();
        }
        private string GetWeek()
        {
            string[] weekdays = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            string week = weekdays[Convert.ToInt32(DateTime.Now.DayOfWeek)];
            return week;
        }
        //教学周计划
        public ActionResult GetData1(string year, string semester)
        {
            if (string.IsNullOrEmpty(semester))
            {
                if (DateTime.Now.Month == 1 || DateTime.Now.Month > 8)
                {
                    semester = "1";
                }
                else
                {
                    semester = "2";
                }
            }
            int[] sarr = new int[] { 9, 10, 11, 12, 1 };
            if (semester != "1")
            {
                sarr = new int[] { 3, 4, 5, 6, 7 };
            }
            if (string.IsNullOrEmpty(year))
            {
                year = DateTime.Now.Year.ToString();
            }
            HttpCookie cookie = Request.Cookies["Gaokao"];
            string username = cookie["UserId"].ToString();
            string schoolid = cookie["SchoolId"].ToString();
            //List<Jw_calendar> list = _xwBll.GetListByWhere("and jw_schoolid='" + schoolid + "' and jw_username='" + username + "' and jw_schoolyear='2017' and jw_semester='1' and jw_delflag='1'");
            List<Jw_calendar> list = _xwBll.GetListByWhere("and jw_schoolid='" + schoolid + "'  and jw_username='" + username + "' and jw_schoolyear='" + year + "' and jw_semester='" + semester + "' and jw_delflag='1'");
            string tableStr = GetTableStr(year, semester, sarr, (i) =>
              {
                  string tdStr = "<td class='calendar' style='width: 18%; text-align:left; cursor: pointer; ' data-year='" + year + "' data-semeter='" + semester + "' data-week='" + i + "'";
                  string str = "<tr>";
                  str += "<td style='height: 150px; width: 10%; vertical-align:middle !important'>第" + i + "周</td>";

                  var m1 = GetListStr(list.Where(c => c.jw_Month == sarr[0] && c.jw_Week == i && c.jw_DelFlag == "1").ToList());
                  str += tdStr + "data-month='" + sarr[0] + "' data-html='" + m1 + "'>" + StringOpe(m1) + "</td>";

                  var m2 = GetListStr(list.Where(c => c.jw_Month == sarr[1] && c.jw_Week == i && c.jw_DelFlag == "1").ToList());
                  str += tdStr + "data-month='" + sarr[1] + "' data-html='" + m2 + "'>" + StringOpe(m2) + "</td>";

                  var m3 = GetListStr(list.Where(c => c.jw_Month == sarr[2] && c.jw_Week == i && c.jw_DelFlag == "1").ToList());
                  str += tdStr + "data-month='" + sarr[2] + "' data-html='" + m3 + "'>" + StringOpe(m3) + "</td>";

                  var m4 = GetListStr(list.Where(c => c.jw_Month == sarr[3] && c.jw_Week == i && c.jw_DelFlag == "1").ToList());
                  str += tdStr + "data-month='" + sarr[3] + "' data-html='" + m4 + "'>" + StringOpe(m4) + "</td>";

                  var m5 = GetListStr(list.Where(c => c.jw_Month == sarr[4] && c.jw_Week == i && c.jw_DelFlag == "1").ToList());
                  str += tdStr + "data-month='" + sarr[4] + "' data-html='" + m5 + "'>" + StringOpe(m5) + "</td>";
                  str += "</tr>";
                  return str;
              });

            return Content(tableStr);
        }
        //添加周计划
        public ActionResult Calendar(string year, string month, string week, string semeter, string content)
        {
            HttpCookie cookie = Request.Cookies["Gaokao"];
            string username = cookie["UserId"].ToString();
            string schoolid = cookie["SchoolId"].ToString();
            List<Jw_calendar> jc = _xwBll.GetListByWhere(" and jw_schoolid='" + schoolid + "' and jw_username='" + username + "' and jw_schoolyear='" + year + "' and jw_semester='" + semeter + "' and jw_month='" + month + "' and jw_week='" + week + "'");
            bool flag;
            if (jc.Count > 0)
            {
                jc[0].jw_Content = content;
                flag = _xwBll.UpdateByWhere(jc[0], " and jw_schoolid='" + schoolid + "' and jw_username='" + username + "' and jw_schoolyear='" + year + "' and jw_semester='" + semeter + "' and jw_month='" + month + "' and jw_week='" + week + "'");
            }
            else
            {
                
                Jw_calendar jcn = new Jw_calendar();
                jcn.jw_SchoolId = schoolid;
                jcn.jw_UserName = username;
                jcn.jw_SchoolYear = year;
                jcn.jw_Semester = semeter;
                jcn.jw_Month = Convert.ToInt32(month);
                jcn.jw_Week = Convert.ToInt32(week);
                jcn.jw_Content = content;
                jcn.jw_CreateDate = DateTime.Now;
                jcn.jw_UpdateDate = DateTime.Now;
                jcn.jw_DelFlag = "1";
                flag = _xwBll.Add(jcn);
            }
            if (flag)
            {
                return Content(ConvertJson.ToJson(200, "", "{}"));
            }
            return Content(ConvertJson.ToJson(201, "", "{}"));
        }
        //获取list里的第一个值
        private string GetListStr(List<Jw_calendar> list)
        {
            if (list.Count > 0)
            {
                return list[0].jw_Content;
            }
            return "";
        }
        //处理字符串
        private string StringOpe(string str)
        {
            if (str.Length > 100)
            {
                return str.Substring(0, 100) + "...";
            }
            return str;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="semeter"></param>
        /// <param name="sarr"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        private string GetTableStr(string year, string semeter, int[] sarr, Func<int, string> func)
        {
            string tStr = "<table class='layui-table'><tr>";
            tStr += "<td></td><td>" + year + "年/" + sarr[0] + "月</td><td>" + year + "年/" + sarr[1] + "月</td><td>" + year + "年/" + sarr[2] + "月</td><td>" + year + "年/" + sarr[3] + "月</td><td>" + year + "年/" + sarr[4] + "月</td></tr>";
            for (int i = 0; i < 4; i++)
            {
                tStr += func(i + 1);
            }
            tStr += "</table>";
            return tStr;
        }
        public ActionResult CalendarManag()
        {
            ViewBag.now = DateTime.Now.ToString("MM月dd日");
            ViewBag.week = GetWeek();
            return View();
        }
        //周计划统计数据
        public ActionResult GetData(string year, string semester, int page, int limit, string search)
        {
            string anywhere = string.Empty;
            if (!string.IsNullOrEmpty(search))
            {
                anywhere += "where u.ri_realname like '%" + search + "%' or u.ri_tel='" + search + "'";
            }
            int[] sarr = GetMonthBySemester(semester);
            List<CalendarManag> list = _xwBll.GetList(anywhere);
            var query = from g in list
                        group g by new { t1 = g.ri_realname } into companys
                        select new { DeptNo = companys.Key.t1, StallInfo = companys };
            List<CalendarManagView> vlist = new List<CalendarManagView>();

            foreach (var userInfo in query.Take(limit * page).Skip(limit * (page - 1)).ToList())
            {
                List<CalendarManag> list1 = userInfo.StallInfo.ToList();
                List<CalendarManag> cmlist = new List<CalendarManag>();
                CalendarManagView cmv = new CalendarManagView();
                cmv.ri_id = list1[0].ri_id;
                cmv.ri_realname = list1[0].ri_realname;
                cmv.jw_content = list1[0].jw_content;
                for (int i = 0; i < sarr.Length; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        cmlist.Add(GetCalendar(list1, sarr[i], j + 1, year, semester));
                    }
                }
                cmv.cmlist = cmlist;

                vlist.Add(cmv);
            }


            var data = JsonConvert.SerializeObject(vlist);
            string json = ConvertJson.ToJson("200", "", query.Count().ToString(), data);
            return Content(json);

        }
        //获取表头数据
        public ActionResult GetHead(string year, string semester)
        {
            if (string.IsNullOrEmpty(semester))
            {
                if (DateTime.Now.Month == 1 || DateTime.Now.Month > 8)
                {
                    semester = "1";
                }
                else
                {
                    semester = "2";
                }
            }
            int[] sarr = new int[] { 9, 10, 11, 12, 1 };
            if (semester != "1")
            {
                sarr = new int[] { 3, 4, 5, 6, 7 };
            }
            if (string.IsNullOrEmpty(year))
            {
                year = DateTime.Now.Year.ToString();
            }
            var data = JsonConvert.SerializeObject(sarr);
            var json = ConvertJson.ToJson("200", "", "", data, semester, year);
            return Content(json);

        }
        //根据学期获取月份
        private int[] GetMonthBySemester(string semester)
        {
            if (semester == "1")
            {
                return new int[] { 9, 10, 11, 12, 1 };
            }
            else
            {
                return new int[] { 3, 4, 5, 6, 7 };
            }
        }
        private CalendarManag GetCalendar(List<CalendarManag> list, int month, int week, string year, string semester)
        {
            return new CalendarManag { jw_content_view = IsContent(list, month, week, year, semester), jw_month = month, jw_week = week, jw_schoolyear = year, jw_semester = semester, jw_content = GetContent(list, month, week, year, semester) };
        }
        //判断是否已写周计划
        private string IsContent(List<CalendarManag> list, int month, int week, string year, string semester)
        {
            if (list.Where((cm) => cm.jw_schoolyear == year && cm.jw_semester == semester && cm.jw_month == month && cm.jw_week == week).ToList().Count > 0)
            {
                return "已写";
            }
            return "--";
        }
        //获取周计划内容
        private string GetContent(List<CalendarManag> list, int month, int week, string year, string semester)
        {
            var l = list.Where((cm) => cm.jw_schoolyear == year && cm.jw_semester == semester && cm.jw_month == month && cm.jw_week == week).ToList();
            if (l.Count > 0)
            {
                return l[0].jw_content;
            }
            return "";
        }

        public ActionResult SchoolCalendar()
        {
            HttpCookie cookie = Request.Cookies["Gaokao"];
            string role = cookie["type"].ToString();
            ViewBag.roletype = role;
            return View();
        }
        //添加校历
        public ActionResult AddSchoolCalendar(jw_schoolcalendar jsc,string new_m,string new_d)
        {
            HttpCookie cookie = Request.Cookies["Gaokao"];
            string username = cookie["Name"].ToString();

            string schoolid = cookie["SchoolId"].ToString();
            jsc.jw_createTime = DateTime.Now;
            jsc.jw_delflag = "1";
            jsc.jw_type = "1";
            jsc.jw_schoolid = schoolid;
            
            string where = " and jw_schoolid='" + jsc.jw_schoolid + "' and jw_schoolyear='" + jsc.jw_schoolYear + "' and jw_semester='" + jsc.jw_semester + "' and jw_month='" + jsc.jw_month + "' and jw_day='" + jsc.jw_day + "' and jw_delflag='1'";
            List<jw_schoolcalendar> list = _jwSchoolBll.GetListByWhere(where);
            if (!string.IsNullOrEmpty(new_m))
            {
                jsc.jw_day = Convert.ToInt32(new_d);
                jsc.jw_month = Convert.ToInt32(new_m);
            }
            if (list.Count > 0)
            {
                _jwSchoolBll.UpdateByWhere(jsc, where);

            }
            else
            {
                _jwSchoolBll.Add(jsc);

            }
            var json = ConvertJson.ToJson(200, "", "{}");
            return Content(json);

        }
        //添加校历说明
        public ActionResult AddSchoolCalendarDesc(jw_schoolcalendar jsc)
        {
            HttpCookie cookie = Request.Cookies["Gaokao"];
            string username = cookie["Name"].ToString();

            string schoolid = cookie["SchoolId"].ToString();
            jsc.jw_createTime = DateTime.Now;
            jsc.jw_delflag = "1";
            jsc.jw_type = "2";
            jsc.jw_schoolid = schoolid;
            string where = " and jw_schoolid='" + jsc.jw_schoolid + "' and jw_schoolyear='" + jsc.jw_schoolYear + "' and jw_semester='" + jsc.jw_semester + "' and jw_month='" + jsc.jw_month + "' and jw_type='2'  and jw_delflag='1'";
            List<jw_schoolcalendar> list = _jwSchoolBll.GetListByWhere(where);
            if (list.Count > 0)
            {
                _jwSchoolBll.UpdateByWhere(jsc, where);

            }
            else
            {
                _jwSchoolBll.Add(jsc);

            }
            var json = ConvertJson.ToJson(200, "", "{}");
            return Content(json);

        }
        //获取校历数据
        public ActionResult GetSchoolCalendar(jw_schoolcalendar jsc)
        {
            HttpCookie cookie = Request.Cookies["Gaokao"];
            string username = cookie["Name"].ToString();
            string schoolid = cookie["SchoolId"].ToString();
            jsc.jw_schoolid = schoolid;
            List<jw_schoolcalendar> list = _jwSchoolBll.GetListByWhere(" and jw_schoolid='" + jsc.jw_schoolid + "' and jw_schoolyear='" + jsc.jw_schoolYear + "' and jw_semester='" + jsc.jw_semester + "' and jw_delflag='1'");
            var data = JsonConvert.SerializeObject(list);
            var json = ConvertJson.ToJson(200, "", data);
            return Content(json);
        }
        //导出图片
        public ActionResult ExportImg(string imgData)
        {
            string json = "";
            if (string.IsNullOrWhiteSpace(imgData))
            {
                json = ConvertJson.ToJson(20001, "", "{}");
                return Content(json);
            }
            try
            {
                string fname= DateTime.Now.ToString("yyMMddHHmmssfff") ;
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "/File/"+ fname + ".jpg";
                byte[] arr = Convert.FromBase64String(imgData.Substring(imgData.IndexOf("base64,") + 7).Trim('\0'));
                using (MemoryStream ms = new MemoryStream(arr))
                {
                    Bitmap bmp = new Bitmap(ms);
                    //新建第二个bitmap类型的bmp2变量。
                    Bitmap bmp2 = new Bitmap(bmp, bmp.Width, bmp.Height);
                    //将第一个bmp拷贝到bmp2中
                    Graphics draw = Graphics.FromImage(bmp2);
                    draw.DrawImage(bmp, 0, 0);
                    draw.Dispose();
                    bmp2.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                    ms.Close();
                }
                json = ConvertJson.ToJson("200", "", fname);
                return Content(json);
            }
            catch (Exception ex)
            {
                json = ConvertJson.ToJson(20001, "", "{}");
                return Content(json);
            }
            
        }
        
        public ActionResult Open1(string url)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + "/File/" + url + ".jpg";
            return File(path, "iamge/jpeg","校历.jpg");
        }
    }
}