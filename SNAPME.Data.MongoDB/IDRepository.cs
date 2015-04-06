using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using SNAPME.Data.Repositories;

namespace SNAPME.Data.MongoDB
{
    public class IDRepository : IIDRepository
    {
        public string GenerateId()
        {
            return ObjectId.GenerateNewId().ToString();
        }
    }
}
