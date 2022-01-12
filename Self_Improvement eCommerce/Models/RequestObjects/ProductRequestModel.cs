using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Self_Improve_eCommerce.Models.RequestObjects
{
    public class ProductRequestModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public float Price { get; set; }
        [MaxLength(1500)]
        public string Description { get; set; }
        [Required]
        public string ImageUrl { get; set; }
    }
}
