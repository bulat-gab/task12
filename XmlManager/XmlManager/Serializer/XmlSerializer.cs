using System.IO;
using System.Xml;
using System.Xml.Serialization;
using log4net;
using XmlManager.Model;

namespace XmlManager.Serializer
{
    internal class XmlSerializer : IXmlSerializer
    {
        private static readonly XmlWriterSettings WriterSettings = new XmlWriterSettings { OmitXmlDeclaration = true, Indent = true };
        private static readonly XmlSerializerNamespaces Namespaces = new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "") });
        private readonly ILog _logger = LogManager.GetLogger(typeof(XmlSerializer));

        public XmlObject Deserialize(Stream stream)
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(XmlObject));
            var xmlObject = (XmlObject)serializer.Deserialize(stream);
            _logger.Info($"File {xmlObject} has been deserialized.");
            return xmlObject;
        }

        public void Serialize(XmlObject xmlObject, Stream stream)
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(XmlObject));

            using (var writer = XmlWriter.Create(stream, WriterSettings))
            {
                serializer.Serialize(writer, xmlObject, Namespaces);
                _logger.Info($"File {xmlObject} has been serialized.");
            }
        }
    }
}
