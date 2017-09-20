using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Framework.DataAccess
{
    public class BaseRepository
    {
        public void Execute<T>(T entity) where T : class
            => DapperExtension<T>.Execute(entity);

        public List<TFirst> QuerySingle<T, TFirst>(T entity)
            where T : class
            where TFirst : class
            => DapperExtension<T>.QuerySingle<TFirst>(entity);

        public (List<TFirst> ListFirst, List<TSecond> ListSecond) QueryMultiple<T, TFirst, TSecond>(T entity)
            where T : class
            where TFirst : class
            where TSecond : class
            => DapperExtension<T>.QueryMultiple<TFirst, TSecond>(entity);

        public (List<TFirst> ListFirst, List<TSecond> ListSecond, List<TThird> ListThird) QueryMultiple
          <T, TFirst, TSecond, TThird>(T entity)
          where T : class
          where TFirst : class
          where TSecond : class
          where TThird : class
             => DapperExtension<T>.QueryMultiple<TFirst, TSecond, TThird>(entity);

        public (List<TFirst> ListFirst, List<TSecond> ListSecond, List<TThird> ListThird, List<TFour> ListFour) QueryMultiple
            <T, TFirst, TSecond, TThird, TFour>(T entity)
            where T : class
            where TFirst : class
            where TSecond : class
            where TThird : class
            where TFour : class
            => DapperExtension<T>.QueryMultiple<TFirst, TSecond, TThird, TFour>(entity);

        public DataSet ExecuteDataSet<T>(T entity) where T : class
            => DapperExtension<T>.ExecuteDataSet(entity);
    }
}
