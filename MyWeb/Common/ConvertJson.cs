using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MyWeb.Common
{
    public class ConvertJson
    {
        public static string ToJson(string code, string msg, string count, string data)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"code\":\"" + code + "\",");
            sb.Append("\"msg\":\"" + msg + "\",");
            sb.Append("\"count\":\"" + count + "\",");
            sb.Append("\"data\":" + data + "}");

            return sb.ToString();
        }
        public static string ToJson(string code, string msg, string count, string data,string semeterarr,string year)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"code\":\"" + code + "\",");
            sb.Append("\"msg\":\"" + msg + "\",");
            sb.Append("\"count\":\"" + count + "\",");
            sb.Append("\"data\":" + data + ",");
            sb.Append("\"semeter\":\"" + semeterarr + "\",");
            sb.Append("\"year\":\"" + year + "\"}");

            return sb.ToString();
        }
        public static string ToJson(int code, string msg, int count, string data)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"code\":" + code + ",");
            sb.Append("\"msg\":\"" + msg + "\",");
            sb.Append("\"count\":" + count + ",");
            sb.Append("\"data\":" + data + "}");

            return sb.ToString();
        }
        public static string ToJson(int code, string msg, string data)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"code\":" + code + ",");
            sb.Append("\"msg\":\"" + msg + "\",");
            sb.Append("\"data\":" + data + "}");

            return sb.ToString();
        }
        public static string ToJson(int code, string msg, string data,string data1)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"code\":" + code + ",");
            sb.Append("\"msg\":\"" + msg + "\",");
            sb.Append("\"data\":" + data + ",");
            sb.Append("\"data1\":\"" + data1 + "\"}");

            return sb.ToString();
        }
        public static string ToJson(string code, string msg, string data)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"code\":" + code + ",");
            sb.Append("\"msg\":\"" + msg + "\",");
            sb.Append("\"data\":\"" + data + "\"}");

            return sb.ToString();
        }
    }
}