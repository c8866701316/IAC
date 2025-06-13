using System.ComponentModel.DataAnnotations;

namespace IntegratedAppraisalControl.Models
{
    public partial class TblClientAttachments
    {
        [Key]
        public int ClientAttachmentID { get; set; }
        public int? ClientID { get; set; }
        public string AttachmentName { get; set; }
        public string AttachmentDescription { get; set; }
        public bool? MainAttachment { get; set; }
    }
}
