using System;
using System.Collections.Generic;
using System.Linq;
using HR.ATS.WebAPI.Security.Roles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HR.ATS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    // [TnfRoleAuthorize(RolesConstants.AtsCandidate)]
    [TnfRoleAuthorize(RolesConstants.AtsRecruiter)]
    [TnfRoleAuthorize(RolesConstants.AtsCandidate)]
    public class WeatherForecastController : TnfController
    {
        private static readonly string[] Summaries =
        {
            "Freezing",
            "Bracing",
            "Chilly",
            "Cool",
            "Mild",
            "Warm",
            "Balmy",
            "Hot",
            "Sweltering",
            "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5)
                             .Select(
                                 index => new WeatherForecast
                                 {
                                     Date = DateTime.Now.AddDays(index),
                                     TemperatureC = rng.Next(-20, 55),
                                     Summary = Summaries[rng.Next(Summaries.Length)]
                                 }
                             )
                             .ToArray();
        }
    }
}