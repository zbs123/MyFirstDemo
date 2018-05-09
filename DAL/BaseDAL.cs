using Dapper;
using Entity.MyAttribute;
using Entity.Table;
using IDAL;
using MySqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BaseDAL<T> : IBaseDAL<T> where T : class, new()
    {
        /// <summary>
        /// 表名
        /// </summary>
        protected string tableName = "";
        /// <summary>
        /// Id
        /// </summary>
        protected string keyName = "";

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(T model)
        {
            Type type = model.GetType();
            string sqlText = GetSqlInsert(model);
            int res = new DBHelper().ExecuteNonQuery(sqlText);
            return res > 0 ? true : false;
        }

        /// <summary>
        /// 根据Id删除
        /// </summary>
        /// <param name="argGuid"></param>
        /// <returns></returns>
        public bool Delete(string argGuid)
        {
            using (var db = new DBHelper().CreateConnectionD())
            {
                string sqlText = string.Format("Delete From {0} where {1} = '{2}'", tableName, keyName, argGuid);
                int res = db.Execute(sqlText);  //dapper
                return res > 0 ? true : false;
            }
        }

        /// <summary>
        /// 根据Id删除
        /// </summary>
        /// <param name="argWhere"></param>
        /// <returns></returns>
        public bool DeleteByWhere(string argWhere)
        {
            using (var db = new DBHelper().CreateConnectionD())
            {
                string sqlText = string.Format("Delete From {0} where 1=1 {1}", tableName, argWhere);
                int res = db.Execute(sqlText);
                return res > 0 ? true : false;
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <param name="argKey"></param>
        /// <returns></returns>
        public bool Update(T model, string argKey)
        {
            string sqlText = GetSqlUpdate(model, argKey);
            int res = new DBHelper().ExecuteNonQuery(sqlText);
            return res > 0 ? true : false;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <param name="argWhere"></param>
        /// <returns></returns>
        public bool UpdateByWhere(T model, string argWhere)
        {
            string sqlText = GetSqlUpdate(model, argWhere, "1");
            int res = new DBHelper().ExecuteNonQuery(sqlText);
            return res > 0 ? true : false;
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="listModel"></param>
        public virtual void AddBatch(List<T> listModel)
        {
            try
            {
                List<ri_log> ba = new List<ri_log>();
                if (listModel != null && listModel.Count > 0)
                {
                    T model = listModel.FirstOrDefault();
                    var ps = model.GetType().GetProperties();
                    List<string> @colms = new List<string>();
                    List<string> @params = new List<string>();

                    foreach (var p in ps)
                    {
                        if (!p.CustomAttributes.Any(x => x.AttributeType == typeof(PrimaryKeyAttribute)) && !p.CustomAttributes.Any(x => x.AttributeType == typeof(DBIgnoreAttribute)))
                        {
                            @colms.Add(string.Format("[{0}]", p.Name));
                            @params.Add(string.Format("@{0}", p.Name));
                        }
                    }
                    var sql = string.Format("INSERT INTO [{0}] ({1}) VALUES({2})", typeof(T).Name, string.Join(", ", @colms), string.Join(", ", @params));
                    using (var db = new DBHelper().CreateConnectionD())
                    {
                        db.Execute(sql, listModel, null, null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 根据Id获取模型
        /// </summary>
        /// <param name="argGuid"></param>
        /// <returns></returns>
        public T GetModelById(string argGuid)
        {
            using (var db = new DBHelper().CreateConnectionD())
            {
                string sqlText = string.Format("Select * From {0} where {1} = @guid", tableName, keyName);
                T model = db.QueryFirstOrDefault<T>(sqlText, new { guid = argGuid });
                return model;
            }
        }

        /// <summary>
        /// 根据条件语句查询模型
        /// </summary>
        /// <param name="argWhere"></param>
        /// <returns></returns>
        public T GetModelByWhere(string argWhere)
        {
            using (var db = new DBHelper().CreateConnectionD())
            {
                string sqlText = string.Format("Select * From {0} where 1=1 {1}", tableName, argWhere);
                T model = db.QueryFirstOrDefault<T>(sqlText);
                return model;
            }
        }

        /// <summary>
        /// 根据条件语句查询总数
        /// </summary>
        /// <param name="argWhere"></param>
        /// <returns></returns>
        public int GetCountByWhere(string argWhere)
        {
            DBHelper db = new DBHelper();
            string sqlText = string.Format("Select Count(1) From {0} where 1=1 {1}", tableName, argWhere);
            int res = db.GetScalar(sqlText);
            return res;
        }

        /// <summary>
        /// 根据条件语句查询模型列表
        /// </summary>
        /// <param name="argWhere"></param>
        /// <returns></returns>
        public List<T> GetListByWhere(string argWhere)
        {
            using (var db = new DBHelper().CreateConnectionD())
            {
                string sqlText = string.Format("Select * From {0} where 1=1 {1}", tableName, argWhere);
                new DBHelper().Log(sqlText);
                List<T> model = db.Query<T>(sqlText) as List<T>;
                return model;
            }
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="argWhere"></param>
        /// <param name="pageIndex">从1开始计算，1就是第一页</param>
        /// <param name="pageSize">一页有几个</param>
        /// <returns></returns>
        public List<T> GetListByWhere(string argWhere, int pageIndex, int pageSize)
        {
            using (var db = new DBHelper().CreateConnectionD())
            {
                string sqlText = string.Format("Select * From {0} where 1=1 {1}", tableName, argWhere);
                int from = (pageIndex - 1) * pageSize;
                sqlText += string.Format(" Limit {0},{1}", from, pageSize);
                List<T> model = db.Query<T>(sqlText) as List<T>;
                return model;
            }
        }

        /// <summary>
        /// 将错误信息写入日志文件
        /// </summary>
        /// <param name="strText"></param>
        public void Log(string strText)
        {
            new DBHelper().Log(strText);
        }

        /// <summary>
        /// 将错误信息写入日志文件 多行
        /// </summary>
        /// <param name="strTextLst"></param>
        public void Log(List<string> strTextLst)
        {
            new DBHelper().Log(strTextLst);
        }

        private string GetSqlInsert(T t)
        {
            Type type = t.GetType();
            PropertyInfo[] properties = type.GetProperties();
            string str = "Insert into " + type.Name + " ( ";
            foreach (var proper in properties)
            {
                if (!proper.CustomAttributes.Any(x => x.AttributeType == typeof(PrimaryKeyAttribute)) && !proper.CustomAttributes.Any(x => x.AttributeType == typeof(DBIgnoreAttribute)))
                {
                    str += proper.Name + ",";
                }
            }
            str = str.Substring(0, str.LastIndexOf(","));
            str += " ) values ( ";
            foreach (var proper in properties)
            {
                if (!proper.CustomAttributes.Any(x => x.AttributeType == typeof(PrimaryKeyAttribute)) && !proper.CustomAttributes.Any(x => x.AttributeType == typeof(DBIgnoreAttribute)))
                {
                    object val = proper.GetValue(t, null);
                    if (val is int || val is float || val is decimal || val is double)
                    {
                        str += proper.GetValue(t, null) + ",";
                    }
                    else
                    {
                        if (val == null)
                        {
                            str += "null,";
                        }
                        else
                        {
                            str += "\"" + proper.GetValue(t, null) + "\"" + ",";
                        }

                    }
                }
            }
            str = str.Substring(0, str.LastIndexOf(","));
            str += " )";
            return str;
        }
        private string GetSqlInsertNoSame(T t)
        {
            Type type = t.GetType();
            PropertyInfo[] properties = type.GetProperties();
            string str = "REPLACE into " + type.Name + " ( ";
            foreach (var proper in properties)
            {
                if (!proper.CustomAttributes.Any(x => x.AttributeType == typeof(PrimaryKeyAttribute)) && !proper.CustomAttributes.Any(x => x.AttributeType == typeof(DBIgnoreAttribute)))
                {
                    str += proper.Name + ",";
                }
            }
            str = str.Substring(0, str.LastIndexOf(","));
            str += " ) values ( ";
            foreach (var proper in properties)
            {
                if (!proper.CustomAttributes.Any(x => x.AttributeType == typeof(PrimaryKeyAttribute)) && !proper.CustomAttributes.Any(x => x.AttributeType == typeof(DBIgnoreAttribute)))
                {
                    object val = proper.GetValue(t, null);
                    if (val is int || val is float || val is decimal || val is double)
                    {
                        str += proper.GetValue(t, null) + ",";
                    }
                    else
                    {
                        if (val == null)
                        {
                            str += "null,";
                        }
                        else
                        {
                            str += "\"" + proper.GetValue(t, null) + "\"" + ",";
                        }

                    }
                }
            }
            str = str.Substring(0, str.LastIndexOf(","));
            str += " )";
            return str;
        }

        private string GetSqlUpdate(T t, string argKey, string argtype = "0")
        {
            Type type = t.GetType();
            PropertyInfo[] properties = type.GetProperties();
            string str = "Update " + type.Name + " SET  ";
            foreach (var proper in properties)
            {
                if (!proper.CustomAttributes.Any(x => x.AttributeType == typeof(PrimaryKeyAttribute)) && !proper.CustomAttributes.Any(x => x.AttributeType == typeof(DBIgnoreAttribute)))
                {
                    if (proper.Name == "RI_UpdateTime") continue;
                    str += proper.Name + "=";
                    object val = proper.GetValue(t, null);
                    if (val is int || val is float || val is decimal || val is double)
                    {
                        str += proper.GetValue(t, null) + ",";
                    }
                    else
                    {
                        if (val == null)
                        {
                            str += "null,";
                        }
                        else
                        {
                            str += "\'" + proper.GetValue(t, null) + "\'" + ",";
                        }

                    }
                }
            }
            str = str.Substring(0, str.LastIndexOf(","));
            if (argtype == "1")
            {
                str += " where 1=1 " + argKey;
            }
            else
            {
                str += " where " + keyName + " = " + argKey;
            }
            return str;
        }
        protected List<T> ConvertToModel<T>(DataTable dt) where T : new()
        {

            List<T> ts = new List<T>();// 定义集合
            Type type = typeof(T); // 获得此模型的类型
            string tempName = "";
            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                PropertyInfo[] propertys = t.GetType().GetProperties();// 获得此模型的公共属性
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;
                    if (dt.Columns.Contains(tempName))
                    {
                        if (!pi.CanWrite) continue;
                        object value = dr[tempName];
                        if (value != DBNull.Value)
                        {
                            if (pi.PropertyType == typeof(System.Double))
                            {
                                value = Math.Round(Convert.ToDouble(value.ToString()), 1);
                            }
                            pi.SetValue(t, value, null);
                        }
                    }
                }
                ts.Add(t);
            }
            return ts;
        }

        public bool AddNoSame(T model)
        {
            Type type = model.GetType();
            string sqlText = GetSqlInsertNoSame(model);
            new DBHelper().Log(sqlText);
            int res = new DBHelper().ExecuteNonQuery(sqlText);
            return res > 0 ? true : false;
        }
    }
}
