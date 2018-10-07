using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public struct ExchangeRateCacheKey
    {
        public DateTime Date { get; set; }

        public ExchangeRateCacheKey(DateTime date)
        {
            Date = date;
        }
    }
}
