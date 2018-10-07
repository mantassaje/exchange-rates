using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class ExchangeRateDifference
    {
        public ExchangeRate BaseExchangeRate { get; }
        public ExchangeRate OlderExchangeRate { get; }

        //Currently I assume that rate is of currency with specified quantity.
        //Normaly I would disccused with someone to make sure if that is the case.
        public decimal RateDifference { get
            {
                return BaseExchangeRate.Rate / BaseExchangeRate.Quantity - OlderExchangeRate.Rate / OlderExchangeRate.Quantity;
            }
        }

        public ExchangeRateDifference(ExchangeRate baseRate, ExchangeRate olderDayRate)
        {
            if (baseRate == null) new InvalidOperationException("Base rate can not be null");
            if (olderDayRate == null) new InvalidOperationException("Older rate can not be null");
            if (baseRate.Currency != olderDayRate.Currency) new InvalidOperationException("Base rate and older rate currencies don't match");
            BaseExchangeRate = baseRate;
            OlderExchangeRate = olderDayRate;
        }
    }
}
