using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Easychit_Infrastructure.Dar.Dar_Masters;

namespace Easychit_Repository.Interfaces.Dar.Dar_Masters
{
    public interface IDarMasters
    {
        Task<List<UOMDTO>> GetUOM(string globalSchema, string con);
        Task<List<GSTDTO>> GetGST(string globalSchema, string con);
        Task<List<ProductTypeDTO>> GetVendertype(string globalSchema, string con);
        Task<List<CategoryTypeDTO>> GetCategoryType(string globalSchema, string con);
        Task<List<CompanyNameDTO>> GetProductCompanyName(string globalSchema, string con);
        bool AddProductCompanyName(CompanyNameDTO companyNameDTO, string globalSchema, string connectionString);
        bool SaveProductCreation(ProductMasterDTO objmaster, string globalSchema, string connectionstring);
        Task<List<ProductMasterDTO>> GetProductDetails(string globalSchema, string con);




        Task<List<StateNamesDTO>> GetStateNames(string globalSchema, string con);
        bool SaveVendordDetails(VendorDTO objmaster, string globalSchema, string connectionstring);
        Task<List<VendorDTO>> GetVendorDetails(string globalSchema, string con);




        bool SaveTankDetails(TanksDTO tanksDTO, string globalSchema, string connectionstring);
        Task<List<TanksDTO>> GetTanksDetails(string globalSchema, string con);
        bool SaveMainValveDetails(MainValveDTO mainValveDTO, string globalSchema, string connectionstring);
        Task<List<MainValveDTO>> GetMainValveDetails(string globalSchema, string con);
        Task<List<MainValveDTO>> GetMainValvesByTankId(string tankId, string globalSchema, string con);
        bool SaveSubValveDetails(SubValveDTO subValveDTO, string globalSchema, string connectionstring);
        Task<List<SubValveDTO>> GetSubValveDetails(string globalSchema, string con);






        Task<List<RouteNamesDTO>> GetRouteNames(string globalSchema, string con);
        bool SaveCustomerDetails(CustomerDTO customerDTO, string globalSchema, string connectionstring);
        Task<List<CustomerDTO>> GetCustomerDetails(string globalSchema, string con);



        bool SaveWeatherDetails(WeatherEntryDTO weatherentryDTO, string globalSchema, string connectionstring);




        Task<List<ServiceNosDTO>> GetServiceNos(string globalSchema, string con);
        Task<ServiceNosDTO> GetUscNoValues(string Serviceno, string globalSchema, string con);
        bool SaveElectricityBillDetails(ElectricityBillDTO electricityBillDTO, string globalSchema, string connectionString);
    }
}
