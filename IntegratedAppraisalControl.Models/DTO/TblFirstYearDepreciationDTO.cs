using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedAppraisalControl.Models.DTO
{
    public class TblFirstYearDepreciationDTO
    {
        public int FirstYearDepreciationId { get; set; }
        public string FirstYearDepreciationText { get; set; }
        public bool? Active { get; set; }
    }
}
