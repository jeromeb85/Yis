using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yis.Framework.Core.Caching.Contract
{
    public interface IExpirationPolicy
    {
        #region Properties

        bool CanReset { get; }

        bool IsExpired { get; }

        #endregion Properties

        #region Methods

        void Reset();

        #endregion Methods
    }
}