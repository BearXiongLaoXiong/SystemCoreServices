using System;
using System.Collections.Generic;
using System.Framework.DataAccess;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRepositoryFactory
{
    public class CommonBl : BaseBl, ICommonBl
    {
        protected override void SetCurrentRepository()
        {
            CurrentRepository = RepositoryFactory.CommonRepository;
        }
    }
}
