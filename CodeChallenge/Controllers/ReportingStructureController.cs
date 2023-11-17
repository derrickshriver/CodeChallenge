using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CodeChallenge.Services;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/reportingstructure")]
    public class ReportingStructureController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IReportingStructureService _reportingStructureService;

        public ReportingStructureController(
            ILogger<ReportingStructureController> logger, IReportingStructureService reportingStructureService)
        {
            _logger = logger;
            _reportingStructureService = reportingStructureService;
        }

        [HttpGet("{id}", Name = "getByEmployeeId")]
        public IActionResult GetEmployeeById(String id)
        {
            _logger.LogDebug($"Received reporting structure get request for '{id}'");

            var employee = _reportingStructureService.GetByEmployeeId(id);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }
    }
}
