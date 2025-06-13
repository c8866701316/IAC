using System;
using System.Collections.Generic;

namespace IntegratedAppraisalControl.Models
{
    public partial class TblBuildings
    {
        public TblBuildings()
        {
            TblBuildingAdditionalData = new HashSet<TblBuildingAdditionalData>();
            TblBuildingImages = new HashSet<TblBuildingImages>();
        }

        public int BuildingId { get; set; }
        public int? ClientId { get; set; }
        public string BuildingCode { get; set; }
        public string BuildingName { get; set; }
        public bool? MarkForDeletion { get; set; }
        public bool? Deleted { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string ContractNo { get; set; }
        public string RevaluationNo { get; set; }
        public string EffectiveDate { get; set; }
        public string BuildingCrn { get; set; }
        public string ContentsCrn { get; set; }
        public string StainedGlass { get; set; }
        public string Pito { get; set; }
        public string GrandTotal { get; set; }
        public string Occupancy { get; set; }
        public string YearsBuilt { get; set; }
        public string YearAcq { get; set; }
        public string SuperSqft { get; set; }
        public string BasementSqft { get; set; }
        public string Stories { get; set; }
        public string ConstructionClass { get; set; }
        public string Condition { get; set; }
        public string Leased { get; set; }
        public string ExteriorWallType { get; set; }
        public string Heating { get; set; }
        public string Coolling { get; set; }
        public string RoofMaterial { get; set; }
        public string Gps { get; set; }
        public string Sprinklers { get; set; }
        public string FloodZone { get; set; }
        public string LocalFireAlarm { get; set; }
        public string CentralFireAlarm { get; set; }
        public string FireProctectionClass { get; set; }
        public string SecuritySystem { get; set; }
        public string PassengerElevator { get; set; }
        public string FreightElevator { get; set; }
        public string EmergencyGenerator { get; set; }
        public string Sdf1label { get; set; }
        public string Sdf1 { get; set; }
        public string Sdf2label { get; set; }
        public string Sdf2 { get; set; }
        public string Capacity { get; set; }
        public string AdditionalFeatures { get; set; }
        public bool? Church { get; set; }
        public bool? Structure { get; set; }
        public string Gpslat { get; set; }
        public string MonthAcq { get; set; }
        public string Cost { get; set; }
        public string CapacitySize { get; set; }
        public string CapacityType { get; set; }
        public string ConstructionType { get; set; }
        public string Gpslong { get; set; }

        public ICollection<TblBuildingAdditionalData> TblBuildingAdditionalData { get; set; }
        public ICollection<TblBuildingImages> TblBuildingImages { get; set; }
    }
}
