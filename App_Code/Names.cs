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
public class Names
{
    private string id;   
    private string name;

    public Names(string id, string name)
	{
        this.id = id;
        this.name = name;
	}

    public string ID
    {
        get { return id; }
        set { id = value; }
    }

    public string NAME
    {
        get { return name; }
        set { name = value; }
    }


}
