using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Data.Repositories
{
    public interface ISaleRepository
    {
        Sale GetById(string id, bool active);
    }
}
