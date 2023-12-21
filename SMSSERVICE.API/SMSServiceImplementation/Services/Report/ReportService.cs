using AutoMapper;
using IntegratedInfrustructure.Data;
using IntegratedInfrustructure.Model.HRM;
using Microsoft.EntityFrameworkCore;
using SMSServiceImplementation.DTOS.Message;
using SMSServiceImplementation.DTOS.Report;
using SMSServiceImplementation.Interfaces.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static IntegratedInfrustructure.Data.EnumList;

namespace SMSServiceImplementation.Services.Report
{
    public class ReportService : IReport
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public ReportService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<GetReportDto>> GetReports(Guid? organizationId)
        {
            var results = await _dbContext.Reports
             .Include(x => x.MessageGroup.Organization)
             .Select(x => new GetReportDto
             {
                 //Content = x.Content,
                 //GroupName = x.GroupName,
                 //NumberOfCustomer = x.NumberOfCustomer,
                 MessageStatus = x.MessageStatus,
                 Name = x.Name,
                 OrganizationId = x.MessageGroup.OrganizationId,
             }).ToListAsync();
            if (organizationId != null)
            {
                results = results.Where(x => x.OrganizationId == organizationId).ToList();
            }

            return results;
        }
    }
}
