using System;

namespace SocialNetworl.Common.Utility
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
