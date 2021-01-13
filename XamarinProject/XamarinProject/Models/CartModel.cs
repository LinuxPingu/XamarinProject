using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinProject.Models
{
    public class CartModel
    {

        public string CartID { get; set; }
        public string UserID { get; set; }
        public double Total { get; set; }
        public bool isRevision { get; set; }
        public bool isBought { get; set; }

        public CartModel()
        {
        }
        public CartModel(string cartID, string userID, double total, bool isRevision, bool isBought)
        {
            CartID = cartID;
            UserID = userID;
            Total = total;
            this.isRevision = isRevision;
            this.isBought = isBought;
        }
    }
}
