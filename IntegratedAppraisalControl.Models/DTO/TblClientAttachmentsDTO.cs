namespace IntegratedAppraisalControl.Models.DTO
{
    public class TblClientAttachmentsDTO
    {
        public int ClientAttachmentID { get; set; }
        public int ClientID { get; set; }
        public string AttachmentName { get; set; }
        public string AttachmentDescription { get; set; }
        public bool MainAttachment { get; set; }
    }
}