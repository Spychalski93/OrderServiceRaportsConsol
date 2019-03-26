using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OrderServiceRaportsConsol
{
    class RaportView
    {      
        /// <summary>
        /// Metoda pozwalająca wybrać sposób wyświetlenia raportu
        /// </summary>
        /// <param name="raport">raport do wyświetlenia</param>
        public static void fileOrConsole(string raport)
        {
            int selectOption;
            string choice;

            Console.WriteLine();
            Console.WriteLine("Jak przedstawic raport: ");
            Console.WriteLine("1. Zapisac do pliku");
            Console.WriteLine("2. Wypisac w konsoli");
            Console.WriteLine("3. Wypisac w konsoli i zapisac do pliku");
            Console.WriteLine("Aby wybrać sposób wyświetlenia raportu wpisz odpowiadającą mu cyfre: ");

            while (true)
            {
                choice = Console.ReadLine();
                try
                {
                    selectOption = int.Parse(choice);

                    switch (selectOption)
                    {
                        case 1:
                            SaveInFile(raport);
                            break;
                        case 2:
                            RaportInDisplay(raport);
                            break;
                        case 3:
                            RaportInDisplay(raport);
                            SaveInFile(raport);
                            break;
                        default:
                            ErrorInterfaceInformation(choice);
                            break;
                    }
                    if ((selectOption == 1) || (selectOption == 2) || (selectOption == 3)) break;
                }
                catch (Exception)
                {
                    ErrorInterfaceInformation(choice);
                }
            }
        }
       
        /// <summary>
        /// Metoda wyświetlajaca dane w konsoli
        /// </summary>
        /// <param name="raport">raport do wyświetlenia</param>
        private static void RaportInDisplay(string raport)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Cyan; 
            
                Console.WriteLine("*********RAPORT********");
                Console.WriteLine(raport);

                Console.ResetColor();
                Console.WriteLine();
            }

        /// <summary>
        /// Metoda tworzy katalog do zapis u zapisuje w nim plik z raportem
        /// </summary>
        /// <param name="raport">raport do zapisu</param>
        private static void SaveInFile(string raport)
        {           
            string pathApplication = Directory.GetCurrentDirectory();
            string pathDirectory = pathApplication.Remove(pathApplication.IndexOf(@"l\bin\D") + 1);
            string pathDirectoryRaports = System.IO.Path.Combine(pathDirectory, "Files raports");
            
            if (!System.IO.Directory.Exists(pathDirectoryRaports))
                System.IO.Directory.CreateDirectory(pathDirectoryRaports);

            CreateFileWithRaport(pathDirectoryRaports, raport);                 
        }

        #region Metody pomocnicze

        /// <summary>
        /// Metoda wyświetljąca informację o błędnym wyborze opcji przez użytkownika
        /// </summary>
        /// <param name="choice">wybór wpisany przez użytkownika</param>
        public static void ErrorInterfaceInformation(string choice)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(choice + ": nie jest odpowiednia wartoscia");
            Console.ResetColor();
            Console.WriteLine("Wpisz ponownie: ");
        }

        /// <summary>
        /// Metoda tworzaca plik
        /// </summary>
        /// <param name="raport">raport do wyświtlenia</param>        
        /// <param name="pathDirectoryRaports">ścieżka do folderu</param>     
        private static void CreateFileWithRaport(string pathDirectoryRaports, string raport)
        {
            string choice;
            int selectOption;
            string pathRaportFile;
            string fileName;            
            FileInfo file = null;
            StreamWriter writer = null;

            Console.WriteLine();
            Console.WriteLine("Podaj nazwe pliku bez rozszerzenia: ");
            fileName = Console.ReadLine() + ".csv";

            pathRaportFile = System.IO.Path.Combine(pathDirectoryRaports, fileName);

            if (System.IO.File.Exists(pathRaportFile))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Plik już istnieje!");
                Console.ResetColor();

                Console.WriteLine();
                Console.WriteLine("1.Nadpisz plik.");
                Console.WriteLine("2.Wprowadź inną nazwę.");
                Console.WriteLine("Aby wybrać opcej wpisz odpowiadającą mu cyfre: ");

                while (true)
                {
                    choice = Console.ReadLine();
                    try
                    {
                        selectOption = int.Parse(choice);

                        if (selectOption == 1)
                        {
                            System.IO.File.Delete(pathRaportFile);
                            file = new FileInfo(pathRaportFile);
                            break;
                        }
                        else if (selectOption == 2)
                        {                            
                            CreateFileWithRaport(pathDirectoryRaports, raport);
                            goto next;
                        }
                        else
                            RaportView.ErrorInterfaceInformation(choice);                                                                                
                    }
                    catch (Exception)
                    {
                        RaportView.ErrorInterfaceInformation(choice);
                    }                      
                }                
            }
            else
            {
                file = new FileInfo(pathRaportFile);          
            }
            try
            {
                writer = file.AppendText();           
                writer.WriteLine("*******RAPORT******");
                writer.Write(raport);

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Plik został zapisany" + Environment.NewLine + "Miejsce zapisu: " + pathRaportFile);
                Console.ResetColor();

            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Błąd w zapisie");
                Console.ResetColor();
                Console.WriteLine();
            }
            finally
            {
                if (writer != null) writer.Close();
                
            }
            next:;
        }
        #endregion
    }        
}

