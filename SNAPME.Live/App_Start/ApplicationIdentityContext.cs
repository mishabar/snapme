using AspNet.Identity.MongoDB;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SNAPME.Live.App_Start
{
    //public class ApplicationIdentityContext : IdentityContext, IDisposable
    //{
    //    public ApplicationIdentityContext(MongoCollection users, MongoCollection roles)
    //        : base(users, roles)
    //    {
    //    }

    //    public static ApplicationIdentityContext Create()
    //    {
    //        // todo add settings where appropriate to switch server & database in your own application
    //        var client = new MongoClient(ConfigurationManager.ConnectionStrings["MongoDB"].ConnectionString);
    //        var database = client.GetDatabase(ConfigurationManager.AppSettings["DBName"]);
    //        var users = database.GetCollection<IdentityUser>("users");
    //        var roles = database.GetCollection<IdentityRole>("roles");

    //        IndexChecks.EnsureUniqueIndexOnUserName(users);
    //        IndexChecks.EnsureUniqueIndexOnEmail(users);
    //        IndexChecks.EnsureUniqueIndexOnRoleName(roles);

    //        return new ApplicationIdentityContext(users, roles);
    //    }

    //    public void Dispose()
    //    {
    //    }
    //}
}