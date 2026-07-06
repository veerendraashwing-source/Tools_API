using System;
using System.Collections.Generic;
using System.Text;

namespace Easychit_Infrastructure.Dar.Dar_Masters
{
    public class UOMDTO
    {
        public string vchuomid { get; set; }
        public string vchuom_description { get; set; }
    }

    public class GSTDTO
    {
        public long gstid { get; set; }
        public string gst_percentage { get; set; }
    }

    public class ProductTypeDTO
    {
        public string vchmodulename { get; set; }
        public string vchmoduleid { get; set; }
    }

    public class CategoryTypeDTO
    {
        public string vchcategoryname { get; set; }
        public string vchcategoryid { get; set; }
    }

    public class CompanyNameDTO
    {
        public string vchprodcompanyname { get; set; }
        public string vchprodcompanyid { get; set; }
    }

    public class ProductMasterDTO
    {
        public string vchproductid { get; set; }
        public string vchproductname { get; set; }
        public string vchtype { get; set; }
        public int createdby { get; set; }
        public string vchuom { get; set; }
        public string vchcategoryid { get; set; }
        public string vchcategoryname { get; set; }
        public string vchprodcompanyid { get; set; }
        public string vchprodcompanyname { get; set; }
        public string gst_percentage { get; set; }
    }
}
