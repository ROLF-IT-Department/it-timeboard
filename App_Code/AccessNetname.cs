using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// ����� ��� ������
/// </summary>
public class AccessNetname
{
    private int id;
    private string netname;      
    private int rights;     // 0 - ����� ������ �� ������ , 1 - ����� �� ������ � ������

    public AccessNetname(int id, string netname, int rights)
    {
        this.id = id;
        this.netname = netname;
        this.rights = rights;
    }

    public int ID
    {
        get { return id; }
        set { id = value; }
    }

    public string Netname
    {
        get { return netname; }
        set { netname = value; }
    }

    public int Rights
    {
        get { return rights; }
        set { rights = value; }
    }
}
