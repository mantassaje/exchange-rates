using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services.Contracts
{
    public interface IExchangeRateService
    {
        Task<IEnumerable<ExchangeRate>> GetExchangeRatesCached(DateTime date);
        Task<IEnumerable<ExchangeRate>> GetExchangeRates(DateTime date);
        Task<IEnumerable<ExchangeRateDifference>> GetExchangeRateDifferences(DateTime date);
    }
}
