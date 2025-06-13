using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedAppraisalControl.Models.DTO
{
    public class TblAnnualDepreciationDTO
    {
        public int AnnualDepreciationId { get; set; }
        public string AnnualDepreciationText { get; set; }
        public bool? Active { get; set; }
    }
}
