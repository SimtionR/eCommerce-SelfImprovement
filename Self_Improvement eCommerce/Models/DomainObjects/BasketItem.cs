using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Self_Improve_eCommerce.Models.DomainObjects
{
    public class BasketItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int BasketId { get; set; }
        public int Quantity { get; set; }
        public float UnitPrice { get; set; }

        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ImageUrl { get; set; }




    }
}
