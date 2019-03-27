using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderServiceRaportsConsol;
using OrderServiceRaportsConsol.ReadDateToCollection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OrderServiceRaportsConsolTest
{
    [TestClass]
    public class ReadDataTest
    {
        [TestMethod]
        public void ReadFileTest()
        {
            //Arrange
           string pathApplication = Directory.GetCurrentDirectory();
            string pathDirectory = pathApplication.Remove(pathApplication.IndexOf(@"t\bin\D") + 1);
            string pathFile = System.IO.Path.Combine(pathDirectory, @"TestFiles\listaPlikow.txt");
            List<string> readData = new List<string>();
            int count = 4;
            //Act
            readData = ReadData.ReadFile(pathFile);
            int countAct = readData.Count;
            //Assert
            Assert.AreEqual(count, countAct);
        }

        [TestMethod]
        public void CsvToListTest()
        {
            //Arrange
            string pathApplication = Directory.GetCurrentDirectory();
            string pathDirectory = pathApplication.Remove(pathApplication.IndexOf(@"t\bin\D") + 1);
            string pathFile = System.IO.Path.Combine(pathDirectory, @"TestFiles\z.csv");            
            int count = 6;
            //Act
            ReadData.CsvToList(pathFile);
            int countAct = Order.orders.Count;           
            //Assert
            Assert.AreEqual(count, countAct);

            Order.orders.RemoveRange(0, countAct);
        }

        [TestMethod]
        public void XmlToListTest()
        {
            //Arrange
            string pathApplication = Directory.GetCurrentDirectory();
            string pathDirectory = pathApplication.Remove(pathApplication.IndexOf(@"t\bin\D") + 1);
            string pathFile = System.IO.Path.Combine(pathDirectory, @"TestFiles\z.xml");
            int count = 4;
            //Act
            ReadData.XmlToList(pathFile);
            int countAct = Order.orders.Count;           
            //Assert
            Assert.AreEqual(count, countAct);

            Order.orders.RemoveRange(0, countAct);
        }

        [TestMethod]
        public void JsonToListTest()
        {
            //Arrange
            string pathApplication = Directory.GetCurrentDirectory();
            string pathDirectory = pathApplication.Remove(pathApplication.IndexOf(@"t\bin\D") + 1);
            string pathFile = System.IO.Path.Combine(pathDirectory, @"TestFiles\z.Json");
            int count = 4;
            //Act
            ReadData.JsonToList(pathFile);
            int countAct = Order.orders.Count;           
            //Assert
            Assert.AreEqual(count, countAct);

            Order.orders.RemoveRange(0, countAct);
        }
    }
}
