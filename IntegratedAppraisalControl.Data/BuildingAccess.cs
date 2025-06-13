using IntegratedAppraisalControl.Data.Models;
using IntegratedAppraisalControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using IntegratedAppraisalControl.Models.DTO;

namespace IntegratedAppraisalControl.Data
{
    public class BuildingAccess : IBuildingAccess
    {
        private readonly IntegratedAppraisalControlContext _dbContext;
        public BuildingAccess()
        {
            _dbContext = new IntegratedAppraisalControlContext();
        }
        public async Task<List<TblBuildings>> GetBuildingListBySearchCriteriaAsync(BuildingSearchCriteriaModel criteria)
        {

            return await (from bul in _dbContext.TblBuildings
                          where bul.ClientId == criteria.ClientID
                          && bul.Deleted == (criteria.IsSuperAdmin ? bul.Deleted : false) /*If Superadmin show all*/
                          select new TblBuildings
                          {
                              BuildingCode = bul.BuildingCode,
                              BuildingName = bul.BuildingName,
                              Address1 = bul.Address1,
                              City = bul.City,
                              BuildingId = bul.BuildingId
                          }).ToListAsync();
        }


        public async Task<List<TblBuildingsDTO>> GetBuildingListDDlBySearchCriteriaAsync(BuildingSearchCriteriaModel criteria)
        {
            return await (from bul in _dbContext.TblBuildings
                          where bul.ClientId == criteria.ClientID
                          && bul.Deleted == (criteria.IsSuperAdmin ? bul.Deleted : false) /*If Superadmin show all*/
                          select new TblBuildingsDTO
                          {
                              BuildingCode = bul.BuildingCode + " : " + bul.BuildingName,
                              BuildingName = bul.BuildingName,
                              Address1 = bul.Address1,
                              City = bul.City,
                              BuildingId = bul.BuildingId
                          }).ToListAsync();
        }



        public async Task<List<TblBuildingImages>> GetBuildingImagesSearchCriteriaAsync(BuildingSearchCriteriaModel criteria)
        {
            return await _dbContext.TblBuildingImages.Where(m => m.BuildingId == criteria.BuildingId).ToListAsync();
        }
        public async Task<bool> CheckBuildingCodeExistance(BuildingCodeSearchCriteria criteria)
        {
            return await _dbContext.TblBuildings.AnyAsync(m => m.BuildingCode == criteria.BuildingCode && m.ClientId == criteria.ClientID);
        }
        public async Task<TblBuildings> AddUpdateBuilding(TblBuildings tblBuildings)
        {
            if (tblBuildings.BuildingId == 0)
            {
                await _dbContext.TblBuildings.AddAsync(tblBuildings);
            }
            else
            {
                _dbContext.TblBuildings.Update(tblBuildings);
            }
            _dbContext.SaveChanges();
            _dbContext.Entry(tblBuildings).State = EntityState.Detached;
            return tblBuildings;
        }
        public async Task<TblBuildingsDTO> GetBuildingDetailsAsync(BuildingSearchCriteriaModel criteria)
        {
            return await (from bul in _dbContext.TblBuildings
                          join client in _dbContext.TblClients on bul.ClientId equals client.ClientId
                          where bul.BuildingId == criteria.BuildingId && bul.ClientId == criteria.ClientID
                          select new TblBuildingsDTO
                          {
                              BuildingId = bul.BuildingId,
                              ClientId = bul.ClientId,
                              BuildingCode = bul.BuildingCode,
                              BuildingName = bul.BuildingName,
                              MarkForDeletion = bul.MarkForDeletion,
                              Deleted = bul.Deleted,
                              Address1 = bul.Address1,
                              Address2 = bul.Address2,
                              City = bul.City,
                              State = bul.State,
                              ZipCode = bul.ZipCode,
                              ContractNo = bul.ContractNo,
                              RevaluationNo = bul.RevaluationNo,
                              EffectiveDate = bul.EffectiveDate,
                              BuildingCrn = bul.BuildingCrn,
                              ContentsCrn = bul.ContentsCrn,
                              StainedGlass = bul.StainedGlass,
                              Pito = bul.Pito,
                              GrandTotal = bul.GrandTotal,
                              Occupancy = bul.Occupancy,
                              YearsBuilt = bul.YearsBuilt,
                              YearAcq = bul.YearAcq,
                              SuperSqft = bul.SuperSqft,
                              BasementSqft = bul.BasementSqft,
                              Stories = bul.Stories,
                              ConstructionClass = bul.ConstructionClass,
                              Condition = bul.Condition,
                              Leased = bul.Leased,
                              ExteriorWallType = bul.ExteriorWallType,
                              Heating = bul.Heating,
                              Coolling = bul.Coolling,
                              RoofMaterial = bul.RoofMaterial,
                              Gps = bul.Gps,
                              Sprinklers = bul.Sprinklers,
                              FloodZone = bul.FloodZone,
                              LocalFireAlarm = bul.LocalFireAlarm,
                              CentralFireAlarm = bul.CentralFireAlarm,
                              FireProctectionClass = bul.FireProctectionClass,
                              SecuritySystem = bul.SecuritySystem,
                              PassengerElevator = bul.PassengerElevator,
                              FreightElevator = bul.FreightElevator,
                              EmergencyGenerator = bul.EmergencyGenerator,
                              Sdf1label = bul.Sdf1label,
                              Sdf1 = bul.Sdf1,
                              Sdf2label = bul.Sdf2label,
                              Sdf2 = bul.Sdf2,
                              Capacity = bul.Capacity,
                              AdditionalFeatures = bul.AdditionalFeatures,
                              Church = bul.Church,
                              Structure = bul.Structure,
                              Gpslat = bul.Gpslat,
                              MonthAcq = bul.MonthAcq,
                              Cost = bul.Cost,
                              CapacitySize = bul.CapacitySize,
                              CapacityType = bul.CapacityType,
                              ConstructionType = bul.ConstructionType,
                              Gpslong = bul.Gpslong,
                              AppraisalDate = client.AppraisalDate,
                          }
                          ).FirstOrDefaultAsync();
        }
        public async Task<List<TblBuildingSectionPricing>> GetBuildingSectionPricingAsync(BuildingSearchCriteriaModel criteria)
        {
            return await (from bulSec in _dbContext.TblBuildingSectionPricing
                          join bul in _dbContext.TblBuildings on bulSec.BuildingId equals bul.BuildingId
                          where bulSec.BuildingId == criteria.BuildingId
                          //&& bul.Deleted == (criteria.IsSuperAdmin ? bul.Deleted : false) /*If Superadmin show all*/
                          select new TblBuildingSectionPricing
                          {
                              BuildingSectionId = bulSec.BuildingSectionId,
                              BuildingId = bulSec.BuildingId,
                              SectionDesc = bulSec.SectionDesc,
                              Yracq = bulSec.Yracq,
                              Cost = bulSec.Cost,
                              HistoricalCost = bulSec.HistoricalCost,
                              PercentOf = bulSec.PercentOf,
                              Isoclass = bulSec.Isoclass,
                              Life = bulSec.Life
                          }).ToListAsync();
        }
        public async Task<List<TblBuildingAdditionalData>> GetBuildingAdditionalDataAsync(BuildingSearchCriteriaModel criteria)
        {
            return await (from bulAddData in _dbContext.TblBuildingAdditionalData
                          join bul in _dbContext.TblBuildings on bulAddData.BuildingId equals bul.BuildingId
                          where bulAddData.BuildingId == criteria.BuildingId
                          //&& bul.Deleted == (criteria.IsSuperAdmin ? bul.Deleted : false) /*If Superadmin show all*/
                          select new TblBuildingAdditionalData
                          {
                              BuildingAdditionalDataId = bulAddData.BuildingAdditionalDataId,
                              BuildingId = bulAddData.BuildingId,
                              AdditionalDataName = bulAddData.AdditionalDataName,
                              Deleted = bulAddData.Deleted,
                              AdditionalDataDescription = bulAddData.AdditionalDataDescription
                          }).ToListAsync();
        }
        public async Task<List<TblBuildingChangesDTO>> GetBuildingChangesAsync(BuildingSearchCriteriaModel criteria)
        {
            return await (from bulChanges in _dbContext.TblBuildingChanges
                          join bul in _dbContext.TblBuildings on bulChanges.BuildingId equals bul.BuildingId
                          join chngType in _dbContext.TblChangeType on bulChanges.ChangeTypeId equals chngType.ChangeTypeId
                          where bulChanges.BuildingId == criteria.BuildingId
                          //&& bul.Deleted == (criteria.IsSuperAdmin ? bul.Deleted : false) /*If Superadmin show all*/
                          select new TblBuildingChangesDTO
                          {
                              ChangeType = chngType.ChangeType,
                              BuildingChangesId = bulChanges.BuildingChangesId,
                              BuildingId = bulChanges.BuildingId,
                              ChangeTypeId = bulChanges.ChangeTypeId,
                              Description = bulChanges.Description,
                              MonthAcq = bulChanges.MonthAcq,
                              YearAcq = bulChanges.YearAcq,
                              Cost = bulChanges.Cost,
                              Auser = bulChanges.Auser,
                              AdateTime = bulChanges.AdateTime,
                              Cuser = bulChanges.Cuser,
                              CdateTime = bulChanges.CdateTime
                          }).ToListAsync();
        }
        public async Task<List<TblBuildingMaintenanceDTO>> GetBuildingMaintenanceAsync(BuildingSearchCriteriaModel criteria)
        {
            return await (from bulMaint in _dbContext.TblBuildingMaintenance
                          join bul in _dbContext.TblBuildings on bulMaint.BuildingId equals bul.BuildingId
                          join chngType in _dbContext.TblChangeType on bulMaint.ChangeTypeId equals chngType.ChangeTypeId
                          where bulMaint.BuildingId == criteria.BuildingId
                          //&& bul.Deleted == (criteria.IsSuperAdmin ? bul.Deleted : false) /*If Superadmin show all*/
                          select new TblBuildingMaintenanceDTO
                          {
                              ChangeType = chngType.ChangeType,
                              BuildingMaintenanceId = bulMaint.BuildingMaintenanceId,
                              BuildingId = bulMaint.BuildingId,
                              ChangeTypeId = bulMaint.ChangeTypeId,
                              Description = bulMaint.Description,
                              MonthAcq = bulMaint.MonthAcq,
                              YearAcq = bulMaint.YearAcq,
                              Cost = bulMaint.Cost,
                              Auser = bulMaint.Auser,
                              AdateTime = bulMaint.AdateTime,
                              Cuser = bulMaint.Cuser,
                              CdateTime = bulMaint.CdateTime
                          }).ToListAsync();
        }

        public async Task<TblBuildingChangesDTO> GetBuildingChangeAsync(BuildingSearchCriteriaModel criteria)
        {
            return await (from bulChanges in _dbContext.TblBuildingChanges
                          join bul in _dbContext.TblBuildings on bulChanges.BuildingId equals bul.BuildingId
                          join chngType in _dbContext.TblChangeType on bulChanges.ChangeTypeId equals chngType.ChangeTypeId
                          where 
                          //bulChanges.BuildingId == bul.BuildingId &&
                          //&& bul.Deleted == (criteria.IsSuperAdmin ? bul.Deleted : false) /*If Superadmin show all*/
                          bulChanges.BuildingChangesId == criteria.BuildingChangesId
                          select new TblBuildingChangesDTO
                          {
                              ChangeType = chngType.ChangeType,
                              BuildingChangesId = bulChanges.BuildingChangesId,
                              BuildingId = bulChanges.BuildingId,
                              ChangeTypeId = bulChanges.ChangeTypeId,
                              Description = bulChanges.Description,
                              MonthAcq = bulChanges.MonthAcq,
                              YearAcq = bulChanges.YearAcq,
                              Cost = bulChanges.Cost,
                              Auser = bulChanges.Auser,
                              AdateTime = bulChanges.AdateTime,
                              Cuser = bulChanges.Cuser,
                              CdateTime = bulChanges.CdateTime
                          }).FirstOrDefaultAsync();
        }
        public async Task<TblBuildingChangesDTO> AddUpdateBuildingChangesAsync(TblBuildingChanges tblBuildingChanges)
        {

            if (tblBuildingChanges.BuildingChangesId == 0)
            {
                await _dbContext.TblBuildingChanges.AddAsync(tblBuildingChanges);
            }
            else
            {
                _dbContext.TblBuildingChanges.Update(tblBuildingChanges);
            }
            _dbContext.SaveChanges();
            _dbContext.Entry(tblBuildingChanges).State = EntityState.Detached;

            return await (from bulChanges in _dbContext.TblBuildingChanges
                          join bul in _dbContext.TblBuildings on bulChanges.BuildingId equals bul.BuildingId
                          join chngType in _dbContext.TblChangeType on bulChanges.ChangeTypeId equals chngType.ChangeTypeId
                          where bulChanges.BuildingChangesId == tblBuildingChanges.BuildingChangesId
                          //&& bul.Deleted == (criteria.IsSuperAdmin ? bul.Deleted : false) /*If Superadmin show all*/
                          select new TblBuildingChangesDTO
                          {
                              ChangeType = chngType.ChangeType,
                              BuildingChangesId = bulChanges.BuildingChangesId,
                              BuildingId = bulChanges.BuildingId,
                              ChangeTypeId = bulChanges.ChangeTypeId,
                              Description = bulChanges.Description,
                              MonthAcq = bulChanges.MonthAcq,
                              YearAcq = bulChanges.YearAcq,
                              Cost = bulChanges.Cost,
                              Auser = bulChanges.Auser,
                              AdateTime = bulChanges.AdateTime,
                              Cuser = bulChanges.Cuser,
                              CdateTime = bulChanges.CdateTime
                          }).FirstOrDefaultAsync();
        }
        public async Task<TblBuildingMaintenanceDTO> AddUpdateBuildingMaintenanceAsync(TblBuildingMaintenance tblBuildingMaintenance)
        {

            if (tblBuildingMaintenance.BuildingMaintenanceId == 0)
            {
                await _dbContext.TblBuildingMaintenance.AddAsync(tblBuildingMaintenance);
            }
            else
            {
                _dbContext.TblBuildingMaintenance.Update(tblBuildingMaintenance);
            }
            _dbContext.SaveChanges();
            _dbContext.Entry(tblBuildingMaintenance).State = EntityState.Detached;

            return await (from bulMaint in _dbContext.TblBuildingMaintenance
                          join bul in _dbContext.TblBuildings on bulMaint.BuildingId equals bul.BuildingId
                          join chngType in _dbContext.TblChangeType on bulMaint.ChangeTypeId equals chngType.ChangeTypeId
                          where bulMaint.BuildingMaintenanceId == tblBuildingMaintenance.BuildingMaintenanceId
                          //&& bul.Deleted == (criteria.IsSuperAdmin ? bul.Deleted : false) /*If Superadmin show all*/
                          select new TblBuildingMaintenanceDTO
                          {
                              ChangeType = chngType.ChangeType,
                              BuildingMaintenanceId = bulMaint.BuildingMaintenanceId,
                              BuildingId = bulMaint.BuildingId,
                              ChangeTypeId = bulMaint.ChangeTypeId,
                              Description = bulMaint.Description,
                              MonthAcq = bulMaint.MonthAcq,
                              YearAcq = bulMaint.YearAcq,
                              Cost = bulMaint.Cost,
                              Auser = bulMaint.Auser,
                              AdateTime = bulMaint.AdateTime,
                              Cuser = bulMaint.Cuser,
                              CdateTime = bulMaint.CdateTime
                          }).FirstOrDefaultAsync();
        }
        public async Task<TblBuildingMaintenanceDTO> GetBuildingMaintenanceByIdAsync(BuildingSearchCriteriaModel criteria)
        {
            return await (from bulMaint in _dbContext.TblBuildingMaintenance
                          join bul in _dbContext.TblBuildings on bulMaint.BuildingId equals bul.BuildingId
                          join chngType in _dbContext.TblChangeType on bulMaint.ChangeTypeId equals chngType.ChangeTypeId
                          where bulMaint.BuildingMaintenanceId == criteria.BuildingMaintenanceId
                          //&& bul.Deleted == (criteria.IsSuperAdmin ? bul.Deleted : false) /*If Superadmin show all*/
                          select new TblBuildingMaintenanceDTO
                          {
                              ChangeType = chngType.ChangeType,
                              BuildingMaintenanceId = bulMaint.BuildingMaintenanceId,
                              BuildingId = bulMaint.BuildingId,
                              ChangeTypeId = bulMaint.ChangeTypeId,
                              Description = bulMaint.Description,
                              MonthAcq = bulMaint.MonthAcq,
                              YearAcq = bulMaint.YearAcq,
                              Cost = bulMaint.Cost,
                              Auser = bulMaint.Auser,
                              AdateTime = bulMaint.AdateTime,
                              Cuser = bulMaint.Cuser,
                              CdateTime = bulMaint.CdateTime
                          }).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteBuildingChanges(int BuildingChangesId)
        {
            if (_dbContext.TblBuildingChanges.Any(m => m.BuildingChangesId == BuildingChangesId))
            {
                _dbContext.TblBuildingChanges.Remove(await _dbContext.TblBuildingChanges.Where(m => m.BuildingChangesId == BuildingChangesId).FirstOrDefaultAsync());
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteBuildingMaintanance(int BuildingMaintenanceId)
        {
            if (_dbContext.TblBuildingMaintenance.Any(m => m.BuildingMaintenanceId == BuildingMaintenanceId))
            {
                _dbContext.TblBuildingMaintenance.Remove(await _dbContext.TblBuildingMaintenance.Where(m => m.BuildingMaintenanceId == BuildingMaintenanceId).FirstOrDefaultAsync());
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}