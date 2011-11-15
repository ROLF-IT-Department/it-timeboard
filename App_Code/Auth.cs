using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Common;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Auth
/// </summary>
public class Auth
{
    private string ConnectionString;

	public Auth()
	{
		this.ConnectionString = WebConfigurationManager.ConnectionStrings["IT_Timeboard"].ConnectionString;
	}

    public Employee Authentication(string netname, string login, string password)
    {
        //netname = "IVOhrimenko";
        
        SqlConnection conn = new SqlConnection(ConnectionString);
        string sql = "SELECT * FROM it_timeboard_persons WHERE (netname = @net)";
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add(new SqlParameter("@net", SqlDbType.VarChar, 50));
        cmd.Parameters["@net"].Value = netname;
        
        Employee emp = null;

        try
        {
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
                emp = new Employee((int)reader["person_id"], (string)reader["fio"], (string)reader["netname"], (string)reader["login"], (string)reader["password"], (string)reader["post_id"], (string)reader["post"], (string)reader["department_id"], (string)reader["department"], (string)reader["company_id"], (string)reader["company"], (string)reader["fullpath"], (string)reader["out_date"]); 

            reader.Close();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        finally
        {
            conn.Close();
        }

        return emp;

    }
}
