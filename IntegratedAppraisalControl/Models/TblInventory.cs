using System;
using System.Collections.Generic;

namespace IntegratedAppraisalControl.Models
{
    public partial class TblInventory
    {
        public int InventoryId { get; set; }
        public int? ClientId { get; set; }
        public int? BuildingId { get; set; }
        public int? ValuationId { get; set; }
        public string Tag { get; set; }
        public int? RoomId { get; set; }
        public int? DeptId { get; set; }
        public int? AssetClassId { get; set; }
        public int? Qty { get; set; }
        public string Descr { get; set; }
        public string Pono { get; set; }
        public string Fund { get; set; }
        public string Funct { get; set; }
        public int? Crn { get; set; }
        public int? SoundValue { get; set; }
        public int? Cost { get; set; }
        public int? SalvageValue { get; set; }
        public string Size { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public short? Depr { get; set; }
        public short? Life { get; set; }
        public string SerNo { get; set; }
        public string Mfg { get; set; }
        public string Model { get; set; }
        public string ImageFileName { get; set; }
        public string Comment { get; set; }
        public string Sdf1 { get; set; }
        public string Sdf2 { get; set; }
        public bool? MarkForDeletion { get; set; }
        public bool? Deletion { get; set; }
        public int? Auser { get; set; }
        public DateTime? AdateTime { get; set; }
        public int? Cuser { get; set; }
        public DateTime? CdateTime { get; set; }
        public string Sdf3 { get; set; }
        public string Sdf4 { get; set; }
        public string Floor { get; set; }
        public int? Duser { get; set; }
        public DateTime? DdateTime { get; set; }
        public int? SystemNumber { get; set; }
        public int? AnnualDepreciation { get; set; }
        public int? UndepreciatedValue { get; set; }
        public int? AccumulatedDepreciation { get; set; }
    }
}
