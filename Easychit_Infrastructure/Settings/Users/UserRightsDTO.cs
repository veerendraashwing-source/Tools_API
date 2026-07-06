using System;
using System.Collections.Generic;
using System.Text;

namespace Easychit_Infrastructure.Settings.Users
{
    public class UserRightsAgroParentModuleDTO
    {
        public long pparentmoduleid { set; get; }
        public string pparentmodulename { set; get; }
        public List<UserRightsAgroSubModuleDTO> lstSubModuleDTO { set; get; }
    }
    public class UserRightsAgroSubModuleDTO
    {
        public long psubmoduleid { set; get; }
        public string psubmodulename { set; get; }
        public List<UserRightsAgroFormsDTO> lstFormsDTO { set; get; }
    }

    public class UserRightsAgroFormsDTO
    {
        public long pFormid { set; get; }
        public string pFormname { set; get; }
        public string url { set; get; }
    }


}
