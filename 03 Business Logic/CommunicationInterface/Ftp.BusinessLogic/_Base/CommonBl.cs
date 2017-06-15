using System.Framework.DataAccess;

namespace Ftp.BusinessLogic._Base
{
    public class CommonBl : BaseBl, ICommonBl
    {
        protected override void SetCurrentRepository()
        {
            CurrentRepository = RepositoryFactory.CommonRepository;
        }
    }
}
