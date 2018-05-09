using Entity.MyAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Table
{
    /// <summary>
    /// 日志
    /// </summary>
    public class ri_log
    {
        [PrimaryKey]
        public int RI_ID { get; set; }
        public string RI_UserName { get; set; }
        public string RI_Option { get; set; }
        public string RI_Content { get; set; }
        [DBIgnore]
        public DateTime RI_CreateTime { get; set; }
    }
}
