using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Easychit_Infrastructure.Dar.Horticulture;
using Easychit_Infrastructure.Dar.Dar_Masters;

namespace Easychit_Repository.Interfaces.Dar.Horticulture
{
    public interface IHorticulture
    {
        Task<List<SubValveDTO>> GetSubValvesByTankAndMainValve(string tankId, string mainValve, string globalSchema, string con);
        Task<List<FruitsDTO>> GetFruits(string globalSchema, string con);
        bool SavePlantationDetails(PlantationDTO plantation, string globalSchema, string connectionstring);
        Task<List<FruitsDTO>> GetUOMByFruit(string productType, string ModuleName, string globalSchema, string con);
        Task<List<AreaValvesDTO>> GetAreaValves(string productType,string globalSchema, string con);
        Task<List<AreaValvesDTO>> GetRows(string productType,string valve,string globalSchema, string con);
        bool SaveProductionDetails(ProductionDTO productionDTO, string globalSchema, string connectionstring);
    }
}
