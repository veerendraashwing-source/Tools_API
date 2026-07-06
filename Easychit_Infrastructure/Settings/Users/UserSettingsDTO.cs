using System;
using System.Collections.Generic;
using System.Text;

namespace Easychit_Infrastructure.Settings.Users
{

    public class ParentModulesAGRODTO
    {
        public long pParentModuleId { get; set; }
        public string pParentModulename { get; set; }
    }

    public class SubmodulesAGRODTO : ParentModulesAGRODTO
    {
        public long pSubmoduleId { get; set; }
        public string pSubmodulename { get; set; }
    }

    public class FormsAGRODTO : SubmodulesAGRODTO
    {
        public string pFormname { get; set; }
        public string pFormurl { get; set; }
        public long pUserId { get; set; }
    }


    public class UsersDTO
    {
        public long userid { get; set; }
        public string username { get; set; }
    }

    public class UserRightsAGRODTO
    {
        public long user_id { get; set; }
        public long submoduleid { get; set; }
        public List<formaccesscheckAGRODto> _formaccesscheckDto { get; set; }
    }

    public class formaccesscheckAGRODto
    {
        public long formid { get; set; }
        public string formname { get; set; }
        public string chrnewright { get; set; }
    }

    public class UsersAGRODTO
    {
        public long userid { get; set; }
        public string username { get; set; }
        public string password { get; set; }


    }

}
