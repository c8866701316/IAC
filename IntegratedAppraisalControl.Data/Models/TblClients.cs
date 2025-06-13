using System;
using System.Collections.Generic;

namespace IntegratedAppraisalControl.Data.Models
{
    public partial class TblClients
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string PointOfContact { get; set; }
        public string Telephone { get; set; }
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
        public bool? Accounting { get; set; }
        public bool? Insurance { get; set; }
        public bool? Fmv { get; set; }
        public DateTime? AccountDataAsOf { get; set; }
        public int NextDepartmentNumber { get; set; }
        public int NextRoomNumber { get; set; }
    }
}
