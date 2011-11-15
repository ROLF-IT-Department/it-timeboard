<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>IT табель</title>
    <link type="text/css" href="CSS/main.css" rel="Stylesheet" />
    <script type="text/javascript" src="JS/main.js"></script>
    <script type="text/javascript" src="JS/calendar/src/js/jscal2.js"></script>
    <script type="text/javascript" src="JS/calendar/src/js/lang/ru.js"></script>
    <link type="text/css" href="JS/calendar/src/css/border-radius.css" rel="Stylesheet" />
    <link type="text/css" href="JS/calendar/src/css/jscal2.css" rel="Stylesheet" />
    <link type="text/css" href="JS/calendar/src/css/reduce-spacing.css" rel="Stylesheet" />
</head>
<body>
    <script>
        // счетчик записей
        var countRow = "<% =count %>";
    </script>
    <form id="form1" runat="server" method="post">
    
    <asp:Label ID="lbTempDays" runat="server" Text=""></asp:Label>
        
    <div id="hours" class="hours"  style="display: none;"></div>
    
    <div id="header">

    <table cellpadding="0" cellspacing="0" border="0" width="1050px" class="person_info">
        <tr>
            <td rowspan="7" width="150px" align="center"><img src="App_Resources/profile.bmp" alt="" /></td>
            <td align="left" width="200px"><span class="personal">Табельный номер: &nbsp</span></td>
            <td align="left" width="700px"><span class="personal_info"><asp:Label ID="lbTab" runat="server" Text=""></asp:Label></span></td>
           
        </tr>
        <tr>
            <td align="left" width="200px"><span class="personal">Сотрудник: &nbsp</span></td>
            <td align="left" width="700px"><span class="personal_info"><asp:Label ID="lbEmployee" runat="server" Text=""></asp:Label></span></td>
            
        </tr>
        <tr>
            <td align="left" width="200px"><span class="personal">Должность: &nbsp</span></td>
            <td align="left" width="700px"><span class="personal_info"><asp:Label ID="lbPost" runat="server" Text=""></asp:Label></span></td>
        </tr>
        <tr>
            <td align="left" width="200px"><span class="personal">Отдел: &nbsp</span></td>
            <td align="left" width="700px"><span class="personal_info"><asp:Label ID="lbDepartment" runat="server" Text=""></asp:Label></span></td>
        </tr>
        <tr>
            <td align="left" width="200px"><span class="personal">Компания: &nbsp</span></td>
            <td align="left" width="700px"><span class="personal_info"><asp:Label ID="lbCompany" runat="server" Text=""></asp:Label></span></td>
        </tr>
        <tr>
            <td align="left" width="200px"><span class="personal">Год: &nbsp</span></td>
            <td align="left" width="700px"><span class="personal_info"><asp:Label ID="lbYear" runat="server" Text=""></asp:Label></span></td>
        </tr>
        <tr>
            <td align="left" width="200px"><span class="personal">Месяц: &nbsp</span></td>
            <td align="left" width="700px"><span class="personal_info"><asp:Label ID="lbMonth" runat="server" Text=""></asp:Label></span></td>
        </tr>
        <tr>
            <td width="150px" align="center">&nbsp;</td>
            <td align="left" colspan="2" >
                <span class="personal">
                    <asp:Label ID="lbTemplate" runat="server" Text=""></asp:Label>
                </span>
                <br> 
                <div id="template" style="display: none; width: 630px; height:100px; border: dotted 1px black; padding-left: 20px; padding-top: 10px;">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tr>
                            <td width="150px"><span class="personal">Проект: </span> </td>
                            <td> <asp:DropDownList Width="460px" ID="ddlProject" runat="server"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td width="150px"><span class="personal">Кол-во часов: </span> </td>
                            <td><asp:Label ID="lbHours" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td colspan="2"><img alt='' src="App_Resources/big_button_text.bmp" onclick="ShowTimesheet('template', 'times');" style="cursor: hand;"></td>
                        </tr>
                    </table>
                </div>
            </td>
            
        </tr> 
    </table>
    
    </div>
    <br>
    <div id="timesheet" >
        <center>
            
            <table cellpadding="0" cellspacing="0" style="margin-bottom: 10px;" width="760px">
                <tr>
                    <td align="left" valign="middle" width="150px"><asp:Label ID="lbAdd" runat="server" Text=""></asp:Label></td>
                    <td align="left" valign="middle" width="150px"><asp:ImageButton ID="btSave" ImageUrl="~/App_Resources/button_save.bmp" runat="server" OnClick="btSave_Click"/></td>
                    <td align="left" valign="middle" width="150px"><asp:Label ID="lbCheck" runat="server" Text=""></asp:Label></td>
                    <td>&nbsp;</td>
                    <td align="right" valign="middle" width="40px"><img alt='Отчет' src='App_Resources/export2excel.GIF' onclick="onReportClick();" style='cursor: hand; cursor: pointer; ' /></td>
                     <td align="right" valign="middle"  width="40px"><img alt='Справка' src='App_Resources/help.bmp' onclick="window.open('Docs/Инструкция%20по%20ведению%20ИТ-табеля.doc','_blank','menubar=no,width=800,height=600,resizable=yes,scrollbars=yes')" style='cursor: hand; cursor: pointer; ' /></td>
                </tr>
            </table>    
             <asp:Label ID="lbTable" runat="server" Text=""></asp:Label>
             
        </center>
    </div>
    <br><br>
    </form>
</body>
</html>
