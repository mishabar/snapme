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
        }
    }
}
