using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Easychit_Infrastructure.Dar.Vegetable;

namespace Easychit_Repository.Interfaces.Dar.Vegetable
{
    public interface IVegetable
    {

        Task<List<VegetableProductionDTO>> GetCollectionInchargeNames(string globalSchema, string con);
        Task<List<VegetableProductionDTO>> GettypeOfVegetable(string globalSchema, string con);
        Task<List<VegetableProductionDTO>> GetBookNobasedonCollectionIncharge(string CollectionInchargeName, string globalSchema, string con);
        Task<List<VegetableProductionDTO>> GetUomByVegName(string VegName, string ModuleName, string globalSchema, string con);
        Task<List<VegetableProductionDTO>> GetBlockName(string VegName, string globalSchema, string con);

        Task<List<VegetableProductionDTO>> GetBedNo(string VegName, string BlockName, string globalSchema, string con);
        bool SaveProductionDetails(List<VegetableProductionDTO> productionDTO, string globalSchema, string connectionstring);

    }
}