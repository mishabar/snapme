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
    public class UserDetailsRepository : IUserDetailsRepository
    {
        private MongoCollection<UserDetails> _collection;

        public UserDetailsRepository(MongoDatabase db)
        {
            _collection = db.GetCollection<UserDetails>("user_details");
        }

        public UserDetails GetDetails(string id)
        {
            return _collection.FindOne(Query<UserDetails>.EQ(u => u.id, id)) ?? UserDetails.Empty;
        }


        public Address AddAddress(string id, Address address)
        {
            _collection.Update(Query<UserDetails>.EQ(u => u.id, id), Update<UserDetails>.Push(u => u.addresses, address));

            return address;
        }

        public Address UpdateAddress(string id, int idx, Address address)
        {
            var details = _collection.FindOne(Query<UserDetails>.EQ(u => u.id, id));
            details.addresses[idx] = address;
            _collection.Update(Query<UserDetails>.EQ(u => u.id, id), Update<UserDetails>.Set(u => u.addresses, details.addresses));

            return address;
        }


        public UserDetails Save(UserDetails userDetails)
        {
            _collection.Update(Query<UserDetails>.EQ(u => u.id, userDetails.id), 
                Update<UserDetails>
                    .Set(u => u.first_name, userDetails.first_name)
                    .Set(u => u.last_name, userDetails.last_name)
                    .Set(u => u.middle_name, userDetails.middle_name)
                    .Set(u => u.email, userDetails.email)
                );

            return userDetails;
        }


        public bool DeleteAddress(string id, int idx)
        {
            var details = _collection.FindOne(Query<UserDetails>.EQ(u => u.id, id));
            List<Address> list = new List<Address>(details.addresses);
            list.RemoveAt(idx);

            _collection.Update(Query<UserDetails>.EQ(u => u.id, id), Update<UserDetails>.Set(u => u.addresses, list.ToArray()));

            return true;
        }


        public void EnsureRecord(UserDetails userDetails)
        {
            var details = _collection.FindOne(Query<UserDetails>.EQ(d => d.id, userDetails.id));
            if (details == null)
            {
                userDetails.addresses = new Address[0];
                _collection.Insert(userDetails);
            }
        }
    }
}
