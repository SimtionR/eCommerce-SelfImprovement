using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Self_Improve_eCommerce.Models.DomainObjects
{
    public class Basket
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        public IEnumerable<BasketItem> ListOfBasketItems { get; set; } = new List<BasketItem>();
    }
}
