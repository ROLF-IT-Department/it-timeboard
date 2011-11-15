using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Check : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string html = "";
        int month = 0;
        int year = 0;
        int person_id = 0;
        SQLDB sql = new SQLDB();
        Date dt = new Date();
        MonthDB mdb = new MonthDB();
        
        if (Request.QueryString["month"] != null)
            month = Convert.ToInt32(Request.QueryString["month"]);

        if (Request.QueryString["year"] != null)
            year = Convert.ToInt32(Request.QueryString["year"]);

        if (Request.QueryString["id"] != null)
            person_id = Convert.ToInt32(Request.QueryString["id"]);

        Employee emp = sql.getEmployee(person_id);

        lbEmployeeName.Text = emp.FIO;
        lbPeriodName.Text = mdb.getMonthName(month).ToUpper() + " " + year.ToString();

        string start_period = dt.getDataToSAP(1, month, year);

        List<CheckedHours> hours = sql.getCheckedSchedule(start_period, person_id);

        if (hours.Count > 0)
        {
            html = @"<table cellpadding='0' cellspacing='0' class='main_table' width='270px'>
                            <tr style='background: url(App_Resources/header.bmp) repeat-x;' >
                                <td class='header_table' width='70px' style='border-left: solid 1px #999999; border-top: solid 1px #999999;' >Дата</td>
                                <td class='header_table' width='100px' style='border-top: solid 1px #999999;'>IT</td>
                                <td class='header_table' width='100px' style='border-top: solid 1px #999999;'>SAP</td>
                            </tr>";

            foreach (CheckedHours ch in hours)
            {
                html += "<tr><td width='70px' style='border-left: solid 1px #999999;'>" + ch.DayDate + "</td><td  width='100px'>" + ch.IT_Hours.ToString() + "</td><td  width='100px'>" + ch.SAP_Hours.ToString() + "</td><tr>";
            }

            html += "</table>";
        }
        else
        {
            string msg = "";
            if (dt.isOpen(month, year))
                msg = "Введенный график совпадает с графиком SAP";
            else
                msg = "Нет данных";

            html = "<div class='check_personal' style='width: 270px; height:50px; color: DarkRed;'>" + msg + "</div>";
        }
        lbTable.Text = html;
    }
}
