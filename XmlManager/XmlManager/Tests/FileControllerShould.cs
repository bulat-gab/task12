using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;
using XmlManager.Controller;

namespace XmlManager.Tests
{
    [TestFixture]
    class FileControllerShould
    {
        private FileController _fileController = new FileController();

        [Test]
        public void CorrectlyCheckFileName()
        {
            var str1 = "ФайлОдин_1_asd";
            var str2 = "ФаО2ин_14_avsd";
            var str3 = "РвымвыуссАУААААААЫАВМВМЫААЫ_20_123abc2";

            var res1 = _fileController.IsFileNameCorrect(str1);
            var res2 = _fileController.IsFileNameCorrect(str2);
            var res3 = _fileController.IsFileNameCorrect(str3);

            Assert.AreEqual(true, res1);
            Assert.AreEqual(false, res2);
            Assert.AreEqual(true, res3);
        }
    }
}
