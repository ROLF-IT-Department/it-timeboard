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
using System.IO;
using System.Security.Principal;
using System.Data.Common;
using System.Data.SqlClient;


public partial class Report : System.Web.UI.Page
{
	private Employee employee;

    protected void Page_Load(object sender, EventArgs e)
    {
		string netname = "";
		string user_id = "";
        string user = User.Identity.Name.ToUpper();
        if (user.Contains("\\"))
            netname = user.Substring(user.IndexOf('\\') + 1, user.Length - user.IndexOf('\\') - 1);
		Auth auth = new Auth();
		employee = auth.Authentication(netname, "", "");
		
		if (employee != null)
        {
			user_id = employee.PersonID.ToString();
			
		}
		
        string ConnectionString = WebConfigurationManager.ConnectionStrings["IT_Timeboard"].ConnectionString;

        Response.Clear();
        Response.Charset = "UTF-8";
        Response.ContentType = "application/vnd.ms-excel";

        int month = DateTime.Now.Month;
        int year = DateTime.Now.Year;

		if (Request.QueryString["month"] != null)
            month = Convert.ToInt32(Request.QueryString["month"]);

        if (Request.QueryString["year"] != null)
            year = Convert.ToInt32(Request.QueryString["year"]);

        string str = @"<table cellspacing='0' cellpadding='0' border='1'>
                            <tr>
                                <td style='background-color:#BBBBBB'>
                                    Дата
                                </td>
                                <td style='background-color:#BBBBBB'>
                                    Отдел
                                </td>
                                <td style='background-color:#BBBBBB'>
                                    Должность
                                </td>
                                <td style='background-color:#BBBBBB'>
                                    ФИО
                                </td>
                                <td style='background-color:#BBBBBB'>
                                    Проекты
                                </td>
                                <td style='background-color:#BBBBBB'>
                                    Группы проектов
                                </td>
                                <td style='background-color:#BBBBBB'>
                                    Часы
                                </td>
                                <td style='background-color:#BBBBBB'>
                                    Полный путь
                                </td>
</tr>
                    ";

        SqlConnection conn = new SqlConnection(ConnectionString);
        string sql = "SELECT * FROM it_timeboard_report WHERE (MONTH(day_date)=@month) AND (YEAR(day_date)=@year)";

		if(user_id != "")
			sql += " AND (person_id=@user_id)";
			
		sql += "ORDER BY fio, day_date";
        
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add(new SqlParameter("@user_id", SqlDbType.Int, 6));
        cmd.Parameters["@user_id"].Value = user_id;
		cmd.Parameters.Add(new SqlParameter("@month", SqlDbType.Int, 4));
        cmd.Parameters["@month"].Value = month;
        cmd.Parameters.Add(new SqlParameter("@year", SqlDbType.Int, 4));
        cmd.Parameters["@year"].Value = year;
        try
        {
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                str += "<tr><td style='height: 30px;'>" + ((DateTime)reader["day_date"]).ToString("dd.MM.yyyy") + "</td><td style='height: 30px;'>" + (string)reader["department"] + "</td><td style='height: 30px;'>" + (string)reader["post"] + "</td><td style='height: 30px;'>" + (string)reader["fio"] + "</td><td style='height: 30px;'>" + (string)reader["project_name"] + "</td><td style='height: 30px;'>" + (string)reader["groups"] + "</td><td style='height: 30px;'>" + (decimal)reader["hours"] + "</td><td style='height: 30px;'>" + (string)reader["full_path"] + "</td></tr>";
            }
            reader.Close();


        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            conn.Close();
        }



        Response.Write(str);
        Response.End();

    }
}
