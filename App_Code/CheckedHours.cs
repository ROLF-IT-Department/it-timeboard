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
/// Summary description for CheckedHours
/// </summary>
public class CheckedHours
{
    private int person_id;
    private string day_date;
    private decimal it_hours;
    private decimal sap_hours;

    public CheckedHours(int person_id, string day_date, decimal it_hours, decimal sap_hours)
	{
        this.person_id = person_id;
        this.day_date = day_date;
        this.it_hours = it_hours;
        this.sap_hours = sap_hours;
	}

    public int PersonID
    {
        get { return person_id; }
        set { person_id = value; }
    }

    public string DayDate
    {
        get { return day_date; }
        set { day_date = value; }
    }

    public decimal IT_Hours
    {
        get { return it_hours; }
        set { it_hours = value; }
    }

    public decimal SAP_Hours
    {
        get { return sap_hours; }
        set { sap_hours = value; }
    }

}
