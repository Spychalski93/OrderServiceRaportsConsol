using OrderServiceRaportsConsol;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

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
            List<string> readDate = new List<string>();

            try
            {
                StreamReader streamReader = File.OpenText(filePath);
                
                do
                {
                    textLine = streamReader.ReadLine();
                    if (textLine == null) break;
                    readDate.Add(textLine);
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
            return readDate;
        }
        /// <summary>
        /// Metoda sprawdzająca czu odczytane dane z pliku mają odpowiedni
        /// format, jesli tak to zapisuje je do listy z zamówieniami      
        /// </summary>
        /// <param name="readDate"> lista z odczytanymi danymi z pliku.</param>
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
        #endregion
    }
}