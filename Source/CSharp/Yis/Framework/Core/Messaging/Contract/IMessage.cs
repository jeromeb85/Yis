using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yis.Framework.Core.Messaging.Contract
{
    public interface IMessage
    {
    }

    public interface IMessage<TResult> : IMessage
    {
        TResult Result { get; set; }
    }
}