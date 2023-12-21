using IntegratedInfrustructure.Model.HRM;
using SMSServiceInfrustructure.Model.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IntegratedInfrustructure.Data.EnumList;

namespace SMSServiceImplementation.DTOS.Report
{
    public class GetReportDto
    {
        public Guid OrganizationId { get; set; }
        public Organization Name { get; set; }
        public MessageGroup GroupName { get; set; }
        public MessageGroup MessageGroup { get; set; }
        public MessageGroup Content { get; set; }
        public MessageGroup NumberOfCustomer { get; set; }
        public MessageStatus MessageStatus { get; set; }
        public DateTime SendTime { get; set; }
        public int UnSentCount { get; set; }
    }
}
