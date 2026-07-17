using HelperManager;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Easychit_Infrastructure;
using System.Threading.Tasks;


namespace Easychit_Repository.DataAccess
{
    public class CommonDAL 
    {
        NpgsqlConnection con = new NpgsqlConnection(NPGSqlHelper.SQLConnString);
        //NpgsqlTransaction trans;

        public static string FormatDate(object strFormateDate)
        {
            string strDate = Convert.ToString(strFormateDate);
            string Date = null;
            if (!string.IsNullOrEmpty(strDate))
            {
                //strDate = Convert.ToDateTime(strDate).ToString("dd-MM-yyyy");
                strDate = strDate.Substring(0, 10);

                string[] dat = null;
                if (strDate != null)
                {
                    if (strDate.Contains("/"))
                    {
                        dat = strDate.Split('/');
                    }
                    else if (strDate.Contains("-"))
                    {
                        dat = strDate.Split('-');
                    }
                    Date = dat[2] + "-" + dat[1] + "-" + dat[0];
                }
            }
            return Date;
        }
        protected string ManageQuote(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                str = str.Replace("'", "''");
            }
            else if (string.IsNullOrEmpty(str))
            {
                str = string.Empty;
            }
            return str;
        }

        public static object AddDoubleQuotes(object value)
        {
            return "\"" + value + "\"";
        }

    }
}
