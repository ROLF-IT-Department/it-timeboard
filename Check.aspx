<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Check.aspx.cs" Inherits="Check" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Проверка рабочего графика с SAP</title>
    <link type="text/css" href="CSS/main.css" rel="Stylesheet" />
</head>
<body>
<center>
    <form id="form1" runat="server">
    <table cellpadding='0' cellspacing='0' border='0' width='270px'>
        <tr>
            <td width="80px" style="height:40px;" align="left"><span class="check_personal">Сотрудник:</span></td>
            <td style="height:40px;"><span class="check_personal"><asp:Label ID="lbEmployeeName" runat="server" Text=""></asp:Label></span></td>
        </tr>
        <tr>
            <td width="80px" style="height:40px;" align="left"><span class="check_personal">Период:</span></td>
            <td style="height:40px;"><span class="check_personal"><asp:Label ID="lbPeriodName" runat="server" Text=""></asp:Label></span></td>
        </tr>
    </table>
    <!--<div class="check_personal" style="height:40px; color: DarkRed;">Результаты проверки</div>-->
    <div>
    <asp:Label ID="lbTable" runat="server" Text=""></asp:Label>
        
    </div>
    </form>
</center>
</body>
</html>
