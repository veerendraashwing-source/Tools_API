using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Easychit_Infrastructure.Dar.Dairy;

namespace Easychit_Repository.Interfaces.Dar.Dairy
{
    public interface IDairy
    {
        Task<List<ProductionDTO>> GetTagNo(string globalSchema, string con);
        Task<List<ProductionDTO>> GetMilkType(string globalSchema, string con, string tag);
        Task<List<DamageReasonDTO>> GetDamageReason(string globalSchema, string con);
        string GenerateNextID(string strtablename, string strcolname, int prefix, string strdate, string strColumnDate, string strPrefix);
        bool SaveProductionDetails(List<SaveProductionDTO> SaveProductionDTO, string globalSchema, string connectionstring);
    }
}
