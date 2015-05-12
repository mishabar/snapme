
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;

namespace SNAPME.Data.MongoDB
{
    public class DBInitializer
    {
        public static void Init(ContainerBuilder builder)
        {
            #region Autofac

            MongoClient mongoClient = new MongoClient(ConfigurationManager.ConnectionStrings["MongoDB"].ConnectionString);
            builder.Register(c => mongoClient.GetServer().GetDatabase(ConfigurationManager.AppSettings["DBName"])).AsSelf();

            builder.Register(c => new IDRepository()).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.Register(c => new SellerRepository(c.Resolve<MongoDatabase>())).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.Register(c => new ProductRepository(c.Resolve<MongoDatabase>())).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.Register(c => new SaleRepository(c.Resolve<MongoDatabase>())).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.Register(c => new UserPreferencesRepository(c.Resolve<MongoDatabase>())).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.Register(c => new InvitationRepository(c.Resolve<MongoDatabase>())).AsImplementedInterfaces().InstancePerLifetimeScope();
            
            #endregion

            #region MongoDB Related classes settings

            BsonClassMap.RegisterClassMap<Seller>(cm =>
            {
                cm.AutoMap();
                cm.SetIdMember(cm.GetMemberMap(c => c.id));
                cm.IdMemberMap.SetIdGenerator(StringObjectIdGenerator.Instance);
                cm.GetMemberMap(c => c.id).SetRepresentation(BsonType.ObjectId);
            });

            BsonClassMap.RegisterClassMap<Product>(cm =>
            {
                cm.AutoMap();
                cm.SetIdMember(cm.GetMemberMap(c => c.id));
                cm.IdMemberMap.SetIdGenerator(StringObjectIdGenerator.Instance);
                cm.GetMemberMap(c => c.id).SetRepresentation(BsonType.ObjectId);
            });

            BsonClassMap.RegisterClassMap<Sale>(cm =>
            {
                cm.AutoMap();
                cm.SetIdMember(cm.GetMemberMap(c => c.id));
                cm.IdMemberMap.SetIdGenerator(StringObjectIdGenerator.Instance);
                cm.GetMemberMap(c => c.id).SetRepresentation(BsonType.ObjectId);
            });

            BsonClassMap.RegisterClassMap<UserPreferences>(cm =>
            {
                cm.AutoMap();
                cm.SetIdMember(cm.GetMemberMap(c => c.id));
                cm.IdMemberMap.SetIdGenerator(StringObjectIdGenerator.Instance);
                cm.GetMemberMap(c => c.id).SetRepresentation(BsonType.ObjectId);
            });

            BsonClassMap.RegisterClassMap<Invitation>(cm =>
            {
                cm.AutoMap();
                cm.SetIdMember(cm.GetMemberMap(c => c.email));
            });
            #endregion
        }
    }
}
