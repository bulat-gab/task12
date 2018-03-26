using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XmlManager.Model
{
    [XmlRoot("File")]
    public class XmlObject
    {
        public XmlObject(string fileName, string fileVersion, DateTime dateTime)
        {
            this.FileName = fileName;
            this.FileVersion = fileVersion;
            this.DateTime = dateTime;
        }

        public XmlObject()
        {
        }

        [XmlIgnore]
        public int Id { get; set; }

        [XmlElement(ElementName = "Name")]
        public string FileName { get; set; }

        [XmlAttribute("FileVersion")]
        public string FileVersion { get; set; }

        [XmlElement(ElementName = "DateTime")]
        public DateTime DateTime { get; set; }

        public override string ToString()
        {
            return $"Id: {Id} Name: {FileName} " +
                   $"Version: {FileVersion} DateModified: {DateTime}";
        }
    }
}
