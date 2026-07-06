using HelperManager;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Easychit_Infrastructure;
using System.Threading.Tasks;

using Easychit_Infrastructure.Easy_Chit_Tools;

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

        //public bool SaveNameChange(string globalSchema, string conn, NameChangeSaveDTO dto)
        //{
        //    if (dto == null || dto.Contact_Id <= 0)
        //        throw new ArgumentException("Invalid DTO");

        //    // Build SQL
        //    string sql = $@"
        //        UPDATE {globalSchema}.tbl_mst_contact
        //        SET 
        //            contact_name = @NewName,
        //            contact_surname = @NewSurname,
        //            contact_mailing_name = @NewMailingName
        //        WHERE tbl_mst_contact_id = @ContactId";

        //    using var connection = new NpgsqlConnection(conn);
        //    connection.Open();

        //    using var cmd = new NpgsqlCommand(sql, connection);
        //    cmd.Parameters.AddWithValue("@NewName", dto.NewName ?? (object)DBNull.Value);
        //    cmd.Parameters.AddWithValue("@NewSurname", dto.NewSurname ?? (object)DBNull.Value);
        //    cmd.Parameters.AddWithValue("@NewMailingName", dto.NewMailingName ?? (object)DBNull.Value);
        //    cmd.Parameters.AddWithValue("@ContactId", dto.Contact_Id);

        //    int rowsAffected = cmd.ExecuteNonQuery();

        //    return rowsAffected > 0;
        //}
        public static object AddDoubleQuotes(object value)
        {
            return "\"" + value + "\"";
        }

    }
}
