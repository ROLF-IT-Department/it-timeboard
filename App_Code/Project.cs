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
/// Summary description for Projects
/// </summary>
public class Project
{
    private int id;   
    private string name;     
    private int category_id;

	public Project(int id, string name)
	{
        this.id = id;
        this.name = name;
        //this.category_id = category_id;
	}

    public int ID
    {
        get { return id; }
        set { id = value; }
    }

    public string NAME
    {
        get { return name; }
        set { name = value; }
    }

    public int CategoryID
    {
        get { return category_id; }
        set { category_id = value; }
    }

}
