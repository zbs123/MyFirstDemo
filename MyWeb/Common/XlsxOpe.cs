using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NPOI;
using Entity.Table;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;

namespace MyWeb.Common
{
    public class XlsxOpe
    {
       /// <summary>
       /// 将一行作为一个对象操作
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <param name="path">xlsx文件路径</param>
       /// <param name="sheetnum">表从0开始</param>
       /// <param name="rownum">行</param>
       /// <param name="cellnum">列</param>
       /// <param name="Fun">操作</param>
       /// <returns></returns>
        public static List<T> ReadXlsx<T>(string path,int sheetnum,int rownum,int cellnum, Func<IRow, T> Fun)
        {
            List<T> list = new List<T>();
            if (string.IsNullOrEmpty(path))
            {
                return list;
            }

            IWorkbook workbook = null;  //新建IWorkbook对象  
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            if (path.ToLower().IndexOf(".xlsx") > 0) // 2007版本  
            {
                workbook = new XSSFWorkbook(fileStream);  //xlsx数据读入workbook  
            }
            else if (path.ToLower().IndexOf(".xls") > 0) // 2003版本  
            {
                workbook = new HSSFWorkbook(fileStream);  //xls数据读入workbook  
            }
            ISheet sheet = workbook.GetSheetAt(sheetnum);  //获取第一个工作表  
            IRow row;// = sheet.GetRow(0);            //新建当前工作表行数据  
            //课表  白
            for (int i = rownum; i < sheet.LastRowNum; i++)  //对工作表每一行  真实数据从第6行开始
            {
                var o = Fun(sheet.GetRow(i));
                if (o == null)
                {
                    continue;
                }
                list.Add(o);
            }
            return list;
        }
        /// <summary>
        /// 将一行作为一个对象操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">xlsx文件路径</param>
        /// <param name="sheetnum">表从0开始</param>
        /// <param name="rownum">行</param>
        /// <param name="cellnum">列</param>
        /// <param name="Fun">操作</param>
        /// <returns></returns>
        public static List<T> ReadXlsx<T>(string path, int sheetnum, int rownum, int cellnum, Func<IRow,string,Func<ICell,string>, T> Fun,Func<IRow,string> Fun1)
        {
            List<T> list = new List<T>();
            if (string.IsNullOrEmpty(path))
            {
                return list;
            }

            IWorkbook workbook = null;  //新建IWorkbook对象  
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            if (path.ToLower().IndexOf(".xlsx") > 0) // 2007版本  
            {
                workbook = new XSSFWorkbook(fileStream);  //xlsx数据读入workbook  
            }
            else if (path.ToLower().IndexOf(".xls") > 0) // 2003版本  
            {
                workbook = new HSSFWorkbook(fileStream);  //xls数据读入workbook  
            }
            ISheet sheet = workbook.GetSheetAt(sheetnum);  //获取第一个工作表  
            IRow row;// = sheet.GetRow(0);            //新建当前工作表行数据  

            //课表  白
            for (int i = rownum; i < sheet.LastRowNum+1; i++)  //对工作表每一行  真实数据从第6行开始
            {
                var o = Fun(sheet.GetRow(i),Fun1(sheet.GetRow(1)),(c)=> {
                    switch (c.CellType)
                    {
                        case CellType.Numeric:
                            return c.NumericCellValue.ToString();
                        case CellType.Blank:
                            return null;
                        case CellType.Formula:
                            return null;
                            //if (Path.GetExtension(strFileName).ToLower().Trim() == ".xlsx")
                            //{
                            //    XSSFFormulaEvaluator eva = new XSSFFormulaEvaluator(hssfworkbook);
                            //    if (eva.Evaluate(row.GetCell(j)).CellType == CellType.NUMERIC)
                            //    {
                            //        if (HSSFDateUtil.IsCellDateFormatted(row.GetCell(j)))//日期类型
                            //        {
                            //            itemArray[j] = row.GetCell(j).DateCellValue.ToString("yyyy-MM-dd");
                            //        }
                            //        else//其他数字类型
                            //        {
                            //            itemArray[j] = row.GetCell(j).NumericCellValue;
                            //        }
                            //    }
                            //    else
                            //    {
                            //        itemArray[j] = eva.Evaluate(row.GetCell(j)).StringValue;
                            //    }
                            //}
                            //else
                            //{
                            //    HSSFFormulaEvaluator eva = new HSSFFormulaEvaluator(hssfworkbook);
                            //    if (eva.Evaluate(row.GetCell(j)).CellType == CellType.NUMERIC)
                            //    {
                            //        if (HSSFDateUtil.IsCellDateFormatted(row.GetCell(j)))//日期类型
                            //        {
                            //            itemArray[j] = row.GetCell(j).DateCellValue.ToString("yyyy-MM-dd");
                            //        }
                            //        else//其他数字类型
                            //        {
                            //            itemArray[j] = row.GetCell(j).NumericCellValue;
                            //        }
                            //    }
                            //    else
                            //    {
                            //        itemArray[j] = eva.Evaluate(row.GetCell(j)).StringValue;
                            //    }
                            //}
                            //break;
                        default:
                            return System.Text.RegularExpressions.Regex.Replace(c.StringCellValue, @"\s", "").Length==0?null: c.StringCellValue;

                    }
                });
                if (o == null)
                {
                    continue;
                }
                list.Add(o);
            }
            return list;
        }
    }
}