using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Self_Improve_eCommerce.Models.RequestObjects
{
    public class AddressRequestModel
    {
        public string City { get; set; }
        public string Street { get; set; }
        public int ZipCode { get; set; }
        public int ApartamenetNumber { get; set; }
    }
}
