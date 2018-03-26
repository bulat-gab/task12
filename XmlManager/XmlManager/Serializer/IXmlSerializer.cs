using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlManager.Model;

namespace XmlManager.Serializer
{
    internal interface IXmlSerializer
    {
        XmlObject Deserialize(Stream stream);

        void Serialize(XmlObject xmlObject, Stream stream);
    }
}
