using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Data.Repositories
{
    public interface IUserSnapsRepository
    {
        IEnumerable<UserSnap> GetSnaps(string id);
    }
}
