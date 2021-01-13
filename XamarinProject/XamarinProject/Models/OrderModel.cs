using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinProject.Models
{
    public class OrderModel
    {

        public string UserID { get; set; }
        public string OrderID { get; set; }
        public double Total { get; set;  }
        public bool isRevision { get; set; }
        public bool isBought { get; set; }


    }
}
