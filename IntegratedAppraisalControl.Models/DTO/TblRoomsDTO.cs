using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedAppraisalControl.Models.DTO
{
    public class TblRoomsDTO
    {
        public int RoomId { get; set; }
        public int? BuildingId { get; set; }
        public int? ClientId { get; set; }
        public string RoomCode { get; set; }
        public string RoomDescription { get; set; }
        public bool? MarkForDeletion { get; set; }
        public bool? Deleted { get; set; }
        public string Floor { get; set; }
        public string BuildingCode { get; set; }
    }
}
