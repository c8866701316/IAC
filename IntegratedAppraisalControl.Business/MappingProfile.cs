using AutoMapper;
using IntegratedAppraisalControl.Data.Models;
using IntegratedAppraisalControl.Models.DTO;

namespace IntegratedAppraisalControl.Business
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TblUsers, TblUsersDTO>().ReverseMap();
            CreateMap<TblClients, TblClientsDTO>().ReverseMap();
            CreateMap<TblBuildings, TblBuildingsDTO>().ReverseMap();
            CreateMap<TblBuildingSectionPricing, TblBuildingSectionPricingDTO>().ReverseMap();
            CreateMap<TblBuildingAdditionalData, TblBuildingAdditionalDataDTO>().ReverseMap();
            CreateMap<TblBuildingChanges, TblBuildingChangesDTO>().ReverseMap();
            CreateMap<TblBuildingMaintenance, TblBuildingMaintenanceDTO>().ReverseMap();
            CreateMap<TblChangeType, TblChangeTypeDTO>().ReverseMap();
            CreateMap<TblInventory, TblInventoryDTO>().ReverseMap();
            CreateMap<TblRooms, TblRoomsDTO>().ReverseMap();
            CreateMap<TblAssetClass, TblAssetClassDTO>().ReverseMap();
            CreateMap<TblDepartments, TblDepartmentsDTO>().ReverseMap();
            CreateMap<TblClientAttachments, TblClientAttachmentsDTO>().ReverseMap();
            CreateMap<TblInventoryMaintenance, TblInventoryMaintenanceDTO>().ReverseMap();
            CreateMap<TblAnnualDepreciation, TblAnnualDepreciationDTO>().ReverseMap();
            CreateMap<TblClientStatus, TblClientStatusDTO>().ReverseMap();
        }
    }
}