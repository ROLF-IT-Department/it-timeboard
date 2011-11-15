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
/// Summary description for Date
/// </summary>
public class Date
{
	public Date()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    // формируем дату в формате SAP
    public string getDataToSAP(int day, int month, int year)
    {
        string sap_year = year.ToString();
        string sap_month = month.ToString();
        if (sap_month.Length == 1) sap_month = sap_month.Insert(0, "0");
        string sap_day = day.ToString();
        if (sap_day.Length == 1) sap_day = sap_day.Insert(0, "0");
        string sap_date = sap_year + sap_month + sap_day;
        return sap_date;
    }

    // определяем границу редактирования - прошлый текущий будущий месяц
    public bool isOpen(int month, int year)
    {
        int cur_month = DateTime.Now.Month;
        int cur_year = DateTime.Now.Year;

        // текущий месяц
        if (month == cur_month) return true;

        // прошлый месяц
        if (cur_month != 1)
        {
            if (month == cur_month - 1) return true;
        }
        else
        {
            if ((month == 12) && (year == cur_year - 1)) return true;
        }

        // будущий месяц
        if (cur_month != 12)
        {
            if (month == cur_month + 1) return true;
        }
        else
        {
            if ((month == 1) && (year == cur_year + 1)) return true;
        }

        return false;
    } 

}
