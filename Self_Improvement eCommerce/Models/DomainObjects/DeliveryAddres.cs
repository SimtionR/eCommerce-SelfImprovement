using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Self_Improve_eCommerce.Models.DomainObjects
{
    public class DeliveryAddres
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
        public int ZipCode { get; set; }
        public int ApartamenetNumber { get; set; }

    }
}
