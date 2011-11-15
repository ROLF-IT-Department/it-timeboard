using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class CheckEmployees : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void submit_Click(object sender, EventArgs e)
    {
        Auth auth = new Auth();
        SQLDB db = new SQLDB();

        string netname_user = "";
        string user = User.Identity.Name.ToUpper();
        if (user.Contains("\\"))
            netname_user = user.Substring(user.IndexOf('\\') + 1, user.Length - user.IndexOf('\\') - 1);
        if (db.isAccessAllowed(netname_user))
        {
            string netname = Netname.Text.ToUpper();
            string login = Login.Text.ToUpper();
            string password = Password.Text;
            if (netname.Length < 3)
            {
                MessageBox.Show("Сотрудник не найден!");
                return;
            }
            Employee employee = auth.Authentication(netname, login, password);
            if (employee != null)
                Response.Redirect("Default.aspx?netname=" + employee.Netname);
            else
                MessageBox.Show("Сотрудник не найден!");
        }
        else
        {
            MessageBox.Show("У вас недостаточно прав доступа для входа!");
        }

    }


}
