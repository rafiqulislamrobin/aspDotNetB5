using Autofac;
using SocialNetworl.Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworl.Common
{
   public class CommonModule : Module
    {

        protected override void Load(ContainerBuilder builder)
        {
           

            builder.RegisterType<DatetimeUtility>().As<IDatetimeUtility>()
                .InstancePerLifetimeScope();


            base.Load(builder);
        }
    }
}
