using AttendanceSystem.Areas.Admin.Models;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceSystem
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AttendanceModel>().AsSelf();
            base.Load(builder);
        }
    }
}
