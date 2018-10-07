using Api.Models;
using Api.Services.Contracts;
using LbExchangeRates;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services
{
    public class ExchangeRateService : IExchangeRateService
    {
        private string webServiceUrl = "http://www.lb.lt/webservices/ExchangeRates/ExchangeRates.asmx";
        private IMemoryCache cache;

        public ExchangeRateService(IMemoryCache cache)
        {
            this.cache = cache;
        }

        /// <summary>
        /// Caching is implemented on soap call level. It is the responses of soap that will be cached.
        /// </summary>
        public async Task<IEnumerable<ExchangeRate>> GetExchangeRatesCached(DateTime date)
        {
            date = date.Date;
            IEnumerable<ExchangeRate> rates = null;
            var cacheKey = new ExchangeRateCacheKey(date);
            if (!cache.TryGetValue(cacheKey, out rates))
            {
                rates = await GetExchangeRates(date);
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1));
                cache.Set(cacheKey, rates, cacheEntryOptions);
            }
            return rates;
        }

        public async Task<IEnumerable<ExchangeRate>> GetExchangeRates(DateTime date)
        {
            using (var client = new ExchangeRatesSoapClient(ExchangeRatesSoapClient.EndpointConfiguration.ExchangeRatesSoap, webServiceUrl))
            {
                var soapResult = await client.getExchangeRatesByDateAsync(new LbExchangeRates.getExchangeRatesByDateRequest(date.Date.ToString("yyyy-MM-dd HH:mm:ss")));
                var exchangeRateElements = soapResult.getExchangeRatesByDateResult.Elements();
                var exchangeRates = exchangeRateElements.Select(v => new ExchangeRate()
                {
                    Date = Convert.ToDateTime(v.Element("date").Value),
                    Currency = v.Element("currency").Value,
                    Quantity = Convert.ToInt32(v.Element("quantity").Value),
                    Rate = Convert.ToDecimal(v.Element("rate").Value),
                    Unit = v.Element("unit").Value
                });
                return exchangeRates;
            }
        }

        public async Task<IEnumerable<ExchangeRateDifference>> GetExchangeRateDifferences(DateTime date)
        {
            var baseRates = await GetExchangeRatesCached(date);
            var olderRates = await GetExchangeRatesCached(date.AddDays(-1));
            var rateDifferences = new List<ExchangeRateDifference>();
            foreach(var baseRate in baseRates)
            {
                //Selection of older rate is lenient. 
                //If there will be more items of the same currency or non then it will try to resolve it anyways.
                var olderRate = olderRates.FirstOrDefault(v => v.Currency == baseRate.Currency);
                if (olderRate == null) continue;
                var rateDifference = new ExchangeRateDifference(baseRate, olderRate);
                rateDifferences.Add(rateDifference);
            }
            var rateDifferencesOrdered = rateDifferences.OrderByDescending(v => v.RateDifference);
            return rateDifferencesOrdered;
        }
    }
}
