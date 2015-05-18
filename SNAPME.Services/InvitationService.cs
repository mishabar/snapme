using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SNAPME.Data.Repositories;
using SNAPME.Services.Interfaces;

namespace SNAPME.Services
{
    public class InvitationService : IInvitationService
    {
        private IInvitationRepository _invitationRepository;

        public InvitationService(IInvitationRepository invitationRepository)
        {
            _invitationRepository = invitationRepository;
        }

        public string AddToList(string email, string referer)
        {
            return _invitationRepository.AddToList(email, referer);
        }
    }
}
