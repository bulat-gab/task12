using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using log4net;
using log4net.Repository.Hierarchy;
using XmlManager.DAL;
using XmlManager.Model;

namespace XmlManager.Controller
{
    internal class FileController : IFileController
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(FileController));

        public bool SaveFileToDB(XmlObject xmlObject)
        {
            using (var db = new DataBaseContext())
            {
                xmlObject.Id = 100;
                db.Xmls.Add(xmlObject);
                var isSaved = db.SaveChanges() == 1;

                if (isSaved)
                {
                    _logger.Info($"File: {xmlObject} has been saved to DB.");
                }
                else
                {
                    _logger.Error($"Error occured while saving file: {xmlObject}.");
                }
                
                return isSaved;
            }
        }

        public bool IsFileNameCorrect(string fileName)
        {
            var regex = new Regex("^[а-яА-Я]{0,100}_(1|10|14|15|16|17|18|19|20)_.{0,7}$");
            
            return regex.IsMatch(fileName);
        }
    }
}
