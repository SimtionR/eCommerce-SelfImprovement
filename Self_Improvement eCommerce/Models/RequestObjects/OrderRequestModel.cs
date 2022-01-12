using Self_Improve_eCommerce.Models.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Self_Improve_eCommerce.Models.RequestObjects
{
    public class OrderRequestModel
    {
        public IEnumerable<OrderItem> ListOfOrderItems { get; set; } = new List<OrderItem>();  //instantiate it for it to not lead to bugs in EF
        public float TotalPrice { get; set; }
        public float DeliveryPrice { get; set; }

      
        public string DiscountName { get; set; }
        public float Discount { get; set; }
        public float FinalPrice { get; set; }
    }
}
