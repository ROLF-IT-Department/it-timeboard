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
/// Summary description for ProjectsDB
/// </summary>
public class ProjectDB
{
	private string ConnectionString;

    public ProjectDB()
    {
        this.ConnectionString = WebConfigurationManager.ConnectionStrings["IT_Timeboard"].ConnectionString;
    }

    // получаем список общедоступных активностей
    public List<Project> getALLProjects()
    {
        SqlConnection conn = new SqlConnection(this.ConnectionString);
        string sql = "SELECT * FROM it_timeboard_projects ORDER BY name ASC";
        SqlCommand cmd = new SqlCommand(sql, conn);
        List<Project> projects = new List<Project>();
        try
        {
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Project p = new Project((int)reader["id"], (string)reader["name"]);
                projects.Add(p);
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
        return projects;
    }

    // получаем список общедоступных активностей
    public List<Project> getCommonProjects()
    {
        SqlConnection conn = new SqlConnection(this.ConnectionString);
        string sql = "SELECT * FROM it_timeboard_projects WHERE common IS NOT NULL ORDER BY name DESC";
        SqlCommand cmd = new SqlCommand(sql, conn);
        List<Project> projects = new List<Project>();
        try
        {
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Project p = new Project((int)reader["id"], (string)reader["name"]);
                projects.Add(p);
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
        return projects;
    }

    
    public List<Project> getSpecialProjects(int person_id)
    {
        SqlConnection conn = new SqlConnection(this.ConnectionString);
        string sql = "SELECT * FROM it_timeboard_employee_project WHERE person_id = @person_id ORDER BY name ASC";
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add(new SqlParameter("@person_id", SqlDbType.Int, 4));
        cmd.Parameters["@person_id"].Value = person_id;
        List<Project> projects = new List<Project>();
        try
        {
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Project p = new Project((int)reader["project_id"], (string)reader["name"]);
                projects.Add(p);
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
        return projects;
    }

    public List<Project> getProjects(int person_id)
    {
        List<Project> common = this.getCommonProjects();
        List<Project> special = this.getSpecialProjects(person_id);

        foreach (Project p in common)
        {
            special.Insert(0, p);
        }

        return special;
    }

    public Project getProjectByID(int id)
    {
        SqlConnection conn = new SqlConnection(this.ConnectionString);
        string sql = "SELECT * FROM it_timeboard_projects WHERE id = @id";
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int, 4));
        cmd.Parameters["@id"].Value = id;
        Project project = null;
        try
        {
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                project = new Project((int)reader["id"], (string)reader["name"]);
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
        return project;
    }
}
