using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNAPME.Services.Interfaces
{
    public interface IInvitationService
    {
        void AddToList(string email, string referer);
    }
}
