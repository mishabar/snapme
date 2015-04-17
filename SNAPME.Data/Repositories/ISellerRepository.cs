using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Data.Repositories
{
    public interface ISellerRepository
    {
        Seller Save(Seller seller);

        IEnumerable<Seller> GetAll();

        Seller GetById(string id);

        void Archive(string id);
    }
}
