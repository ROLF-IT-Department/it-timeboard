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
/// Summary description for SQLDB
/// </summary>
public class SQLDB
{
    private string ConnectionString;


    public SQLDB()
    {
        this.ConnectionString = WebConfigurationManager.ConnectionStrings["IT_Timeboard"].ConnectionString;
    }

    public List<string> getTemplateDaysOfSchedule(string employee_id , string start_period)
    {
        List<string> days = new List<string>();

        SqlConnection conn = new SqlConnection(this.ConnectionString);

        string sql = "SELECT * FROM rolf_timeboard_schedules_sap WHERE (employee_id = @employee_id) AND (start_period = @start_period) AND (time_hours <> 0) ORDER BY day_period DESC";
        
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add(new SqlParameter("@employee_id", SqlDbType.VarChar, 50));
        cmd.Parameters["@employee_id"].Value = employee_id;
        cmd.Parameters.Add(new SqlParameter("@start_period", SqlDbType.VarChar, 50));
        cmd.Parameters["@start_period"].Value = start_period;

        try
        {
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string d = (string)reader["day_period"];
                days.Add(d);
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

        return days;
    }

    public bool isAccessAllowed(string netname)
    {
        bool is_access = false;

        SqlConnection conn = new SqlConnection(this.ConnectionString);

        string sql = "SELECT DISTINCT * FROM it_timeboard_allowed_netnames WHERE netname = @netname";

        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add(new SqlParameter("@netname", SqlDbType.VarChar, 255));
        cmd.Parameters["@netname"].Value = netname;


        try
        {
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                is_access = true;
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

        return is_access;
    }

    public AccessNetname getAccessNetname(string netname)
    {
        SqlConnection conn = new SqlConnection(this.ConnectionString);

        string sql = "SELECT DISTINCT * FROM it_timeboard_allowed_netnames WHERE netname = @netname";

        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add(new SqlParameter("@netname", SqlDbType.VarChar, 255));
        cmd.Parameters["@netname"].Value = netname;

        AccessNetname an = null;

        try
        {
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                an = new AccessNetname((int)reader["id"], (string)reader["netname"], (int)reader["rights"]);
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

        return an;
    }

    public List<CheckedHours> getCheckedSchedule(string start_period, int person_id)
    {
        SqlConnection conn = new SqlConnection(ConnectionString);
        string sql = "it_timeboard_check_timetable";
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@start_period", SqlDbType.VarChar, 8));
        cmd.Parameters["@start_period"].Value = start_period;
        cmd.Parameters.Add(new SqlParameter("@person_id", SqlDbType.Int, 4));
        cmd.Parameters["@person_id"].Value = person_id;
        List<CheckedHours> checked_hours = new List<CheckedHours>();
        try
        {
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                CheckedHours hours = new CheckedHours((int)reader["person_id"], (string)reader["day_date"], (decimal)reader["it_hours"], (decimal)reader["schedule_hours"]);
                checked_hours.Add(hours);
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

        return checked_hours;
    
    }

    public Employee getEmployee(int person_id)
    {

        SqlConnection conn = new SqlConnection(ConnectionString);
        string sql = "SELECT * FROM it_timeboard_persons WHERE (person_id = @person_id)";
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add(new SqlParameter("@person_id", SqlDbType.Int, 4));
        cmd.Parameters["@person_id"].Value = person_id;

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
