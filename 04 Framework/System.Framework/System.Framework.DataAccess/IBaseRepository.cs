using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Framework.DataAccess
{
    public interface IBaseRepository
    {
        void Execute<T>(T entity) where T : class;

        List<TFirst> QuerySingle<T, TFirst>(T entity)
            where T : class
            where TFirst : class;

        (List<TFirst> ListFirst, List<TSecond> ListSecond) QueryMultiple<T, TFirst, TSecond>(T entity)
            where T : class
            where TFirst : class
            where TSecond : class;

        (List<TFirst> ListFirst, List<TSecond> ListSecond, List<TThird> ListThird) QueryMultiple
          <T, TFirst, TSecond, TThird>(T entity)
          where T : class
          where TFirst : class
          where TSecond : class
          where TThird : class;

        DataSet ExecuteDataSet<T>(T entity) where T : class;
    }
}
