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
/// Summary description for HoursDB
/// </summary>
public class HoursDB
{
    private string ConnectionString;

    public HoursDB()
    {
        this.ConnectionString = WebConfigurationManager.ConnectionStrings["IT_Timeboard"].ConnectionString;
    }


    public List<Hours> getHours(int person_id, int month, int year)
    {
        SqlConnection conn = new SqlConnection(this.ConnectionString);
        string sql = "SELECT * FROM it_timeboard_hours WHERE (person_id = @person_id) AND (MONTH(day_date) = @month) AND (YEAR(day_date) = @year) ORDER BY DAY(day_date) DESC, project_id";
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add(new SqlParameter("@person_id", SqlDbType.Int, 4));
        cmd.Parameters["@person_id"].Value = person_id;
        cmd.Parameters.Add(new SqlParameter("@month", SqlDbType.Int, 4));
        cmd.Parameters["@month"].Value = month;
        cmd.Parameters.Add(new SqlParameter("@year", SqlDbType.Int, 4));
        cmd.Parameters["@year"].Value = year;
        List<Hours> hours = new List<Hours>();
        try
        {
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Hours h = new Hours((int)reader["id"], (int)reader["person_id"], (DateTime)reader["day_date"], (decimal)reader["hours"], (int)reader["project_id"], (string)reader["post_id"], (string)reader["department_id"], (string)reader["company_id"]);
                hours.Add(h);
            }
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
        return hours;
    }


    public void insertHours(int person_id, DateTime day_date, decimal hours, int project_id, string post_id, string post, string department_id, string department, string company_id, string company, string full_path)
    {

        SqlConnection conn = new SqlConnection(this.ConnectionString);
        string sql = "INSERT INTO it_timeboard_hours VALUES (@person_id, @day_date, @hours, @project_id, @post_id, @post, @department_id, @department, @company_id, @company, @full_path, @date_record)";
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add(new SqlParameter("@person_id", SqlDbType.Int, 4));
        cmd.Parameters["@person_id"].Value = person_id;
        cmd.Parameters.Add(new SqlParameter("@day_date", SqlDbType.SmallDateTime, 4));
        cmd.Parameters["@day_date"].Value = day_date;
        cmd.Parameters.Add(new SqlParameter("@hours", SqlDbType.Decimal, 5));
        cmd.Parameters["@hours"].Value = hours;
        cmd.Parameters.Add(new SqlParameter("@project_id", SqlDbType.Int, 4));
        cmd.Parameters["@project_id"].Value = project_id;
        cmd.Parameters.Add(new SqlParameter("@post_id", SqlDbType.VarChar, 50));
        cmd.Parameters["@post_id"].Value = post_id;
        cmd.Parameters.Add(new SqlParameter("@post", SqlDbType.VarChar, 255));
        cmd.Parameters["@post"].Value = post;
        cmd.Parameters.Add(new SqlParameter("@department_id", SqlDbType.VarChar, 50));
        cmd.Parameters["@department_id"].Value = department_id;
        cmd.Parameters.Add(new SqlParameter("@department", SqlDbType.VarChar, 255));
        cmd.Parameters["@department"].Value = department;
        cmd.Parameters.Add(new SqlParameter("@company_id", SqlDbType.VarChar, 50));
        cmd.Parameters["@company_id"].Value = company_id;
        cmd.Parameters.Add(new SqlParameter("@company", SqlDbType.VarChar, 255));
        cmd.Parameters["@company"].Value = company;
        cmd.Parameters.Add(new SqlParameter("@full_path", SqlDbType.VarChar, 1024));
        cmd.Parameters["@full_path"].Value = full_path;
        cmd.Parameters.Add(new SqlParameter("@date_record", SqlDbType.DateTime, 8));
        cmd.Parameters["@date_record"].Value = DateTime.Now;
        try
        {
            conn.Open();
            cmd.ExecuteNonQuery();
        }
        catch(Exception e)
        {
            //throw new Exception(e.Message);
        }
        finally
        {
            conn.Close();
        }

    }
    
    public void deleteHours(int person_id, int month, int year)
    {
        SqlConnection conn = new SqlConnection(this.ConnectionString);
        string sql = "DELETE FROM it_timeboard_hours WHERE (person_id = @person_id) AND (MONTH(day_date) = @month) AND (YEAR(day_date) = @year) ";
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add(new SqlParameter("@person_id", SqlDbType.Int, 4));
        cmd.Parameters["@person_id"].Value = person_id;
        cmd.Parameters.Add(new SqlParameter("@month", SqlDbType.Int, 4));
        cmd.Parameters["@month"].Value = month;
        cmd.Parameters.Add(new SqlParameter("@year", SqlDbType.Int, 4));
        cmd.Parameters["@year"].Value = year;
        try
        {
            conn.Open();
            cmd.ExecuteNonQuery();
        }
        catch(Exception e)
        {
            throw new Exception(e.Message);
        }
        finally
        {
            conn.Close();
        }
    }
     

}
