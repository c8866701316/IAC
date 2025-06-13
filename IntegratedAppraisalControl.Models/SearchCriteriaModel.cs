using System;
using System.Collections.Generic;
using System.Text;

namespace IntegratedAppraisalControl.Models
{
    public class ClientSearchCriteriaModel
    {
        public string ClientName { get; set; }
        public string FileNumber { get; set; }
        public int UserId { get; set; }
        public int ClientId { get; set; }
        public bool IsSuperAdmin { get; set; }
    }

    public class BuildingSearchCriteriaModel
    {
        public int ClientID { get; set; }
        public bool IsSuperAdmin { get; set; }
        public int BuildingId { get; set; }
        public int BuildingChangesId { get; set; }
        public int BuildingMaintenanceId { get; set; }
    }
    public class BuildingCodeSearchCriteria
    {
        public int ClientID { get; set; }
        public bool IsSuperAdmin { get; set; }
        public string BuildingCode{ get; set; }

    }

    public class ChangeTypeCriteriaModel
    {
        public bool Active { get; set; }
        public bool IsSuperAdmin { get; set; }
    }


    /*Inventory*/

    public class InventorySearchCriteria
    {
        public int ClientID { get; set; }
        public bool IsSuperAdmin { get; set; }
        public bool IsClientAdmin { get; set; }
        public int BuildingId { get; set; }
        public int InventoryId { get; set; }
        public int RoomId { get; set; }
        public int DepartmentId { get; set; }
        public int AssetClassId { get; set; }
        public string TagNo { get; set; }
    }
    public class DepartmentSearchCriteria
    {
        public int ClientID { get; set; }
        public bool IsSuperAdmin { get; set; }
        public bool IsClientAdmin { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentCode { get; set; }
    }
    public class RoomsearchCriteria
    {
        public int ClientID { get; set; }
        public bool IsSuperAdmin { get; set; }
        public bool IsClientAdmin { get; set; }
        public int BuildingId { get; set; }
        public int RoomId { get; set; }
        public string RoomCode { get; set; }
    }


    public class UserSearchCriteria
    {
        public int ClientID { get; set; }
        public int UserId { get; set; }
        public bool IsSuperAdmin { get; set; }
        public bool IsClientAdmin { get; set; }
        public string UserName { get; set; }
    }

    public class CertifiedReportSearchCriteria
    {
        public int ClientID { get; set; }
    }

    public class InventoryMaintenanceSearchCriteria
    {
        public int InventoryMaintenanceID { get; set; }
    }

    public class TransactionsSearchCritria
    {
        public int ClientId { get; set; }
        public int superadmin { get; set; }
    }

    /*Location*/

    public class LocationSearchCriteria
    {
        public int ClientID { get; set; }
        public bool IsSuperAdmin { get; set; }
        public bool IsClientAdmin { get; set; }
        public int BaseUserId { get; set; }
        public int LocationId { get; set; }
    }
}
