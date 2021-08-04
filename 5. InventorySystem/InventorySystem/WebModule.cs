using Autofac;
using InventorySystem.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventorySystem
{
    public class WebModule :Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductListModel>().AsSelf();
            base.Load(builder);
        }
    }
}
