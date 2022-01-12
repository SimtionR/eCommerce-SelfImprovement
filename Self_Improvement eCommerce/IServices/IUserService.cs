using Self_Improve_eCommerce.Models.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Self_Improve_eCommerce.IServices
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        User GetUserById(string Id);
        bool SaveChanges();
    }
}
