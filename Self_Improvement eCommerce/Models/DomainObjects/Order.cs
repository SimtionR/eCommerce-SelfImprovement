using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Self_Improve_eCommerce.Models.DomainObjects
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        public int DeliveryId { get; set; }
        public IEnumerable<OrderItem> ListOfOrderItems { get; set; } = new List<OrderItem>();  //instantiate it for it to not lead to bugs in EF
        public float  TotalPrice { get; set; }
        public float DeliveryPrice { get; set; }

        [Required]
        public string DiscountName { get; set; }
        public float Discount { get; set; }
        public float FinalPrice { get; set; }


    }
}
