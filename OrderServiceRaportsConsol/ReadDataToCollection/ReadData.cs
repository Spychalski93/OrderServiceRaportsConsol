using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;

namespace OrderServiceRaportsConsol.ReadDateToCollection
{
    public static class ReadData
    {
        #region Metody
        /// <summary>
        /// Metoda odczytująca pojedyńcze linie z pliku Csv z pominięciem pierwszej,
        /// która jest nagłówkiem.
        /// </summary>
        /// <param name="filePath">Ścieżka do pliku.</param>       
        public static List<string> ReadFile(string filePath)
        {
            string textLine;
            List<string> readData = new List<string>();

            try
            {
                StreamReader streamReader = File.OpenText(filePath);
                
                do
                {
                    textLine = streamReader.ReadLine();
                    if (textLine == null) break;
                    readData.Add(textLine);
                } while (textLine != null);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Odczytano plik: " + filePath + Environment.NewLine);
                Console.ResetColor();

                streamReader.Close();

            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Błędna ściezka do pliku: " + filePath + Environment.NewLine);
                Console.ResetColor();
            }
            return readData;
        }

        /// <summary>
        /// Metoda sprawdzająca czu odczytane dane z pliku csv mają odpowiedni
        /// format, jesli tak to zapisuje je do listy z zamówieniami      
        /// </summary>
        /// <param name="filePath">ścieżka do pliku</param>
        public static void CsvToList(string filePath)
        {
            List<string> readDate = ReadFile(filePath);

            readDate.RemoveAt(0);

            foreach (var item in readDate)
            {
                String[] orderDetails = item.Split(',');
                try
                {
                    Order order = new Order(orderDetails[0], ulong.Parse(orderDetails[1]), orderDetails[2],
                       uint.Parse(orderDetails[3]), Double.Parse(orderDetails[4], CultureInfo.InvariantCulture));
                    order.OrdersAdd(order);
                }
                catch (FormatException fe)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("!!!!Błąd w pliku!!!! " + Environment.NewLine + filePath + Environment.NewLine +
                        "*** Błąd w linii: " + item.ToString() + " *** " + fe.Message + Environment.NewLine);
                    Console.ResetColor();

                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("!!!!Błąd w pliku!!!! " + Environment.NewLine + filePath + Environment.NewLine +
                        " *** Błąd w linii: " + item.ToString() + " *** " + e.Message + Environment.NewLine);
                    Console.ResetColor();
                }
            }
        }

        /// <summary>
        /// Metoda sprawdzająca czu odczytane dane z pliku xml mają odpowiedni
        /// format, jesli tak to zapisuje je do listy z zamówieniami
        /// </summary>
        /// <param name="filePath">ścieżka do pliku</param>
        public static void XmlToList(string filePath)
        {
            XmlDocument XmlDoc = new XmlDocument();
            try
            {
                XmlDoc.Load(filePath);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Odczytano plik: " + filePath + Environment.NewLine);
                Console.ResetColor();

                int count = XmlDoc.GetElementsByTagName("request").Count;                
               
                for (int i = 0; i < count; i++)
                {
                    string clientId = XmlDoc.GetElementsByTagName("clientId").Item(i).InnerText;
                    string requestId = XmlDoc.GetElementsByTagName("requestId").Item(i).InnerText;
                    string name = XmlDoc.GetElementsByTagName("name").Item(i).InnerText;
                    string quantity = XmlDoc.GetElementsByTagName("quantity").Item(i).InnerText;
                    string price = XmlDoc.GetElementsByTagName("price").Item(i).InnerText;

                    try
                    {
                        Order order = new Order(clientId, ulong.Parse(requestId), name,
                           uint.Parse(quantity), Double.Parse(price, CultureInfo.InvariantCulture));
                        order.OrdersAdd(order);
                    }
                    catch (FormatException fe)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("!!!!Błąd w pliku!!!! " + Environment.NewLine + filePath + Environment.NewLine +
                            "*** Błąd w request: " + (i + 1) + " *** " + Environment.NewLine 
                            + "ClientId: " + clientId + Environment.NewLine
                            + "RequestId: " + requestId + Environment.NewLine
                            + "Name: " + name + Environment.NewLine
                            + "Quantity: " + quantity + Environment.NewLine
                            + "Price: " + price + Environment.NewLine
                            + fe.Message + Environment.NewLine);
                        Console.ResetColor();

                    }
                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("!!!!Błąd w pliku!!!! " + Environment.NewLine + filePath + Environment.NewLine +                           
                            "*** Błąd w request: " + (i + 1) + " *** " + Environment.NewLine
                            + "ClientId: " + clientId + Environment.NewLine
                            + "RequestId: " + requestId + Environment.NewLine
                            + "Name: " + name + Environment.NewLine
                            + "Quantity: " + quantity + Environment.NewLine
                            + "Price: " + price + Environment.NewLine
                            + e.Message + Environment.NewLine);
                        Console.ResetColor();
                    }
                }
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Błąd odczytu pliku: " + filePath + Environment.NewLine + e.Message);
                Console.ResetColor();
            }
            
        }

        /// <summary>
        /// Metoda sprawdzająca czu odczytane dane z pliku json mają odpowiedni
        /// format, jesli tak to zapisuje je do listy z zamówieniami
        /// </summary>
        /// <param name="filePath">ścieżka do pliku</param>
        public static void JsonToList(string filePath)
        {            
            string entireJsonFileInString = "";
            int i;

            try
            {
                StreamReader streamReader = File.OpenText(filePath);
                entireJsonFileInString = streamReader.ReadToEnd();
              
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Odczytano plik: " + filePath + Environment.NewLine);
                Console.ResetColor();
                
                streamReader.Close();
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Błędna ściezka do pliku: " + filePath + Environment.NewLine);
                Console.ResetColor();
            }

            JObject jsonObject = JObject.Parse(entireJsonFileInString);            
            JEnumerable<JToken> requestsObjects = (jsonObject["requests"].Children());
            i = 0;

            foreach (var item in requestsObjects)
            {
                i++;
                string clientId = (string)item["clientId"];
                string requestId = (string)item["requestId"];
                string name = (string)item["name"];
                string quantity = (string)item["quantity"];
                string price = (string)item["price"];

                try
                {
                    Order order = new Order(clientId, ulong.Parse(requestId), name,
                       uint.Parse(quantity), Double.Parse(price, CultureInfo.InvariantCulture));
                    order.OrdersAdd(order);
                }
                catch (FormatException fe)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("!!!!Błąd w pliku!!!! " + Environment.NewLine + filePath + Environment.NewLine +
                        "*** Błąd w request: " + (i) + " *** " + Environment.NewLine
                        + "ClientId: " + clientId + Environment.NewLine
                        + "RequestId: " + requestId + Environment.NewLine
                        + "Name: " + name + Environment.NewLine
                        + "Quantity: " + quantity + Environment.NewLine
                        + "Price: " + price + Environment.NewLine
                        + fe.Message + Environment.NewLine);
                    Console.ResetColor();

                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("!!!!Błąd w pliku!!!! " + Environment.NewLine + filePath + Environment.NewLine +
                        "*** Błąd w request: " + (i) + " *** " + Environment.NewLine
                        + "ClientId: " + clientId + Environment.NewLine
                        + "RequestId: " + requestId + Environment.NewLine
                        + "Name: " + name + Environment.NewLine
                        + "Quantity: " + quantity + Environment.NewLine
                        + "Price: " + price + Environment.NewLine
                        + e.Message + Environment.NewLine);
                    Console.ResetColor();
                }
            }           
        }
        #endregion
    }
}