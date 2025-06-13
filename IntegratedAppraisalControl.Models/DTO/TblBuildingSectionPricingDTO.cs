
namespace IntegratedAppraisalControl.Models.DTO
{
    public class TblBuildingSectionPricingDTO
    {
        ////BCC/ BEGIN CUSTOM CODE SECTION 
        ////ECC/ END CUSTOM CODE SECTION 
        public int BuildingSectionId { get; set; }
        public int? BuildingId { get; set; }
        public string SectionDesc { get; set; }
        public int? Yracq { get; set; }
        public int? Cost { get; set; }
        public int? HistoricalCost { get; set; }
        public byte? PercentOf { get; set; }
        public short? Isoclass { get; set; }
        public short? Life { get; set; }
    }
}
