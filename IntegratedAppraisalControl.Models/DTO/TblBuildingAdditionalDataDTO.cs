namespace IntegratedAppraisalControl.Models.DTO
{
    public class TblBuildingAdditionalDataDTO
    {
        public int BuildingAdditionalDataId { get; set; }
        public int BuildingId { get; set; }
        public string AdditionalDataName { get; set; }
        public string AdditionalDataDescription { get; set; }
        public bool Deleted { get; set; }
    }
}
