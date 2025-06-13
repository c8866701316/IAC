namespace IntegratedAppraisalControl.Models.DTO
{
    public class TblAssetClassDTO
    {
        public int AssetClassId { get; set; }
        public int? ClientId { get; set; }
        public string ClassCode { get; set; }
        public string AssetClassName { get; set; }
        public short? DefaultLife { get; set; }
        public bool? Active { get; set; }
    }
}