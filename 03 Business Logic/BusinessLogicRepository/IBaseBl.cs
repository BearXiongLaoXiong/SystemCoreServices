using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BusinessLogicRepository
{
    public interface IBaseBl
    {
        void Execute<T>(T entity) where T : class;

        List<TFirst> QuerySingle<T, TFirst>(T entity)
            where T : class
            where TFirst : class;

        Task<List<TFirst>> QuerySingleAsync<T, TFirst>(T entity)
            where T : class
            where TFirst : class;



        (List<TFirst> ListFirst, List<TSecond> ListSecond) QueryMultiple<T, TFirst, TSecond>(T entity)
            where T : class
            where TFirst : class
            where TSecond : class;
        Task<(List<TFirst> ListFirst, List<TSecond> ListSecond)> QueryMultipleAsync<T, TFirst, TSecond>(T entity)
           where T : class
           where TFirst : class
           where TSecond : class;



        (List<TFirst> ListFirst, List<TSecond> ListSecond, List<TThird> ListThird) QueryMultiple<T, TFirst, TSecond, TThird>(T entity)
          where T : class
          where TFirst : class
          where TSecond : class
          where TThird : class;
        Task<(List<TFirst> ListFirst, List<TSecond> ListSecond, List<TThird> ListThird)> QueryMultipleAsync<T, TFirst, TSecond, TThird>(T entity)
          where T : class
          where TFirst : class
          where TSecond : class
          where TThird : class;


        (List<TFirst> ListFirst, List<TSecond> ListSecond, List<TThird> ListThird, List<TFour> ListFour) QueryMultiple<T, TFirst, TSecond, TThird, TFour>(T entity)
            where T : class
            where TFirst : class
            where TSecond : class
            where TThird : class
            where TFour : class;
        Task<(List<TFirst> ListFirst, List<TSecond> ListSecond, List<TThird> ListThird,List<TFour> ListFour)> QueryMultipleAsync<T, TFirst, TSecond, TThird, TFour>(T entity)
            where T : class
            where TFirst : class
            where TSecond : class
            where TThird : class
            where TFour : class;


        DataSet ExecuteDataSet<T>(T entity) where T : class;

        Task<DataSet> ExecuteDataSetAsync<T>(T entity) where T : class;

    }
}
