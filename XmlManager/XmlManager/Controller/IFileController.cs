using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlManager.Model;

namespace XmlManager.Controller
{
    internal interface IFileController
    {
        bool SaveFileToDB(XmlObject xmlObject);

        bool IsFileNameCorrect(string fileName);
    }
}
