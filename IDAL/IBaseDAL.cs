using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface IBaseDAL<T> where T : class, new()
    {
        bool Add(T model);
        bool AddNoSame(T model);
        bool Delete(string argGuid);
        bool DeleteByWhere(string argWhere);
        bool Update(T model, string argKey);
        T GetModelById(string argGuid);
        bool UpdateByWhere(T model, string argWhere);
        T GetModelByWhere(string argWhere);
        List<T> GetListByWhere(string argWhere);
        List<T> GetListByWhere(string argWhere, int pageIndex, int pageSize);
        void Log(string strText);
        void Log(List<string> strTextLst);
        void AddBatch(List<T> list);
    }
}
