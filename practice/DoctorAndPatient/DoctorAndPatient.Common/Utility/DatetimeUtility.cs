﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAndPatient.Common.Utility
{
    public class DatetimeUtility : IDatetimeUtility
    {
        public DateTime Now
        {
            get
            {
                return DateTime.Now;
            }
        }
    }
}
