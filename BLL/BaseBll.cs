using IBLL;
using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BaseBll<T> : IBaseBll<T> where T : class, new()
    {
        protected IBaseDAL<T> _baseDal;
        public BaseBll(IBaseDAL<T> baseDal)
        {
            _baseDal = baseDal;
        }
        public bool Add(T model)
        {
            return _baseDal.Add(model);
        }


        public void AddBatch(List<T> list)
        {
            _baseDal.AddBatch(list);
        }

        public bool AddNoSame(T model)
        {
            return _baseDal.AddNoSame(model);
        }

        public bool Delete(string argGuid)
        {
            return _baseDal.Delete(argGuid);
        }

        public bool DeleteByWhere(string argWhere)
        {
            return _baseDal.DeleteByWhere(argWhere);
        }

        public List<T> GetListByWhere(string argWhere)
        {
            return _baseDal.GetListByWhere(argWhere);
        }

        public T GetModelById(string argGuid)
        {
            return _baseDal.GetModelById(argGuid);
        }

        public T GetModelByWhere(string argWhere)
        {
            return _baseDal.GetModelByWhere(argWhere);
        }

        public bool Update(T model, string argKey)
        {
            return _baseDal.Update(model, argKey);
        }

        public bool UpdateByWhere(T model, string argWhere)
        {
            return _baseDal.UpdateByWhere(model, argWhere);
        }

    }
}
