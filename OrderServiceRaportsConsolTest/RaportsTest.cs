using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderServiceRaportsConsol;
using OrderServiceRaportsConsol.Raports;
using OrderServiceRaportsConsol.ReadDateToCollection;
using System.IO;

namespace OrderServiceRaportsConsolTest
{
    [TestClass]
    public class RaportsTest : Raports
    {
        #region Metody pomocnicze
        /// <summary>
        /// Metoda dodaje dane to lisy zamówieñ
        /// </summary>
        private void TestData()
        {
            string pathApplication = Directory.GetCurrentDirectory();
            string pathDirectory = pathApplication.Remove(pathApplication.IndexOf(@"t\bin\D") + 1);
            string pathFile = System.IO.Path.Combine(pathDirectory, @"TestFiles\z.csv");

            ReadData.CsvToList(pathFile);
        }
        private void TestDataClear()
        {
            int countAct = Order.orders.Count;
            Order.orders.RemoveRange(0, countAct);
        }
        #endregion

        [TestMethod]
        public void NumbersOfOrdersTest()
        {
            //Arrange
            TestData();
            int countOrders = 4;
            //Act
            int countOrdersAct = NumbersOfOrders(Order.orders);
            //Assert
            Assert.AreEqual(countOrders, countOrdersAct);

            TestDataClear();
        }

        [TestMethod]
        public void ClientNumbersOfOrdersTest()
        {
            //Arrange
            TestData();
            int countOrders = 2;
            string clientId = "2";
            //Act
            int countOrdersAct = ClientNumbersOfOrders(Order.orders, clientId);
            //Assert
            Assert.AreEqual(countOrders, countOrdersAct);

            TestDataClear();
        }

        [TestMethod]
        public void TotalPriceTest()
        {
            //Arrange
            TestData();
            double totalPrice = 125.9;
            //Act
            double totalPriceAct = TotalPrice(Order.orders);
            //Assert
            Assert.AreEqual(totalPrice, totalPriceAct);

            TestDataClear();
        }

        [TestMethod]
        public void ClientTotalPriceTest()
        {
            //Arrange
            TestData();
            double clientTotalPrice = 55;
            string clientId = "2";
            //Act
            double clientTotalPriceAct = ClientTotalPrice(Order.orders, clientId);
            //Assert
            Assert.AreEqual(clientTotalPrice, clientTotalPriceAct);

            TestDataClear();
        }

        [TestMethod]
        public void AllOldersTest()
        {
            //Arrange
            TestData();
            //Act
            string allOrders = AllOrders(Order.orders);
            //Assert
            Assert.IsNotNull(allOrders);

            TestDataClear();
        }

        [TestMethod]
        public void ClientALLOrdersTest()
        {
            //Arrange
            TestData();
            string clientId = "2";
            //Act
            string clientAllOrders = ClientAllOrders(Order.orders, clientId);
            //Assert
            Assert.IsNotNull(clientAllOrders);

            TestDataClear();
        }

        [TestMethod]
        public void AverageOrderValueTest()
        {
            //Arrange
            TestData();
            double averageOrderValue = 31.48;
            //Act
            double AverageOrderValueAct = AverageOrderValue(Order.orders);
            //Assert
            Assert.AreEqual(averageOrderValue, AverageOrderValueAct);

            TestDataClear();
        }

        [TestMethod]
        public void ClientAverageOrderValueTest()
        {
            //Arrange
            TestData();
            double averageOrderValue = 27.5;
            string clientId = "2";
            //Act
            double AverageOrderValueAct = ClientAverageOrderValue(Order.orders, clientId);
            //Assert
            Assert.AreEqual(averageOrderValue, AverageOrderValueAct);

            TestDataClear();
        }

        [TestMethod]
        public void GroupNumberOfOrdersTest()
        {
            //Arrange
            TestData();
            string field = "ClientId";
            string ascendingOrDescending = "Ascending";
            //Act
            string groupNumberOfOrders = GroupNumberOfOrders(Order.orders, field, ascendingOrDescending);
            //Assert
            Assert.IsNotNull(groupNumberOfOrders);

            TestDataClear();
        }

        [TestMethod]
        public void ClientGroupNumberOfOrdersTest()
        {
            //Arrange
            TestData();
            string clientId = "2";
            string field = "ClientId";
            string ascendingOrDescending = "Ascending";
            //Act
            string clientGroupNumberOfOrders = GroupNumberOfOrders(Order.orders, field, ascendingOrDescending);
            //Assert
            Assert.IsNotNull(clientGroupNumberOfOrders);

            TestDataClear();
        }

        [TestMethod]
        public void OrdersPriceRangeTest()
        {
            //Arrange
            TestData();
            double min = 10;
            double max = 40;
            //Act
            string ordersPriceRange = OrdersPriceRange(Order.orders, min, max);
            bool score = ordersPriceRange.Contains("Zamówienie 3:");
            //Assert
            Assert.IsTrue(score);

            TestDataClear();
        }
    }
}
