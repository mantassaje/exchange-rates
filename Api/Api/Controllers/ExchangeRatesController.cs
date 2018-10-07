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
    public class ExchangeRatesController : ControllerBase
    {
        private IExchangeRateService exchangeRateService;

        public ExchangeRatesController(IExchangeRateService exchangeRateService)
        {
            this.exchangeRateService = exchangeRateService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DateTime date)
        {
            if (date >= new DateTime(2015, 01, 01))
            {
                return BadRequest(new ErrorResponse("Only dates up to the end of the year 2014 are allowed"));
            }
            else
            {
                var rates = await exchangeRateService.GetExchangeRateDifferences(date);
                return new OkObjectResult(rates);
            }
        }
    }
}
