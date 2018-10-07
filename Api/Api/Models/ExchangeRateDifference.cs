using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class ExchangeRateDifference
    {
        private ExchangeRate BaseExchangeRate { get; }
        private ExchangeRate OlderExchangeRate { get; }

        /// <summary>
        /// I assume that rate is of currency with specified quantity.
        /// </summary>
        public decimal RateDifference { get
            {
                return BaseExchangeRate.Rate / BaseExchangeRate.Quantity - OlderExchangeRate.Rate / OlderExchangeRate.Quantity;
            }
        }
        public string Currency { get => BaseExchangeRate.Currency; }
        public decimal BaseRate { get => BaseExchangeRate.Rate; }
        public decimal OlderRate { get => OlderExchangeRate.Rate; }
        public decimal Quantity { get => OlderExchangeRate.Quantity; }
        public DateTime DateFrom { get => OlderExchangeRate.Date; }
        public DateTime DateTo { get => BaseExchangeRate.Date; }

        public ExchangeRateDifference(ExchangeRate baseRate, ExchangeRate olderDayRate)
        {
            if (baseRate == null) new InvalidOperationException("Base rate can not be null");
            if (olderDayRate == null) new InvalidOperationException("Older rate can not be null");
            if (baseRate.Currency != olderDayRate.Currency) new InvalidOperationException("Base rate and older rate currencies don't match");
            if (baseRate.Quantity != olderDayRate.Quantity) new InvalidOperationException("Quantities don't match");
            BaseExchangeRate = baseRate;
            OlderExchangeRate = olderDayRate;
        }
    }
}
