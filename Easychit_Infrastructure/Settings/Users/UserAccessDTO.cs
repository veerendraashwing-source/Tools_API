using System;
using System.Collections.Generic;
using System.Text;

namespace Easychit_Infrastructure.Settings.Users
{
    public class UserAccessDTO
    {
        public long pUserID { set; get; }
        public string pUserName { set; get; }
        public long pRoleid { set; get; }
        public string pPassword { set; get; }
        public string pToken { set; get; }
        public string pBranchschema { set; get; }
        public string pBranchname { set; get; }
        public object ipaddress { get; set; }
        public bool ispassword { set; get; }
        public bool isbiometric { set; get; }
        public string type { set; get; }
    }

    public class UserWiseBranches
    {
        public string pBranchid { set; get; }
        public string pBranchschema { set; get; }
        public string pBranchname { set; get; }
        public string region_name { get; set; }
    }
    public class UserRegistrationDTO
    {
        public long pUserID { set; get; }
        public string pEmployeeName { set; get; }
        public string pPassWord { set; get; }
        public string pContactRefID { set; get; }
        public string pConfirmPassWord { set; get; }
        public string pRandomNumber { set; get; }
    }
}
