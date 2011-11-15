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
/// Summary description for Employee
/// </summary>
public class Employee
{
    private int person_id;
    private string fio;
    private string netname;
    private string login;
    private string password;
    private string post_id;
    private string post;
    private string department_id;
    private string department;
    private string company_id;
    private string company;
    private string fullpath;
    private string out_date;

    public Employee(int person_id, string fio, string netname, string login, string password, string post_id, string post, string department_id, string department, string company_id, string company, string fullpath, string out_date)
	{
        this.person_id = person_id;
        this.fio = fio;
        this.netname = netname;
        this.login = login;
        this.password = password;
        this.post_id = post_id;
        this.post = post;
        this.department_id = department_id;
        this.department = department;
        this.company_id = company_id;
        this.company = company;
        this.fullpath = fullpath;
        this.out_date = out_date;
	}

    public int PersonID
    {
        get { return person_id; }
        set { person_id = value; }
    }

    public string FIO
    {
        get { return fio; }
        set { fio = value; }
    }

    public string Netname
    {
        get { return netname; }
        set { netname = value; }
    }

    public string Login
    {
        get { return login; }
        set { login = value; }
    }

    public string Password
    {
        get { return password; }
        set { password = value; }
    }

    public string PostID
    {
        get { return post_id; }
        set { post_id = value; }
    }

    public string Post
    {
        get { return post; }
        set { post = value; }
    }

    public string DepartmentID
    {
        get { return department_id; }
        set { department_id = value; }
    }

    public string Department
    {
        get { return department; }
        set { department = value; }
    }

    public string OutDate
    {
        get { return out_date; }
        set { out_date = value; }
    }

    public string CompanyID
    {
        get { return company_id; }
        set { company_id = value; }
    }

    public string Company
    {
        get { return company; }
        set { company = value; }
    }

    public string FullPath
    {
        get { return fullpath; }
        set { fullpath = value; }
    }
}
