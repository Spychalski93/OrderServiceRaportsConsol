
using OrderServiceRaportsConsol.ReadDateToCollection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;


namespace OrderServiceRaportsConsol
{
    class Program : Raports.Raports
    {
        

            
            static void Main(string[] args)
            {                        
                string choice;
                int selectOption;
                string filePath;
                string raport;
                string clientId;
                string sortingMethod;
                string ascendingOrDescending;
                double min, max;

                List<String> fileList = new List<string>();

                Console.WriteLine("*********PROGRAM DO OBSŁUGI ZAMÓWIEŃ********");
                do
                {
                    Console.WriteLine("Podaj ścieżkę do pliku z listą plików z zamówieniami: ");
                    //filePath = @Console.ReadLine();
                    filePath = @"C:\Users\michu\Desktop\IT\Projekty\OrderServiceRaportsConsol\OrderServiceRaportsConsol\TestFiles\listaPlikow.txt";

                    fileList = ReadData.ReadFile(filePath);
                } while (fileList.Count() == 0);

                ReadListFiles(fileList);

             raportInterface:
                RaportsUserInterfaceView();
                while (true)
                {
                    choice = Console.ReadLine();
                    try
                    {
                        selectOption = int.Parse(choice);

                        switch (selectOption)
                        {
                            case 1:
                                raport = "Łączna liczba zamówień: " + NumbersOfOrders(Order.orders);
                                RaportView.fileOrConsole(raport);
                                goto raportInterface;
                            case 2:
                                clientId = LoadClientId();
                                raport = "Łączna liczba zamówień dla klienta o id '" + clientId + "': " 
                                    + ClientNumbersOfOrders(Order.orders, clientId);
                                RaportView.fileOrConsole(raport);
                                goto raportInterface;
                            case 3:
                                raport = "Łączna kwota zamówień: " + TotalPrice(Order.orders);
                                RaportView.fileOrConsole(raport);
                                goto raportInterface;
                            case 4:
                                clientId = LoadClientId();
                                raport = "Łączna kwota zamówień dla klienta o id'" + clientId + "': " 
                                    + ClientTotalPrice(Order.orders, clientId);
                                RaportView.fileOrConsole(raport);
                                goto raportInterface;
                            case 5:                          
                                sortingMethod = ChoseSortingMethod();
                                if (sortingMethod == "Order")
                                    raport = "Wszystkie zamówienia: " + Environment.NewLine + AllOrders(Order.orders);
                                else
                                {
                                    ascendingOrDescending = ChoiceAscendingOrDescending();
                                    raport = "Wszystkie zamówienia: " + Environment.NewLine 
                                        + AllOrders(Order.orders, sortingMethod, ascendingOrDescending);
                                }
                                RaportView.fileOrConsole(raport);
                                goto raportInterface;
                            case 6:
                                clientId = LoadClientId();
                                sortingMethod = ChoseSortingMethod();
                                if (sortingMethod == "Order")
                                    raport = "Wszystkie zamówienia dla klienta o id'" + clientId + "': " + Environment.NewLine 
                                        + ClientAllOrders(Order.orders, clientId);
                                else
                                {
                                    ascendingOrDescending = ChoiceAscendingOrDescending();
                                    raport = "Wszystkie zamówienia dla klienta o id'" + clientId + "': " + Environment.NewLine
                                        + ClientAllOrders(Order.orders, clientId, sortingMethod, ascendingOrDescending);
                                }
                                RaportView.fileOrConsole(raport);
                                goto raportInterface;
                            case 7:
                                raport = "Średnia wartość wszystkich zamówień: " + AverageOrderValue(Order.orders);
                                RaportView.fileOrConsole(raport);
                                goto raportInterface;
                            case 8:
                                clientId = LoadClientId();
                                raport = "Średnia wartość wszystkich zamówień dla klienta o id'" + clientId + "': " 
                                    + ClientAverageOrderValue(Order.orders, clientId);
                                RaportView.fileOrConsole(raport);
                                goto raportInterface;
                            case 9:
                                sortingMethod = SortingMethodGroupByName();
                                ascendingOrDescending = ChoiceAscendingOrDescending();
                                raport = "Ilość zamówień pogrupowane po nazwie produktów: " + Environment.NewLine 
                                    + GroupNumberOfOrders(Order.orders,sortingMethod, ascendingOrDescending);
                                RaportView.fileOrConsole(raport);
                                goto raportInterface;
                            case 10:
                                clientId = LoadClientId();
                                sortingMethod = SortingMethodGroupByName();
                                ascendingOrDescending = ChoiceAscendingOrDescending();
                                raport = "Ilość zamówień pogrupowane po nazwie produktów dla klienta o id'" + clientId + "': " 
                                    + Environment.NewLine 
                                    + ClientGroupNumberOfOrders(Order.orders, clientId, sortingMethod, ascendingOrDescending);
                                RaportView.fileOrConsole(raport);
                                goto raportInterface;
                            case 11:
                                Console.WriteLine("Podaj dolną granice przedziału: ");
                                min = LoadDoubleValue();
                                Console.WriteLine("Podaj górną granice przedziału: ");
                                max = LoadDoubleValue();                           
                                raport = "Zamówienia w zamkniętym przedziale cenowym(" + min + "-" + max +"): " + Environment.NewLine 
                                    + OrdersPriceRange(Order.orders, min, max);
                                RaportView.fileOrConsole(raport);
                                goto raportInterface;
                            case 12:
                                goto end;
                            default:
                                RaportView.ErrorInterfaceInformation(choice);
                                break;
                        }
                    }
                    catch (Exception)
                    {
                        RaportView.ErrorInterfaceInformation(choice);
                    }
                }
            end:;
            }

            #region Metody Pomocnicze
            
            /// <summary>
            /// Metoda odczytująca wartości double podane przez użytkownika
            /// </summary>
            /// <returns name = "value">Wartość double</returns>
            private static double LoadDoubleValue()
            {
                double value;
                string loadValue;
                while (true)
                {
                    loadValue = Console.ReadLine();
                    try
                    {                    
                        value = double.Parse(loadValue);
                        goto end;
                    }
                    catch (Exception)
                    {
                        RaportView.ErrorInterfaceInformation(loadValue);
                    }                
                }
                end:
                return value;
            }
            /// <summary>
            /// Metoda pozwalajaca pobrać z konsoli wybrany przez uzytkownika sposób sortowania
            /// </summary>
            /// <returns name = "sotringMethod"> zmienna z sposobem sortowania</returns>
            private static string SortingMethodGroupByName()
            {
                int selectOption;
                string choice;
                string sortingMethod;

                Console.WriteLine();
                Console.WriteLine("Jak posortować raport: ");
                Console.WriteLine("1. Po nazwach produktów");
                Console.WriteLine("2. Po ilości wystąpień");
                Console.WriteLine("Aby wybrać sposób sortowania raportu wpisz odpowiadającą mu cyfre: ");

                while (true)
                {
                    choice = Console.ReadLine();
                    try
                    {
                        selectOption = int.Parse(choice);

                        switch (selectOption)
                        {
                            case 1:
                                sortingMethod = "Key";
                                goto end;
                            case 2:
                                sortingMethod = "Count";
                                goto end;
                            default:
                                RaportView.ErrorInterfaceInformation(choice);
                                break;
                        }
                    }
                    catch (Exception)
                    {
                        RaportView.ErrorInterfaceInformation(choice);
                    }
                }
            end:
                return sortingMethod;
            }

            /// <summary>
            /// Metoda pozwalajaca pobrać z konsoli wybrany przez uzytkownika sposób sortowania
            /// </summary>
            /// <returns name = "sotringMethod"> zmienna z sposobem sortowania</returns>
            private static string ChoseSortingMethod()
            {
                int selectOption;
                string choice;
                string sortingMethod;

                Console.WriteLine();
                Console.WriteLine("Jak posortować raport: ");
                Console.WriteLine("1. Po zamówieniach");
                Console.WriteLine("2. Po clientId");
                Console.WriteLine("3. Po requestId");
                Console.WriteLine("4. Po name");
                Console.WriteLine("5. Po quantity");
                Console.WriteLine("6. Po price");
                Console.WriteLine("Aby wybrać sposób sortowania raportu wpisz odpowiadającą mu cyfre: ");

                while (true)
                {
                    choice = Console.ReadLine();
                    try
                    {
                        selectOption = int.Parse(choice);

                        switch (selectOption)
                        {
                            case 1:
                                sortingMethod = "Order";
                                goto end;
                            case 2:
                                sortingMethod = "ClientId";
                                goto end;
                            case 3:
                                sortingMethod = "RequstId";
                                goto end;
                            case 4:
                                sortingMethod = "Name";
                                goto end;
                            case 5:
                                sortingMethod = "Quantity";
                                goto end;
                            case 6:
                                sortingMethod = "Price";
                                goto end;
                            default:
                                RaportView.ErrorInterfaceInformation(choice);
                                break;
                        }                
                    }
                    catch (Exception)
                    {
                        RaportView.ErrorInterfaceInformation(choice);
                    }
                }
            end:
                return sortingMethod;
            }

            /// <summary>
            /// Metoda pozwalajaca pobrać z konsoli wybrany przez uzytkownika sposób sortowania
            /// </summary>
            /// <returns name = "ascendingOrDescending"> zmienna z sposobem sortowania</returns>
            private static string ChoiceAscendingOrDescending()
            {
                int selectOption;
                string choice;
                string ascendingOrDescending;

                Console.WriteLine();
                Console.WriteLine("Jak posortować raport: ");
                Console.WriteLine("1. Rosnąco");
                Console.WriteLine("2. Malejąco");
                Console.WriteLine("Aby wybrać sposób sortowania raportu wpisz odpowiadającą mu cyfre: ");

                while (true)
                {
                    choice = Console.ReadLine();
                    try
                    {
                        selectOption = int.Parse(choice);

                        switch (selectOption)
                        {
                            case 1:
                                ascendingOrDescending = "Ascending";
                                goto end;
                            case 2:
                                ascendingOrDescending = "Descending";
                                goto end;
                            default:
                                RaportView.ErrorInterfaceInformation(choice);
                                break;
                        }
                    }
                    catch (Exception)
                    {
                        RaportView.ErrorInterfaceInformation(choice);
                    }
                }
            end:
                return ascendingOrDescending;
            }

            /// <summary>
            /// Metoda pobierająca od użytkownika id klienta ('clientID')
            /// </summary>
            /// <returns name = "clientId">id klienta</returns>
            private static string LoadClientId()
            {
                string clientId;
                Console.WriteLine();
                Console.WriteLine("Podaj numer klienta: ");
                clientId = Console.ReadLine();
                return clientId;
            }
            /// <summary>
            /// Metoda rozpoznaje format pliku i wyołuje odpowiedne metody do ich odczytu
            /// </summary>
            /// <param name="fileList"></param>
            private static void ReadListFiles(List<string> fileList)
            {
                foreach (var item in fileList)
                {
                    if (item.EndsWith(".csv"))
                        ReadData.CsvToList(item);
                    else if (item.EndsWith(".xml"))
                        ReadData.XmlToList(item);
                    else if (item.EndsWith(".json"))
                        ReadData.JsonToList(item);
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Plik: " + item + " - ma nieodpowiedni format");
                        Console.ResetColor();
                    }
                }
            }
            /// <summary>
            /// Metoda wyswietla w consoli interfejs użytkownika do wyboru raportu
            /// </summary>
            private static void RaportsUserInterfaceView()
            {
                Console.WriteLine();
                Console.WriteLine("****************** Mozliwe do wygenerowania raporty *****************");
                Console.WriteLine("1.Ilość zamówień");
                Console.WriteLine("2.Ilość zamówień dla klienta o wskazanym identyfikatorze");
                Console.WriteLine("3.Łączna kwota zamówień");
                Console.WriteLine("4.Łączna kwota zamówień dla klienta o wskazanym identyfikatorze");
                Console.WriteLine("5.Lista wszystkich zamówień");
                Console.WriteLine("6.Lista zamówień dla klienta o wskazanym identyfikatorze");
                Console.WriteLine("7.Średnia wartość zamówienia");
                Console.WriteLine("8.Średnia wartość zamówienia dla klienta o wskazanym identyfikatorze");
                Console.WriteLine("9.Ilość zamówień pogrupowanych po nazwie");
                Console.WriteLine("10.Ilość zamówień pogrupowanych po nazwie dla klienta o wskazanym identyfikatorze");
                Console.WriteLine("11.Zamówienia w podanym przedziale cenowym");
                Console.WriteLine("12.Zakończ program");
                Console.WriteLine();
                Console.WriteLine("Aby wybrać raport wpisz odpowiadającą mu cyfre: ");
                */
        }
            #endregion


    }
}
