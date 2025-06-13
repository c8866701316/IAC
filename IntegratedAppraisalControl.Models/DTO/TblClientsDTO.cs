using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
namespace IntegratedAppraisalControl.Models.DTO
{
    public class TblClientsDTO
    {
        public int ClientId { get; set; }

        [DisplayName("Client Name")]
        public string ClientName { get; set; }
        [DisplayName("Address1")]
        public string Address1 { get; set; }
        [DisplayName("Address2")]
        public string Address2 { get; set; }
        [DisplayName("City")]
        public string City { get; set; }
        [DisplayName("State")]
        public string State { get; set; }
        [DisplayName("Zip Code")]
        public string ZipCode { get; set; }
        public string PointOfContact { get; set; }
        public string Telephone { get; set; }
        [DisplayName("Report Year")]
        public string ReportYear { get; set; }
        public string AccountingYear { get; set; }
        public int? AquisitionCostCutOff { get; set; }
        public string LastUpdated { get; set; }
        public int? AnnualDepreciationId { get; set; }
        public int? FirstYearDepreciationD { get; set; }
        public bool? Active { get; set; }
        public string Sdf1label { get; set; }
        public string Sdf2label { get; set; }
        public string Sdf3label { get; set; }
        public string Sdf4label { get; set; }
        public string FileNo { get; set; }
        public int? ClientStatusId { get; set; }
        public string AppraisalDate { get; set; }
        public string UpdatedTo { get; set; }
        public bool Accounting { get; set; }
        public bool Insurance { get; set; }
        public bool Fmv { get; set; }
        public DateTime? AccountDataAsOf { get; set; }
        public int NextRoomNumber { get; set; }
        public int NextDepartmentNumber { get; set; }

        public string AnnualDepreciationText { get; set; }
        public string FirstYearDepreciationText { get; set; }
    }
}
