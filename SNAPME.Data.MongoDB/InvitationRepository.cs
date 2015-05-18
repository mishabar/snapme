using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

        public string AddToList(string email, string referer)
        {
            email = email.ToLowerInvariant();
            var invitation = _collection.FindOne(Query<Invitation>.EQ(i => i.email, email));
            if (invitation == null)
            {
                invitation = new Invitation
                {
                    email = email,
                    referer = referer,
                    ref_code = GetUniqueKey(8)
                };

                _collection.Save(invitation);
            }

            return invitation.ref_code;
        }

        public static string GetUniqueKey(int maxSize)
        {
            char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
                data = new byte[maxSize];
                crypto.GetNonZeroBytes(data);
            }
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
    }
}
