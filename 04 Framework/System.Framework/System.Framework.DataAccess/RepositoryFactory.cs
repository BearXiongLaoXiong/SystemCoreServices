using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Framework.DataAccess
{
    public static class RepositoryFactory
    {
        #region Common

        public static ICommonRepository CommonRepository => new CommonRepository();

        #endregion
    }
}
