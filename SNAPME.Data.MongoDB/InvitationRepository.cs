using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using SNAPME.Data.Repositories;

namespace SNAPME.Data.MongoDB
{
    public class InvitationRepository : IInvitationRepository 
    {
        private MongoCollection<Invitation> _collection;

        public InvitationRepository(MongoDatabase db)
        {
            _collection = db.GetCollection<Invitation>("invitation_list");
        }

        public void AddToList(string email, string referer)
        {
            email = email.ToLowerInvariant();

            if (_collection.Count(Query<Invitation>.EQ(i => i.email, email)) == 0)
            {
                _collection.Save(new Invitation { 
                    email = email, 
                    referer = referer,
                    ref_code = string.Concat(System.Security.Cryptography.MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(email)).Select(b => b.ToString("X2"))) });
            }
        }
    }
}
