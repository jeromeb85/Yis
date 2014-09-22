using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAXLib;
using Yis.Framework.Core.Extension;
using Yis.Framework.Core.Serialization.Contract;

namespace Yis.Framework.Core.Serialization.Yaxlib
{
    public class YaxlibSerializer : IXmlSerializer
    {
        #region Methods

        public T DeSerialize<T>(string file)
        {
            YAXSerializer Yax = new YAXSerializer(typeof(T));
            return (T)Yax.DeserializeFromFile(file);
        }

        public T DeSerialize<T>(System.IO.Stream stream)
        {
            throw new NotImplementedException();
        }

        public void Serialize<T>(T obj, string file)
        {
            YAXSerializer Yax = new YAXSerializer(typeof(T));
            Yax.SerializeToFile(obj, file);
        }

        public void Serialize<T>(T obj, System.IO.Stream stream)
        {
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}