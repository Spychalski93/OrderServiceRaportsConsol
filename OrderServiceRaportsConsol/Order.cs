using System;
using System.Collections.Generic;

namespace OrderServiceRaportsConsol
{
    /// <summary>
    /// Struktura obiektu Order, przechowująca poszczególne linie zamówienia
    /// </summary>
    public class Order : IEquatable<Order>
    {
    
        #region Pola
        private string clientId;
        private ulong requestId;
        public string name;
        private uint quantity;
        private double price;

        public static List<Order> orders = new List<Order>();
        #endregion

        #region Właściwości
        public string ClientId
        {
            get  => clientId;
            set
            {
                if (value.Length <= 6)
                    clientId = value;
                else
                {
                    throw new ArgumentException(" Za długie ID klienta ");
                }
            }
        }
        public ulong RequestId { get => requestId; set => requestId = value; }
        public string Name { get => name; set => name = value; }
        public uint Quantity { get => quantity; set => quantity = value; }
        public double Price { get => price; set => price = value; }
        #endregion

        #region Konstruktory
        public Order(string clientId, ulong requestId, string name, uint quantity, double price)
        {           
            this.ClientId = clientId;
            this.RequestId = requestId;
            this.Name = name;
            this.Quantity = quantity;
            this.Price = price;
        }

        #endregion

        #region Metody
        /// <summary>
        /// Dodaje do listy (przechowującej wszystkie zamwienia) zamówienia
        /// </summary>
        /// <param name="o">Instancja klasy obiekt</param>
        public void OrdersAdd(Order o)
        {
            orders.Add(o);
        }

        override
        public String ToString()
        {
            return "clientId= " + clientId + " || "
                + "requestId= " + requestId + " || "
                + "name= " + name + " || "
                + "quantity= " + quantity + " || "
                + "price= " + price;
        }

        public bool Equals(Order other)
        {           
            if (Object.ReferenceEquals(other, null)) return false;            
            if (Object.ReferenceEquals(this, other)) return true; 
            return ClientId.Equals(other.ClientId) && RequestId.Equals(other.RequestId) && Name.Equals(other.Name);
        }
          
        public override int GetHashCode()
        {          
            int hashOrderClientId = ClientId.GetHashCode();

            int hashOrdrRequestId = RequestId.GetHashCode();

            int hashOrdrName = Name.GetHashCode();

            return hashOrderClientId ^ hashOrdrRequestId ^ hashOrdrName;
        }

        #endregion
    }
}
