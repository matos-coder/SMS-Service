using AutoMapper;
using AutoMapper.QueryableExtensions;
using IntegratedImplementation.DTOS.HRM;
using IntegratedInfrustructure.Data;
using IntegratedInfrustructure.Model.HRM;
using Microsoft.EntityFrameworkCore;
using SMSServiceImplementation.DTOS.Message;
using SMSServiceImplementation.DTOS.Report;
using SMSServiceImplementation.Interfaces.Report;
using SMSServiceInfrustructure.Model.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Azure.Core.HttpHeader;
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

        //public async Task<List<GetReportDto>> GetReports(Guid OrganizationId)
        //{
        //    var results = await _dbContext.Reports
        //        .Include(x => x.Message)
        //        .Where(x => x.OrganizationId == OrganizationId)
        //        .AsNoTracking()
        //        .ProjectTo<GetReportDto>(_mapper.ConfigurationProvider).ToListAsync();



        //    return results;
        //}



        //public async Task<List<GetReportDto>> GetReports(Guid organizationId)
        //{
        //    var reports = await _dbContext.Reports
        //        .Include(r => r.Name)
        //        .Include(r => r.MessageGroup)
        //        .Where(r => r.OrganizationId == organizationId)
        //        .ToListAsync();

        //    var reportDtos = new List<GetReportDto>();

        //    foreach (var report in reports) {
        //        var messages = await _dbContext.Messages
        //            .Where(m => m.OrganizationId == organizationId && m.MessageGroupId == report.MessageGroupId)
        //            .ToListAsync();



        //        var reportDto = new GetReportDto
        //        {
        //            OrganizationId = report.OrganizationId,
        //            MessageGroupId = report.MessageGroupId,
        //            //Message = messages.Select(m => m.Content).ToList(),
        //            GroupName = report.GroupName,
        //            Content = report.Content,
        //            Name = report.Name,
        //            NumberOfCustomer = report.NumberOfCustomer,
        //            MessageStatus = report.MessageStatus,
        //            SendTime = report.SendTime,
        //            UnSentCount = report.UnSentCount
        //        // Map other properties from the report to the reportDto
        //        // Example: GroupName = report.GroupName, Content = report.Content, etc.
        //        };
        //            reportDtos.Add(reportDto);
        //        }

        //    return reportDtos;
        //}
        public async Task<List<GetReportDto>> GetReports(Guid messageGroupId)
        {
            var messages = await _dbContext.Messages
                .Include(x => x.MessageGroup)
                .Include(x => x.MessageGroup.Organization)
                .Include(x => x.MessageGroup.GroupPhoneNumbers)
                //.Include(x => x.MessageGroup.OrganizationId)
                .Where(x => x.MessageGroupId == messageGroupId)
                .Select(x => new MessageGetDto
                {
                    Id = x.Id,
                    Content = x.Content,
                    MessageGroupId = x.MessageGroupId,
                    OrganizationId = x.OrganizationId,
                    MessageGroup = x.MessageGroup.GroupName,
                    MessageStatus = x.MessageStatus.ToString(),
                    Language = x.Language.ToString(),
                    IsApproved = x.IsApproved,
                    TextSize = x.TextSize,
                    NumberOfCustomer = x.MessageGroup.GroupPhoneNumbers.Count(),
                    OrganizationName = x.MessageGroup.Organization.Name,

                }).ToListAsync();
            var reports = new List<GetReportDto>();
            foreach (var message in messages)
            {
                // Generate the report based on the message
                var reportq = GenerateReportFromMessage(message);

                // Add the report to the list
                reports.Add(reportq);
            }
            return reports;
            
        }
        public GetReportDto GenerateReportFromMessage(MessageGetDto message)
        {
            
            // Perform the necessary logic to generate the report based on the message
            // You can access the properties of the message object and construct the report accordingly

            var reportw = new GetReportDto
            {
                // Set the report properties based on the message
                // For example:
                OrganizationId = message.OrganizationId,
                MessageGroupId = message.MessageGroupId,
                Content = message.Content,
                GroupName = message.MessageGroup,
                NumberOfCustomer = message.NumberOfCustomer,
                //Name = message.OrganizationName,
                //MessageStatus = message.MessageStatus,
                Language = message.Language,

                // Set other report properties based on the message
            };

            return reportw;
        }
    }
}
