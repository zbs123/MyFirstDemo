using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface IBaseBll<T> where T : class, new()
    {
        bool Add(T model);
        bool AddNoSame(T model);
        bool Delete(string argGuid);
        bool DeleteByWhere(string argWhere);
        bool Update(T model, string argKey);
        bool UpdateByWhere(T model, string argWhere);
        T GetModelById(string argGuid);
        T GetModelByWhere(string argWhere);
        List<T> GetListByWhere(string argWhere);
        void AddBatch(List<T> list);
    }
}
