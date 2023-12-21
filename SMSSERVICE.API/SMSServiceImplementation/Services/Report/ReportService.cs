using AutoMapper;
using AutoMapper.QueryableExtensions;
using IntegratedImplementation.DTOS.HRM;
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

        public async Task<List<GetReportDto>> GetReports(Guid OrganizationId)
        {
            var results = await _dbContext.Reports
                .Where(x => x.OrganizationId == OrganizationId)
                .AsNoTracking()
                .ProjectTo<GetReportDto>(_mapper.ConfigurationProvider).ToListAsync();


            

            return results;
        }
    }
}
