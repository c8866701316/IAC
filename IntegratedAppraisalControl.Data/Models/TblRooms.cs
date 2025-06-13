using System;
using System.Collections.Generic;

namespace IntegratedAppraisalControl.Data.Models
{
    public partial class TblRooms
    {
        public int RoomId { get; set; }
        public int? BuildingId { get; set; }
        public int? ClientId { get; set; }
        public string RoomCode { get; set; }
        public string RoomDescription { get; set; }
        public bool? MarkForDeletion { get; set; }
        public bool? Deleted { get; set; }
        public string Floor { get; set; }
    }
}
