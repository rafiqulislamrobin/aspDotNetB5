using System;

namespace BuyAndSell.Data.Common.Utility
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
