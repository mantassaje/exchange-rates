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
    }
}
