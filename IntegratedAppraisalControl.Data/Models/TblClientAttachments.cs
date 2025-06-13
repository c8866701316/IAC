using System;
using System.Collections.Generic;

namespace IntegratedAppraisalControl.Data.Models
{
    public partial class TblClientAttachments
    {
        public int ClientAttachmentId { get; set; }
        public int ClientId { get; set; }
        public string AttachmentName { get; set; }
        public string AttachmentDescription { get; set; }
        public bool MainAttachment { get; set; }
    }
}
