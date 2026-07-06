using System;
using System.Collections.Generic;
using System.Text;

namespace Easychit_Infrastructure.Dar.Plant_Protection
{
    public class DevisionNamesDTO
    { 
        public string vchmoduleid{ get; set; } 
        public string vchmodulename { get; set; }
    }

    public class RecomendationValveDTO
    {
        public string valve { get; set; }
        public string cropname { get; set; }
    }

    public class CategoryTypeDTO
    {
        public string vchcategoryid { get; set; }
        public string vchcategoryname { get; set; }
    }

    public class StockCompanyNamesDTO
    {
        public string vchprodcompanyname { get; set; }
        public string vchprodcompanyid { get; set; }
        public string vchproductname { get; set; }
    }

    public class EmployeeNamesDTO
    {
        public string vchemployeeid { get; set; }
        public string employeename { get; set; }
    }

    public class GeneralItemsDTO
    {
        public string itemname { get; set; }
    }

    public class DailyActivityDTO
    {
        public DateTime Date { get; set; }
        public int createdby { get; set; }
        public string vchmodulename { get; set; }
        public string vchvalve { get; set; }
        public string vchcropname { get; set; }
        public string vchwork { get; set; }
        public List<DailyActivityDTOList> lst_dailyactivity { get;set;}
    }

    public class DailyActivityDTOList
    {
        public string amount { get; set; }
        public string vchstarttime { get; set; }
        public string vchendtime { get; set; }
        public string totalhrs { get; set; }
        public string vchpurchasetype { get; set; }
        public string vchcategoryname { get; set; }
        public string vchprodcompanyname { get; set; }
        public string vchproducttype { get; set; }
        public string vchuom { get; set; }
        public string quantity { get; set; }
        public string rate { get; set; }
    }
}
