using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Self_Improve_eCommerce.Models.RequestObjects
{
    public class BasketItemRequestModel
    {
        public int ProductId { get; set; }
        //public int BasketId { get; set; }
        public int Quantity { get; set; }
        public float UnitPrice { get; set; }
        public string ProductName { get; set; }    
        public string ImageUrl { get; set; }
    }
}
