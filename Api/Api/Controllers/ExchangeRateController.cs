using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Api.Models;
using Api.Services;
using Api.Services.Contracts;
using LbExchangeRates;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeRateController : ControllerBase
    {
        private IExchangeRateService exchangeRateService;

        public ExchangeRateController(IExchangeRateService exchangeRateService)
        {
            this.exchangeRateService = exchangeRateService;
        }

        [HttpGet("Test")]
        public async Task<IEnumerable<ExchangeRate>> Test()
        {
            var rates = await exchangeRateService.GetExchangeRatesCached(DateTime.Now.AddYears(-7));
            return rates;
        }

        [HttpGet]
        public async Task<IEnumerable<ExchangeRate>> Get(DateTime date)
        {
            var rates = await exchangeRateService.GetExchangeRatesCached(date);
            return rates;
        }
    }
}
