using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using SNAPME.Data.Repositories;

namespace SNAPME.Services
{
    public class ServicesInitializer
    {
        public static void Init(ContainerBuilder builder)
        {
            builder.Register(c => new IDService(c.Resolve<IIDRepository>())).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.Register(c => new SellerService(c.Resolve<ISellerRepository>(), c.Resolve<IProductRepository>())).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.Register(c => new ProductService(c.Resolve<IProductRepository>(), c.Resolve<IUserPreferencesRepository>(), c.Resolve<ISocialFeedRepository>())).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.Register(c => new SaleService(c.Resolve<ISaleRepository>(), c.Resolve<IProductRepository>())).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.Register(c => new EmailService()).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.Register(c => new InvitationService(c.Resolve<IInvitationRepository>())).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.Register(c => new FriendsService(c.Resolve<IFriendRepository>())).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.Register(c => new AccountService(c.Resolve<IUserDetailsRepository>(), c.Resolve<IUserPreferencesRepository>(), c.Resolve<IUserSnapsRepository>(), c.Resolve<IFriendRepository>())).AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
