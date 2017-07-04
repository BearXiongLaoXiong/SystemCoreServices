using System.Collections.Generic;
using System.Data;
using System.Framework.DataAccess;
using System.Threading.Tasks;

namespace BusinessLogicRepository
{
    public abstract class BaseBl : IBaseBl
    {
        public IBaseRepository CurrentRepository { get; set; }
        protected BaseBl()
        {
            Initialization();
        }
        private void Initialization() => SetCurrentRepository();

        protected abstract void SetCurrentRepository();

        public void Execute<T>(T entity) where T : class
        {
             CurrentRepository.Execute(entity);
        }

        public List<TFirst> QuerySingle<T, TFirst>(T entity) where T : class where TFirst : class
        {
            return CurrentRepository.QuerySingle<T, TFirst>(entity);
        }

        public async Task<List<TFirst>> QuerySingleAsync<T, TFirst>(T entity) where T : class where TFirst : class
        {
            return await Task.Run(() => QuerySingle<T, TFirst>(entity));
        }

        public (List<TFirst> ListFirst, List<TSecond> ListSecond) QueryMultiple<T, TFirst, TSecond>(T entity) where T : class where TFirst : class where TSecond : class
        {
            return CurrentRepository.QueryMultiple<T, TFirst, TSecond>(entity);
        }

        public async Task<(List<TFirst> ListFirst, List<TSecond> ListSecond)> QueryMultipleAsync<T, TFirst, TSecond>(T entity) where T : class where TFirst : class where TSecond : class
        {
            return await Task.Run(() => QueryMultiple<T, TFirst, TSecond>(entity));
        }

        public (List<TFirst> ListFirst, List<TSecond> ListSecond, List<TThird> ListThird) QueryMultiple<T, TFirst, TSecond, TThird>(T entity) where T : class where TFirst : class where TSecond : class where TThird : class
        {
            return CurrentRepository.QueryMultiple<T, TFirst, TSecond, TThird>(entity);
        }

        public async Task<(List<TFirst> ListFirst, List<TSecond> ListSecond, List<TThird> ListThird)> QueryMultipleAsync<T, TFirst, TSecond, TThird>(T entity) where T : class where TFirst : class where TSecond : class where TThird : class
        {
            return await Task.Run(() => QueryMultiple<T, TFirst, TSecond, TThird>(entity));
        }

        public DataSet ExecuteDataSet<T>(T entity) where T : class
        {
            return CurrentRepository.ExecuteDataSet(entity);
        }

        public async Task<DataSet> ExecuteDataSetAsync<T>(T entity) where T : class
        {
            return await Task.Run(() => ExecuteDataSet(entity));
        }
    }
}
