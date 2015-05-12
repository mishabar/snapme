using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Data.Repositories
{
    public interface IInvitationRepository
    {
        void AddToList(string email, string referer);
    }
}
