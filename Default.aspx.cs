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
using System.IO;
using System.Security.Principal;

public partial class _Default : System.Web.UI.Page
{
    private Employee employee;
    private int month;
    private int year;
    public int count;

    protected void Page_Load(object sender, EventArgs e)
    {
        lbTable.Text = @"<table cellpadding='0' cellspacing='0' class='main_table' width='760px'>
                        <tbody id='times'>
                        <tr style='background: url(App_Resources/header.bmp) repeat-x;' >
                            <td class='header_table' width='30px' style='border-left: solid 1px #999999; border-top: solid 1px #999999;' >№</td>
                            <td class='header_table' width='120px' style='border-top: solid 1px #999999;'><img alt='По возрастанию' src='App_Resources/sort_up.bmp' onclick='SortRows(this)' style='cursor: hand; position:absolute; padding-top: 1px;'>&nbsp;&nbsp;&nbsp;&nbsp;<span>Дата</span></td>
                            <td class='header_table' width='500px' style='border-top: solid 1px #999999;'>Проект</td>
                            <td class='header_table' width='50px'  style='border-top: solid 1px #999999;'>Часы</td>
                            <td class='header_table' width='30px'  style='border-top: solid 1px #999999;'>&nbsp;</td>
                            <td class='header_table' width='30px'  style='border-top: solid 1px #999999;'>&nbsp;</td>
                        </tr>";

        string netname = "";
        string windows_netname = "";
        string user = User.Identity.Name.ToUpper();
        if (user.Contains("\\"))
            netname = user.Substring(user.IndexOf('\\') + 1, user.Length - user.IndexOf('\\') - 1);
        windows_netname = netname;
        Auth auth = new Auth();
        Date dt = new Date();
        ProjectDB prDB = new ProjectDB();
        SQLDB sql = new SQLDB();


        if (Request.QueryString["netname"] != null)
        {
            netname = Request.QueryString["netname"].ToString();
        }

        employee = auth.Authentication(netname, "", "");
        if (employee != null)
        {
            lbTab.Text = employee.PersonID.ToString();
            lbEmployee.Text = employee.FIO;
            lbEmployee.Text += " <span onclick='onCheckEmployees()' class='check_employees' >войти под другим пользователем</span>";
            lbPost.Text = employee.Post;
            lbDepartment.Text = employee.FullPath;
            lbCompany.Text = employee.Company;

            month = DateTime.Now.Month;
            year = DateTime.Now.Year;

            if (Request.QueryString["month"] != null)
                month = Convert.ToInt32(Request.QueryString["month"]);

            if (Request.QueryString["year"] != null)
                year = Convert.ToInt32(Request.QueryString["year"]);

            lbYear.Text = fillYears(year);
            lbMonth.Text = fillMonths(month);
            lbAdd.Text = "<img alt='' src='App_Resources/button_add.bmp' onclick='AddRow(\"times\");' style='cursor: hand;' />";
            lbCheck.Text = "<img alt='' src='App_Resources/button_check.bmp' onclick='OpenCheck();' style='cursor: hand;' />";
            lbHours.Text = "<input type='text' id='tbHours' class='input_hours' maxlength='5' onblur='CheckHourType(this)' />";

            fillProjects();


            List<string> template_days = sql.getTemplateDaysOfSchedule(ConvertToSAP(employee.PersonID), dt.getDataToSAP(1, month, year));
            string days = "";
            foreach (string d in template_days)
            {
                days = days + d + "|";
            }
            if (days != "") days = days.Remove(days.Length - 1);


            string is_access = "0";
            if (sql.isAccessAllowed(windows_netname)) is_access = "1";

            lbTempDays.Text = "<input id='template_days' type='hidden' value='" + days + "'  />";
            lbTempDays.Text += "<input id='is_access' type='hidden' value='" + is_access + "'  />";
            lbTempDays.Text += "<input id='user_id' type='hidden' value='" + employee.PersonID.ToString() + "'  />";
            lbTempDays.Text += "<input id='netname' type='hidden' value='" + netname + "'  />";

            HoursDB db = new HoursDB();
            List<Hours> hours = db.getHours(employee.PersonID, month, year);

            AccessNetname acnet = null;

            count = 0;

            if (!windows_netname.Equals(netname))
            {
                acnet = sql.getAccessNetname(windows_netname);
            }

            if (windows_netname.Equals(netname))
            {
                if (dt.isOpen(month, year))
                {
                    foreach (Hours h in hours)
                    {
                        count++;
                        lbTable.Text += "<tr><td width='30px' style='border-left:solid 1px #999999'>" + count.ToString() + "</td><td width='120px'><input type='text' readonly value='" + h.DayDate.ToString("dd.MM.yyyy") + "'  name='" + h.ID + "||Date' id='" + h.ID + "||Date' class='input_date' onfocus='ShowCalendar(this);' onmouseover = 'ShowToolTip(this, \"hours\", event.clientX + document.documentElement.scrollLeft - 150, event.clientY + document.documentElement.scrollTop);' onmouseout = 'ShowBlock(\"hours\");' ></td><td width='500px'>" + fillProjectsDropDownList(h.ID, h.ProjectID) + "</td><td width='50px'><input type='text' maxlength='5' value='" + CheckDecimalNumber(h.TimeHours.ToString()) + "'  name='" + h.ID + "||Hours' id='" + h.ID + "||Hours' class='input_hours' onblur='CheckHourType(this)' onmouseover = 'ShowToolTip(this, \"hours\", event.clientX + document.documentElement.scrollLeft, event.clientY + + document.documentElement.scrollTop);' onmouseout = 'ShowBlock(\"hours\");'></td><td width='30px'><img src='App_Resources/add.gif' style='cursor: hand' onclick='CopyRow(this);'></td><td width='30px'><img src='App_Resources/delete.gif' style='cursor: hand' onclick='DeleteRow(this);'></td></tr>";

                    }
                    lbTemplate.Text = "<span style='cursor: hand; cursor: pointer;' onclick='ShowBlock(\"template\");'>Шаблон</span> <img id='square' src='App_Resources/plus.bmp' alt='Развернуть' style='cursor: hand; cursor: pointer;' onclick='ShowBlock(\"template\");'>";
                }
                else
                {
                    lbAdd.Text = "&nbsp;";
                    lbCheck.Text = "&nbsp;";
                    btSave.Visible = false;
                    foreach (Hours h in hours)
                    {
                        count++;
                        Project project = prDB.getProjectByID(h.ProjectID);
                        lbTable.Text += "<tr><td width='30px' style='border-left:solid 1px #999999'>" + count.ToString() + "</td><td width='150px'>" + h.DayDate.ToString("dd.MM.yyyy") + "</td><td width='500px'>" + project.NAME + "</td><td width='50px'>" + CheckDecimalNumber(h.TimeHours.ToString()) + "</td><td width='30px'>&nbsp;</td><td width='30px'>&nbsp;</td></tr>";
                    }
                }
            }
            else
            {
                if ((dt.isOpen(month, year)) && (acnet.Rights == 1))
                {
                    foreach (Hours h in hours)
                    {
                        count++;
                        lbTable.Text += "<tr><td width='30px' style='border-left:solid 1px #999999'>" + count.ToString() + "</td><td width='120px'><input type='text' readonly value='" + h.DayDate.ToString("dd.MM.yyyy") + "'  name='" + h.ID + "||Date' id='" + h.ID + "||Date' class='input_date' onfocus='ShowCalendar(this);' onmouseover = 'ShowToolTip(this, \"hours\", event.clientX + document.documentElement.scrollLeft - 150, event.clientY + document.documentElement.scrollTop);' onmouseout = 'ShowBlock(\"hours\");' ></td><td width='500px'>" + fillProjectsDropDownList(h.ID, h.ProjectID) + "</td><td width='50px'><input type='text' maxlength='5' value='" + CheckDecimalNumber(h.TimeHours.ToString()) + "'  name='" + h.ID + "||Hours' id='" + h.ID + "||Hours' class='input_hours' onblur='CheckHourType(this)' onmouseover = 'ShowToolTip(this, \"hours\", event.clientX + document.documentElement.scrollLeft, event.clientY + + document.documentElement.scrollTop);' onmouseout = 'ShowBlock(\"hours\");'></td><td width='30px'><img src='App_Resources/add.gif' style='cursor: hand' onclick='CopyRow(this);'></td><td width='30px'><img src='App_Resources/delete.gif' style='cursor: hand' onclick='DeleteRow(this);'></td></tr>";

                    }
                    lbTemplate.Text = "<span style='cursor: hand; cursor: pointer;' onclick='ShowBlock(\"template\");'>Шаблон</span> <img id='square' src='App_Resources/plus.bmp' alt='Развернуть' style='cursor: hand; cursor: pointer;' onclick='ShowBlock(\"template\");'>";
                }
                else
                {
                    lbAdd.Text = "&nbsp;";
                    btSave.Visible = false;
                    foreach (Hours h in hours)
                    {
                        count++;
                        Project project = prDB.getProjectByID(h.ProjectID);
                        lbTable.Text += "<tr><td width='30px' style='border-left:solid 1px #999999'>" + count.ToString() + "</td><td width='150px'>" + h.DayDate.ToString("dd.MM.yyyy") + "</td><td width='500px'>" + project.NAME + "</td><td width='50px'>" + CheckDecimalNumber(h.TimeHours.ToString()) + "</td><td width='30px'>&nbsp;</td><td width='30px'>&nbsp;</td></tr>";
                    }
                }
            }
            lbTable.Text += "</tbody></table>";

        }
        else
        {
            MessageBox.Show("Сотрудник не найден!");

            lbTable.Text += "</tbody></table>";
        }

    }

    public string ConvertToSAP(int employee_id)
    {
        string emp_id = employee_id.ToString();

        while (emp_id.Length < 8)
        {
            emp_id = emp_id.Insert(0, "0");
        }

        return emp_id;
    }

    // функция для обработки десятичного числа в формате decimal(5,2)
    public string CheckDecimalNumber(string decNumber)
    {
        if (decNumber.Contains(","))
        {
            string integer = decNumber.Substring(0, decNumber.IndexOf(','));     // копируем целую часть числа
            string real = decNumber.Substring(decNumber.IndexOf(',') + 1);      // копируем НЕцелую часть числа

            string result = "";

            if ((real[0] == '0') && (real[1] == '0')) return integer;

            if (real[1] == '0')
            {
                result = integer + ',' + real[0];
                return result;
            }

        }

        return decNumber;

    }

    private string fillProjectsDropDownList(int record_id, int project_id)
    {
        string html = "<select name='" + record_id + "||" + "Project' class='select_project'>";

        ProjectDB pdb = new ProjectDB();

        List<Project> projects = new List<Project>();

        projects = pdb.getProjects(this.employee.PersonID);

        string selected = "";

        foreach (Project project in projects)
        {
            if (project.ID == project_id) selected = "selected";
            html += "<option value='" + project.ID + "' " + selected + ">" + project.NAME + "</option>";
            selected = "";
        }

        html += "</select>";

        return html;
    }

    // заполняем месяцы
    private string fillMonths(int selected_month)
    {
        string html = "<select name='ddlMonth' style='width:100px' onchange='onChangeMonth()'>";

        Date dt = new Date();
        MonthDB mdb = new MonthDB();
        List<Month> months = mdb.getMonths();

        string selected = "";

        foreach (Month month in months)
        {
            if (month.MonthID == selected_month) selected = "selected";
            html += "<option value='" + month.MonthID + "' " + selected + ">" + month.MonthName.ToUpper() + "</option>";
            selected = "";
        }

        return html += "</select>";
    }

    // заполняем год
    private string fillYears(int selected_year)
    {
        string html = "<select name='ddlYear' onchange='onChangeYear()'>";

        string selected = "";

        for (int i = 2009; i < 2012; i++)
        {
            if (i == selected_year) selected = "selected";
            html += "<option value='" + i + "' " + selected + ">" + i + "</option>";
            selected = "";
        }

        return html += "</select>";
    }


    private void fillProjects()
    {
        if (ddlProject.Items.Count != 0) return;

        ProjectDB pdb = new ProjectDB();
        List<Project> projects = new List<Project>();

        projects = pdb.getProjects(this.employee.PersonID);

        foreach (Project project in projects)
        {
            ListItem li = new ListItem(project.NAME, project.ID.ToString());
            ddlProject.Items.Add(li);
        }
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        List<Hours> hours = new List<Hours>();
        HoursDB db = new HoursDB();

        db.deleteHours(employee.PersonID, month, year);

        for (int i = 0; i < Request.Form.Count; i++)
        {


            while (Request.Form.AllKeys[i].Contains("||"))
            {

                string new_date = "";
                string new_project = "";
                string new_hours = "";

                string name = Request.Form.AllKeys[i];
                string id = Request.Form.AllKeys[i].Substring(0, Request.Form.AllKeys[i].IndexOf("||"));


                while (id.Equals(Request.Form.AllKeys[i + 1].Substring(0, Request.Form.AllKeys[i + 1].IndexOf("||"))))
                {
                    if (Request.Form.AllKeys[i].Contains("Date")) new_date = Request.Form[Request.Form.AllKeys[i]];
                    if (Request.Form.AllKeys[i].Contains("Project")) new_project = Request.Form[Request.Form.AllKeys[i]];
                    if (Request.Form.AllKeys[i].Contains("Hours")) new_hours = Request.Form[Request.Form.AllKeys[i]];

                    i++;

                    if ((!Request.Form.AllKeys[i + 1].Contains("||")) || (!id.Equals(Request.Form.AllKeys[i + 1].Substring(0, Request.Form.AllKeys[i + 1].IndexOf("||")))))
                    {
                        if (Request.Form.AllKeys[i].Contains("Date")) new_date = Request.Form[Request.Form.AllKeys[i]];
                        if (Request.Form.AllKeys[i].Contains("Project")) new_project = Request.Form[Request.Form.AllKeys[i]];
                        if (Request.Form.AllKeys[i].Contains("Hours")) new_hours = Request.Form[Request.Form.AllKeys[i]];

                        break;
                    }

                }

                i++;

                if ((new_date != "") && (new_hours != ""))
					db.insertHours(employee.PersonID, DateTime.Parse(new_date), Convert.ToDecimal(new_hours), Convert.ToInt32(new_project), employee.PostID, employee.Post, employee.DepartmentID, employee.Department, employee.CompanyID, employee.Company, employee.FullPath);

                //Response.Write( "Date = " + new_date + "; " + "Project = " + new_project + "; " + "Hours = " + new_hours + "; </br>");

            }



        }

        string netname = "";

        if (Request.QueryString["netname"] != null)
        {
            netname = Request.QueryString["netname"].ToString();
            Response.Redirect("Default.aspx?month=" + month.ToString() + "&year=" + year.ToString() + "&netname=" + netname);
        }

        Response.Redirect("Default.aspx?month=" + month.ToString() + "&year=" + year.ToString());

    }
}
