using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for Hours
/// </summary>
public class Hours
{
    private int id;
    private int person_id;
    private DateTime day_date;
    private decimal hours;
    private int project_id;
    private string post_id;
    private string department_id;
    private string company_id;

	public Hours(int id, int person_id, DateTime day_date, decimal hours, int project_id, string post_id, string department_id, string company_id)
	{
        this.id = id;
        this.person_id = person_id;
        this.day_date = day_date;
        this.hours = hours;
        this.project_id = project_id;
        this.post_id = post_id;
        this.department_id = department_id;
        this.company_id = company_id;
	}

    public int ID
    {
        get { return id; }
        set { id = value; }
    }

    public int PersonID
    {
        get { return person_id; }
        set { person_id = value; }
    }

    public DateTime DayDate
    {
        get { return day_date; }
        set { day_date = value; }
    }

    public decimal TimeHours
    {
        get { return hours; }
        set { hours = value; }
    }

    public int ProjectID
    {
        get { return project_id; }
        set { project_id = value; }
    }

    public string PostID
    {
        get { return post_id; }
        set { post_id = value; }
    }

    public string DepartmentID
    {
        get { return department_id; }
        set { department_id = value; }
    }

    public string CompanyID
    {
        get { return company_id; }
        set { company_id = value; }
    }

}
