using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class ExchangeRateDifference
    {
        public ExchangeRate BaseExchangeRate { get; }
        public ExchangeRate PreviousDayExchangeRate { get; }
        public decimal RateDifference { get
            {
                return 0;
            }
        }
    }
}
