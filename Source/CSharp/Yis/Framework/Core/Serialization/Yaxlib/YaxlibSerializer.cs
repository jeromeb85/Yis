using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Framework.Core.Serialization.Contract;

namespace Yis.Framework.Core.Serialization.Yaxlib
{
    public class YaxlibSerializer : IXmlSerializer
    {
        #region Methods

        public T DeSerialize<T>(string file)
        {
            throw new NotImplementedException();
        }

        public T DeSerialize<T>(string file, int bufferSize)
        {
            throw new NotImplementedException();
        }

        public T DeSerialize<T>(System.IO.Stream stream)
        {
            throw new NotImplementedException();
        }

        public void Serialize<T>(T obj, string file)
        {
            throw new NotImplementedException();
        }

        public void Serialize<T>(T obj, string file, int bufferSize)
        {
            throw new NotImplementedException();
        }

        public void Serialize<T>(T obj, System.IO.Stream stream)
        {
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}