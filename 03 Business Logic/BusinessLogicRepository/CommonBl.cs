using System.Framework.DataAccess;

namespace BusinessLogicRepository
{
    public class CommonBl : BaseBl, ICommonBl
    {
        protected override void SetCurrentRepository()
        {
            CurrentRepository = RepositoryFactory.CommonRepository;
        }
    }
}
