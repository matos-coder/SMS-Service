using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMSServiceImplementation.DTOS.Message;
using SMSServiceImplementation.DTOS.Report;
using SMSServiceImplementation.Interfaces.Report;
using System.Net;

namespace SMSServiceAPI.Controllers.Report
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReport report;

        public ReportController(IReport report)
        {
            this.report = report;
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetReportDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMessageGroups(Guid OrganizationId)
        {
            return Ok(await report.GetReports(OrganizationId));
        }
    }
}
