using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR.ATS.Domain.Candidate;
using HR.ATS.Domain.Common;
using HR.ATS.Infrastructure.Repository.Common;
using HR.ATS.WebAPI.Security.Roles;
using Microsoft.AspNetCore.Http;
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
        private readonly IRepository<Candidate> _candidateRepository;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IRepository<Candidate> candidateRepository)
        {
            _logger = logger;
            _candidateRepository = candidateRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var context = HttpContext;
            var candidate = new Candidate(new Name("Pedro"));
            await _candidateRepository.CreateAsync(candidate);
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