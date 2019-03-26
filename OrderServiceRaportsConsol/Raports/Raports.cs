using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderServiceRaportsConsol.Raports
{
    class Raports
    {
        #region 
        /// <summary>
        /// Metoda sprawdzająca ilośc zamówień z plików wczytanych do programu
        /// </summary>
        /// <param name="orders">Lista wczytanych zamówień</param>
        /// <returns name="numbers">ilość zamówień</returns>
        protected static int NumbersOfOrders(List<Order> orders)
        {
            List<Order> ordersSort = SortByClientIdAndRequestId(orders);
            int numbers = 0;

            if (ordersSort.Count() != 0)
            {
                numbers = 1;
                for (int i = 0; i <= ordersSort.Count() - 2; i++)
                {

                    if ((!ordersSort[i].ClientId.Equals(ordersSort[i + 1].ClientId))
                            || (ordersSort[i].RequestId != (ordersSort[i + 1].RequestId)))
                    {
                        numbers++;
                    }
                }
            }
            else
                return numbers;

            return numbers;
        }

        /// <summary>
        /// Metoda sprawdzająca ilośc zamówień dla wybranego klienta
        /// </summary>
        /// <param name="orders">Lista wczytanych zamówień</param>
        /// <param name="clientId">numer klienta dla którego ma być wyświetlona ilość zamówień</param>
        /// <returns name="numbers">ilość zamówień</returns>
        protected static int ClientNumbersOfOrders(List<Order> orders, String clientId)
        {
            List<Order> ordersSort = SortByClientIdAndRequestId(orders);
            int numbers = 0;
            if (ordersSort.Any(Order => Order.ClientId == clientId))
            {
                numbers = 1;
                for (int i = 0; i <= ordersSort.Count() - 2; i++)
                {              
                    
                    if ((clientId.Equals(ordersSort[i].ClientId)) & (ordersSort[i].RequestId != ordersSort[i + 1].RequestId))
                    {
                        if (clientId.Equals(ordersSort[i + 1].ClientId))
                        {
                            numbers++;
                        }                 
                    }                
                }
            }
            else
                return numbers;
            return numbers;
        }

        /// <summary>
        /// Metoda obliczająca łaczną kwotę zamówień
        /// </summary>
        /// <param name="orders">Lista wczytanych zamówień</param>
        /// <returns name="totalPrice">łączną kwote zamówień</returns>
        protected static double TotalPrice(List<Order> orders)
        {        
            Double totalPrice = 0.0;
            for (int i = 0; i <= orders.Count() - 1; i++)
            {
                Double orderPrice = orders[i].Quantity * orders[i].Price;
                totalPrice = totalPrice + orderPrice;
            }
            return totalPrice;
        }

        /// <summary>
        /// Metoda obliczająca łaczną kwotę zamówień dla wybranego klienta
        /// </summary>
        /// <param name="orders">Lista wczytanych zamówień</param>
        /// <param name="clientId">numer klienta dla którego ma być obliczona łączna kwota zamówień</param>
        /// <returns name="totalPrice">łączną kwote zamówień</returns>
        protected static double ClientTotalPrice(List<Order> orders, string clientId)
        {
            Double totalPrice = 0.0;
            for (int i = 0; i <= orders.Count() - 1; i++)
            {
                if (clientId.Equals(orders[i].ClientId))
                {
                    Double orderPrice = orders[i].Quantity * orders[i].Price;
                    totalPrice = totalPrice + orderPrice;
                }
            }
            return totalPrice;
        }

        /// <summary>
        /// Metoda tworząca zmienną ze wszystkimi zamówieniami posortowaną po ClientId i RequestId
        /// </summary>
        /// <param name="orders">Lista wczytanych zamówień</param>
        /// <returns name="allOrders">Zmienna z wszystkimi zamówieniami</returns>
        protected static string AllOrders(List<Order> orders)
        {
            List<Order> ordersSort = SortByClientIdAndRequestId(orders);
            String allOrders = "";
            allOrders = allOrders + Environment.NewLine + "Zamówienie 1:";
            int j = 2;
            for (int i = 0; i <= ordersSort.Count() - 1; i++)
            {                
                allOrders = allOrders + Environment.NewLine + ordersSort[i].ToString();
                i++;
                if (i < orders.Count())
                {
                    if (!ordersSort[i].ClientId.Equals(ordersSort[i - 1].ClientId)
                        || !ordersSort[i].RequestId.Equals(ordersSort[i - 1].RequestId))
                    {                                               
                        allOrders = allOrders + Environment.NewLine + "----------------------"
                             + Environment.NewLine + "Zamówienie " + j + ":";
                        j++;
                    }
                }
                i--;                                
            }
            return allOrders;
        }

        /// <summary>
        /// Metoda tworząca zmienną ze wszystkimi zamówieniami posortowanymi po wybranym polu
        /// </summary>
        /// <param name="orders">Lista wczytanych zamówień</param>
        /// <param name="field">pole po jakim ma być sortowana lista </param>
        /// <param name="AscendingOrDescending">Wybór sortowania (rosnąco czy malejąco)</param>
        /// <returns name="allOrders">Zmienna z wszystkimi zamówieniami</returns>        
        protected static string AllOrders(List<Order> orders, string field, string AscendingOrDescending)
        {
            List<Order> ordersSort = SelectingSortingMethod(orders, field, AscendingOrDescending);
            
            string allOrders = "";
            
            for (int i = 0; i <= ordersSort.Count() - 1; i++)
            {
                allOrders = allOrders + Environment.NewLine + ordersSort[i].ToString();
                allOrders = allOrders + Environment.NewLine + "------------------";                                        
            }
            return allOrders;
        }

        /// <summary>
        /// Metoda tworząca zmienną ze wszystkimi zamówieniami posortowaną po RequestId dla wybranego klienta
        /// <param name="orders">lista wczytanych zamówień</param>
        /// <param name="clientId">numer klienta dla którego mają być wyświetlone zamowienia</param>
        /// <returns name="allOrders">zmienna ze wszystkimi zamówieniami dla wybranego klienta</returns>
        protected static string ClientAllOrders(List<Order> orders, String clientId)
        {
            String allOrders = "";
            List<Order> ordersSort = SortByClientIdAndRequestId(orders);
               
                allOrders = allOrders + Environment.NewLine + "Zamówienie 1:";
                int j = 2;
                for (int i = 0; i <= ordersSort.Count() - 1; i++)
                {
                    if (ordersSort[i].ClientId.Equals(clientId))
                    {
                        allOrders = allOrders + Environment.NewLine + ordersSort[i].ToString();
                        i++;
                        if (i < orders.Count() && ordersSort[i].ClientId.Equals(clientId))
                        {
                            if (!ordersSort[i].ClientId.Equals(ordersSort[i - 1].ClientId)
                                || !ordersSort[i].RequestId.Equals(ordersSort[i - 1].RequestId))
                            {
                                allOrders = allOrders + Environment.NewLine + "----------------------"
                                     + Environment.NewLine + "Zamówienie " + j + ":";
                                j++;
                            }
                        }
                        i--;
                    }
                }                       
            return allOrders;
        }

        /// <summary>
        /// Metoda tworząca zmienną ze wszystkimi zamówieniami posortowanymi po wybranym polu dla wybranego klienta
        /// </summary>
        /// <param name="orders">lista z wczytanymi zamówieniami</param>
        /// <param name="clientId">numer klienta dla którego mają być wyświetlone zamowienia</param>
        /// <param name="field">pole według, którego ma być posortowana lista</param>
        /// <param name="AscendingOrDescending">Wybór sortowania (rosnąco czy malejąco)</param>
        /// <returns name="allOrders">zmienna ze wszystkimi zamówieniami dla wybranego klienta</returns>
        protected static string ClientAllOrders(List<Order> orders, string clientId, string field, string AscendingOrDescending)
        {
            List<Order> ordersSort = SelectingSortingMethod(orders, field, AscendingOrDescending);

            string allOrders = "";

            for (int i = 0; i <= ordersSort.Count() - 1; i++)
            {
                if (ordersSort[i].ClientId.Equals(clientId))
                {
                    allOrders = allOrders + Environment.NewLine + ordersSort[i].ToString();
                    allOrders = allOrders + Environment.NewLine + "------------------";
                }
            }
            return allOrders;
        }

        /// <summary>
        /// Metoda obliczająca średnią kwotę wszystkich zamówień
        /// </summary>
        /// <param name="orders">lista wczytanych zamówień</param>
        /// <returns name="averageOrderValue">średnia kwota wszystkich zamówień</returns>
        protected static double AverageOrderValue(List<Order> orders)
        {
            Double averageOrderValue = TotalPrice(orders) / NumbersOfOrders(orders);           
            averageOrderValue = Math.Round(averageOrderValue,2);
            return averageOrderValue;            
        }

        /// <summary>
        /// Metoda obliczająca średnią kwotę wszystkich zamówień dla wybranego klienta
        /// </summary>
        /// <param name="orders">lista wczytanych zamówień</param>
        /// <param name="clientId">numer klienta dla którego mają być wyświetlona średnia kwota zamowień</param>
        /// <returns name="averageOrderValue">średnia kwota zamówień dla wybranego klienta</returns>
        protected static double ClientAverageOrderValue(List<Order> orders, String clientId)
        {
            double averageOrderValue = ClientTotalPrice(orders, clientId) / ClientNumbersOfOrders(orders, clientId);           
            averageOrderValue = Math.Round(averageOrderValue, 2);
            return averageOrderValue;
        }

        /// <summary>
        /// Metoda wyświetlajaca ilości zamówień pogrupowane po polu 'Name'. 
        /// Jeśli jakiś rekord ma taki sam 'ClietID', 'RequestId', Name 'Name' nie jest liczony
        /// </summary>
        /// <param name="orders">lista z wczytanymi zamówieniami</param>
        /// <param name="field">pole według, którego ma być posortowana lista(Key/Count)</param>
        /// <param name="AscendingOrDescending">Wybór sortowania (rosnąco czy malejąco)</param>
        /// <returns name="grupedOrders">zmienna z liościami zamowień pogrupowanymi po nazwie</returns>
        protected static string GroupNumberOfOrders(List<Order> orders, string field, string AscendingOrDescending)
        {
            string grupedOrders = "";
            IEnumerable<Order> noDuplicatesOrders = orders.Distinct();

            grupedOrders = GrpupByNameAndSortOrders(field, AscendingOrDescending, noDuplicatesOrders);
            return grupedOrders;
        }

        /// <summary>
        /// Metoda wyświetlajaca ilości zamówień pogrupowane po polu 'Name'dla wybranego klienta
        /// Jeśli jakiś rekord ma taki sam 'ClietID', 'RequestId', Name 'Name' nie jest liczony    
        /// </summary>
        /// <param name="orders">lista z wczytanymi zamówieniami</param>
        /// <param name="clientId">numer klienta dla którego mają być wyświetlony raport</param>
        /// <param name="field">pole według, którego ma być posortowana lista(Key/Count)(</param>
        /// <param name="AscendingOrDescending">Wybór sortowania (rosnąco czy malejąco)</param>
        /// <returns name="grupedOrders">zmienna z liościami zamowień pogrupowanymi po nazwie dla wybranego kliena</returns>
        protected static string ClientGroupNumberOfOrders(List<Order> orders,string clientId, string field, string AscendingOrDescending)
        {
            string grupedOrders = "";
            IEnumerable<Order> noDuplicatesOrders = orders.Distinct().Where(Order => Order.ClientId == clientId);

           grupedOrders = GrpupByNameAndSortOrders(field, AscendingOrDescending, noDuplicatesOrders);

            return grupedOrders;
        }
       
        /// <summary>
        /// Metoda zapisująca do zmiennej zamówienia podanego przedziału wartości.
        /// Zamówienie jako kombinacja dwóch kluczy 'ClientID' i 'RequstId'
        /// </summary>
        /// <param name="orders">lista z wczytanymi zamówieniami</param>
        /// <param name="min">Dolna wartośc przedziału</param>
        /// <param name="max">Górna wartość przedziału</param>
        /// <returns name = "ordersPriceRange">Zmienna z wszystkimi zamówieniami z podanego przedziału</returns>
        public static string OrdersPriceRange(List<Order> orders, double min, double max)
        {
            List<Order> ordersSort = SortByClientIdAndRequestId(orders);
            string ordersPriceRange = "";
            double orderPrice = 0;
            int l = 0;
            int m = 0;
            int j = 2;

            ordersPriceRange = ordersPriceRange + Environment.NewLine + "Zamówienie 1:";
            
            for (int i = 0; i <= ordersSort.Count() - 1; i++)
            {
                orderPrice = orderPrice + ordersSort[i].Quantity * ordersSort[i].Price;

                i++;
                l = m;
                if (i < orders.Count())
                {
                    if (!ordersSort[i].ClientId.Equals(ordersSort[i - 1].ClientId)
                        || !ordersSort[i].RequestId.Equals(ordersSort[i - 1].RequestId))
                    {
                        if (orderPrice >= min && orderPrice <= max)
                        {
                            for (int k = l; k <= i - 1; k++)
                            {
                                ordersPriceRange = ordersPriceRange + Environment.NewLine + ordersSort[k].ToString();
                                m++;
                            }
                            ordersPriceRange = ordersPriceRange + Environment.NewLine + "Wartość zamówienia: " + orderPrice;
                            orderPrice = 0;
                            ordersPriceRange = ordersPriceRange + Environment.NewLine + "----------------------"
                                 + Environment.NewLine + "Zamówienie " + j + ":";
                            j++;
                        }
                        else
                            for (int k = l; k <= i - 1; k++)
                            {
                                m++;
                                orderPrice = 0;
                            }

                    }
                }
                else
                {
                    if (orderPrice >= min && orderPrice <= max)
                    {
                        for (int k = l; k <= i - 1; k++)
                        {
                            ordersPriceRange = ordersPriceRange + Environment.NewLine + ordersSort[k].ToString();
                            m++;
                        }
                        ordersPriceRange = ordersPriceRange + Environment.NewLine + "Wartość zamówienia: " + orderPrice;
                        orderPrice = 0;
                    }
                    else
                        for (int k = l; k <= i - 1; k++)
                        {
                            m++;
                            orderPrice = 0;
                        }
                }
                i--;
            }
            return ordersPriceRange;
       }

        #endregion

        #region Metody pomocnicze
        
        /// <summary>
        /// Metoda sortująca po ClientId i RequestId
        /// </summary>
        /// <param name="orders">Lista zamówień</param>
        /// <returns>Posortowana lista zamówień</returns>
        private static List<Order> SortByClientIdAndRequestId(List<Order> orders)
        {
            return orders.OrderBy(Order => Order.ClientId).ThenBy(Order => Order.RequestId).ToList();
        }

        /// <summary>
        /// Metoda pozwalająca wybrać sposób sortowania listy obiektów
        /// </summary>
        /// <param name="orders">lista do posortowania</param>
        /// <param name="field">pole według, którego ma być posortowana lista</param>
        /// <param name="AscendingOrDescending"> Wybór sortowania (rosnąco czy malejąco)</param>
        /// <returns name="ordersSort">posortowana lista obiektów</returns>
        /// <returns name="orders">nieposortowana lista obiektów</returns>
        private static List<Order> SelectingSortingMethod(List<Order> orders, string field, string AscendingOrDescending)
        {
            List<Order> ordersSort;

            if (AscendingOrDescending.Equals("Ascending"))
            {
                if (field.Equals("ClientId"))
                    return ordersSort = orders.OrderBy(Order => Order.ClientId).ToList();
                if (field.Equals("RequestId"))
                    return ordersSort = orders.OrderBy(Order => Order.RequestId).ToList();
                if (field.Equals("Name"))
                    return ordersSort = orders.OrderBy(Order => Order.Name).ToList();
                if (field.Equals("Quantity"))
                    return ordersSort = orders.OrderBy(Order => Order.Quantity).ToList();
                if (field.Equals("Price"))
                    return ordersSort = orders.OrderBy(Order => Order.Price).ToList();
                else
                    return orders;
            }
            if (AscendingOrDescending.Equals("Descending"))
            {
                if (field.Equals("ClientId"))
                    return ordersSort = orders.OrderByDescending(Order => Order.ClientId).ToList();
                if (field.Equals("RequestId"))
                    return ordersSort = orders.OrderByDescending(Order => Order.RequestId).ToList();
                if (field.Equals("Name"))
                    return ordersSort = orders.OrderByDescending(Order => Order.Name).ToList();
                if (field.Equals("Quantity"))
                    return ordersSort = orders.OrderByDescending(Order => Order.Quantity).ToList();
                if (field.Equals("Price"))
                    return ordersSort = orders.OrderByDescending(Order => Order.Price).ToList();
                else
                    return orders;
            }
            else
                return orders;
        }

        /// <summary>
        /// Metoda wyświetlajaca ilości zamówień pogrupowane po polu 'Name' 
        /// i dająca możliwość sortowania po 'Name' i ilości zamówień pogrupowanej po nazwie.  
        /// </summary>
        /// <param name="field">pole według, którego ma być posortowana lista(Key/Count)</param>
        /// <param name="AscendingOrDescending">Wybór sortowania (rosnąco czy malejąco)</param>
        /// <param name="noDuplicatesOrders">lista zamówień z nieduplikującymi się produktami w zamowieniu</param>
        /// <returns name="grupedOrders">zmienna z liościami zamowień pogrupowanymi po nazwie</returns>
        private static string GrpupByNameAndSortOrders(string field, string AscendingOrDescending, IEnumerable<Order> noDuplicatesOrders)
        {
            string grupedOrders = "";
            var ordersGroupByName = noDuplicatesOrders.GroupBy(
                            Order => Order.Name,
                            Order => Order.Price,
                            (Name, order) => new
                            {
                                Key = Name,
                                Count = order.Count()
                            });

            var ordersGroupByNameSort = ordersGroupByName;

            if (AscendingOrDescending.Equals("Ascending"))
                if (field.Equals("Key"))
                    ordersGroupByNameSort = ordersGroupByName.OrderBy(Order => Order.Key);
            if (field.Equals("Count"))
                ordersGroupByNameSort = ordersGroupByName.OrderBy(Order => Order.Count);
            if (AscendingOrDescending.Equals("Descending"))
                if (field.Equals("Key"))
                    ordersGroupByNameSort = ordersGroupByName.OrderByDescending(Order => Order.Key);
            if (field.Equals("Count"))
                ordersGroupByNameSort = ordersGroupByName.OrderByDescending(Order => Order.Count);

            foreach (var item in ordersGroupByNameSort)
            {
                grupedOrders = grupedOrders + item.Key + ":   " + item.Count + Environment.NewLine;
            }
            return grupedOrders;
        }

        #endregion
    }
}
