using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Easychit_Infrastructure.Dar.Plant_Protection;

namespace Easychit_Repository.Interfaces.Dar.Plant_Protection
{
    public interface IPlantProtection
    {
        Task<List<DevisionNamesDTO>> GetPlationationDivisionNames(string globalSchema, string con);
        Task<List<RecomendationValveDTO>> GetRecomendationValve(string division, string globalSchema, string con);
        Task<List<RecomendationValveDTO>> GetRecomendationCrop(string division, string valvename, string globalSchema, string con);
        Task<List<DevisionNamesDTO>> GetDivisionNames(string globalSchema, string con);
        Task<List<CategoryTypeDTO>> GetCatergoryNames(string division, string globalSchema, string con);
        Task<List<StockCompanyNamesDTO>> GetStockCompanynames(string division, string category, string globalSchema, string con);
        Task<List<StockCompanyNamesDTO>> GetProductByCategoryAndCompnay_Missue(string division, string category, string prodCompName, string globalSchema, string con);
        Task<string> GetUOMName(string ferti, string globalSchema, string con);
        Task<string> GetRATE(string ferti, string fromdate, string globalSchema, string con);
        Task<List<EmployeeNamesDTO>> GetEmployeeNames(string gender, string globalSchema, string con);
        Task<List<GeneralItemsDTO>> GetGeneralItems(string globalSchema, string con);
        Task<string> GetvchUOM(string general, string globalSchema, string con);
        bool SavePlantDailyActivity(DailyActivityDTO dailyActivityDTO, string globalSchema, string connectionstring);









        Task<List<MachineNamesDTO>> GetMachineNames(string globalSchema, string con);
        Task<List<DivisionNamesDTO>> GetDivisions(string globalSchema, string con);
        Task<List<ValveNamesDTO>> GetRoboValves(string globalSchema, string con);
        Task<List<CropNamesDTO>> GetRoboCrops(string valve, string globalSchema, string con);
        bool SaveRoboEntry(List<RoboEntryDTO> roboEntryDTOList, string globalSchema, string connectionstring);
    }
}
