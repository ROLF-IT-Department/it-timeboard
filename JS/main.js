// JScript File


var leftPaddingToolTip = 150;



// разбираем введенные часы
function CheckHourType(node)
{
    var hour = node.value;
    if (hour.length == 0) return;
    

    
    // разбираем число
    if (hour.indexOf(',') < 0)
    {
        if ((parseInt(hour) > 24) || (parseInt(hour) < 0))
        { 
            alert("Время должно быть числом от 0 до 24!");    
            node.value = "";
            return;  
        } 
        
        if (!CheckDigits(hour))
        {
            alert("Недопустимый символ! Время должно быть числом от 0 до 24!");
            node.value = "";
            return;
        }

        return;  
        
    }
    else 
    {
        // берем целую часть десятичного числа
        var int_part = hour.substring(0, hour.indexOf(','));
        //alert(real);
        
        if (!CheckDigits(int_part))
        {
            alert("Недопустимый символ! Время должно быть десятичным числом от 0 до 24,00!");
            node.value = "";
            return;
        }
       
        if ((parseInt(int_part) > 24) || (parseInt(int_part) < 0))
        {
            alert("Время должно быть десятичным числом от 0 до 24,00!");
            node.value = "";
            return;
        }
        
        var real_part = hour.substring(hour.indexOf(',') + 1, hour.length);
        //alert(real_part);
        
        if (!CheckDigits(real_part))
        {
            alert("Недопустимый символ! Время должно быть десятичным числом от 0 до 24,00!");
            node.value = "";
            return;
        }
        
        if (real_part.length > 2)
        {
            alert("Недопустимый символ! Время должно быть десятичным числом от 0 до 24,00!");
            node.value = "";
            return;
        }
             
        
    }

}

// проверяем что в числе только цифры
function CheckDigits(number)
{
    var symbols = "1234567890";
    
    for (i=0; i<number.length; i++)
    {
        if (symbols.indexOf(number.charAt(i)) < 0)
        {
            return false;
        }
    }
    
    return true;
}

function OpenCheck()
{
    var year = document.getElementById("ddlYear");
    var selected_year = year.options[year.selectedIndex].value;
    
    var month = document.getElementById("ddlMonth");
    var selected_month = month.options[month.selectedIndex].value;
    
    var user = document.getElementById("user_id");
    var user_id = user.value;
    
    var url = "Check.aspx?month=" + selected_month + "&year=" + selected_year + "&id=" + user_id;
    
    window.open(url,'_blank','menubar=no,width=400,height=600,resizable=yes,scrollbars=yes');
}

function onCheckEmployees()
{
     var is_access = document.getElementById("is_access");
     if (is_access.value == "1")
        window.navigate("CheckEmployees.aspx");
     else
        alert("У вас нет прав доступа!");
}

function onReportClick()
{
    var year = document.getElementById("ddlYear");
    var selected_year = year.options[year.selectedIndex].value;
    
    var month = document.getElementById("ddlMonth");
    var selected_month = month.options[month.selectedIndex].value;
    
    var url = "Report.aspx?month=" + selected_month + "&year=" + selected_year;
     
    window.open(url,"_blank","menubar=no,width=800,height=600,resizable=yes,scrollbars=yes");
}

function onChangeMonth()
{
    var year = document.getElementById("ddlYear");
    var selected_year = year.options[year.selectedIndex].value;
    
    var month = document.getElementById("ddlMonth");
    var selected_month = month.options[month.selectedIndex].value;
    
    var netname = document.getElementById("netname");
    
    var url = "Default.aspx?month=" + selected_month + "&year=" + selected_year + "&netname=" + netname.value;
    
    window.navigate(url);
        
}

function onChangeYear()
{
    var year = document.getElementById("ddlYear");
    var selected_year = year.options[year.selectedIndex].value;
    
    var month = document.getElementById("ddlMonth");
    var selected_month = month.options[month.selectedIndex].value;
    
    var netname = document.getElementById("netname");
    
    var url = "Default.aspx?month=" + selected_month + "&year=" + selected_year + "&netname=" + netname.value;
    
    window.navigate(url);
        
}

function ShowBlock(id)
{
    var node = document.getElementById(id);
    if (node.style.display == "none")
        node.style.display = "block";
    else
        node.style.display = "none";
}

function numDays(month, year)
{
	switch (month) {
	    case "1": 
	        return 31;
		case "2":
			return (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0)) ? 29 : 28;
	    case "3": 
	        return 31;
		case "4": 
		    return 30;
		case "5": 
	        return 31;    
		case "6": 
		    return 30;
		case "7": 
	        return 31;   
	    case "8": 
	        return 31; 
	    case "9": 
		    return 30;  
		case "10": 
		    return 31;  
		case "11": 
		    return 30;
		case "12":
			return 31;
		
	}
}

function getFormatDate(day, month, year)
{    
    if (parseInt(month) < 10) month = "0" + month;
    
    var str_date = day + "." + month + "." + year;
    
    return str_date;

}

function ShowCalendar(node)
{
    
    var year = document.getElementById("ddlYear");
    var selected_year = year.options[year.selectedIndex].value;
    
    var month = document.getElementById("ddlMonth");
    var selected_month = month.options[month.selectedIndex].value;
        
    var date_initial;
    
    if (node.value != '') 
    {
        var date = node.value.split('.');
        date_initial = date[2] + date[1] + date[0];
        
    }
    else
    {
        var date = getFormatDate(15, selected_month, selected_year).split('.');
        date_initial = date[2] + date[1] + date[0];
    }

    selected_month--;

    Calendar.setup(
        {
          trigger : node.id,
          inputField  : node.id,         // ID of the input field
          dateFormat  : "%d.%m.%Y",    // the date format
          onSelect    : function() { this.hide() },
          electric    : false,
          selection   : date_initial,
          disabled    : function(date) { return (date.getMonth() == selected_month) && (date.getYear() == selected_year) ? false : true ;}
        }
    );

}


function SortRows(img)
{
    var table = document.getElementById("times");
    var len = table.childNodes.length;
    var first = table.firstChild;
    var tr;
    var i = 0; 
    var j = 0;
    if (img.alt == 'По возрастанию')
    {
        img.src = 'App_Resources/sort_down.bmp';
        img.alt = 'По убыванию';

        for(i = len - 1; i > 1 ; i--)
        {
            tr = first;
            for(j = 0 ; j < i-1 ; j++)
            {
                tr = tr.nextSibling;
                var td1 = tr.firstChild;
                var d1 = td1.nextSibling;
                var inp1 = d1.firstChild;
                var date1 = inp1.value;
                var tr2 = tr.nextSibling;
                var td2 = tr2.firstChild;
                var d2 = td2.nextSibling;
                var inp2 = d2.firstChild;
                var date2 = inp2.value;
                if (date1 > date2)
                {
                    table.insertBefore(tr2, tr);
                    tr = tr2;
                }
                
            }

        }

    }
    else
    {
        img.src = 'App_Resources/sort_up.bmp';
        img.alt = 'По возрастанию';
        
        for(i = len - 1; i > 1 ; i--)
        {
            tr = first;
            for(j = 0 ; j < i-1 ; j++)
            {
                tr = tr.nextSibling;
                var td1 = tr.firstChild;
                var d1 = td1.nextSibling;
                var inp1 = d1.firstChild;
                var date1 = inp1.value;
                var tr2 = tr.nextSibling;
                var td2 = tr2.firstChild;
                var d2 = td2.nextSibling;
                var inp2 = d2.firstChild;
                var date2 = inp2.value;
                if (date1 < date2)
                {
                    table.insertBefore(tr2, tr);
                    tr = tr2;
                }
                
            }

        }
    }
    
    CountRows(table);
    
}


function AddRow(tableID)
{

    var project = document.getElementById("ddlProject");

    var table = document.getElementById("times");
    
    var tr    = document.createElement('TR');
    var td0   = document.createElement('TD');
    var td1   = document.createElement('TD');
    var inp1  = document.createElement('INPUT');
    var td2   = document.createElement('TD');
    var sel2  = document.createElement('SELECT');
    var td4   = document.createElement('TD');
    var inp4  = document.createElement('INPUT');
    var td5   = document.createElement('TD');
    var img5  = document.createElement('IMG');
    var td6   = document.createElement('TD');
    var img6  = document.createElement('IMG');
    
    td0.style.width = "30px";
    td0.style.borderLeft = "solid 1px #999999";
    td1.style.width = "120px";
    td2.style.width = "500px";
    td4.style.width = "50px";
    td5.style.width = "30px";
    td6.style.width = "30px";
    
    countRow++;
    
    td0.innerHTML = countRow;        
    
    var inp1_id = countRow + '||' + 'newDate';
    inp1.setAttribute('id', inp1_id);
    inp1.setAttribute('name', inp1_id);
    inp1.setAttribute('readOnly', 'readonly');
    inp1.className = "input_date";
    inp1.onfocus = function() {ShowCalendar(this)} ; 
    inp1.onmouseover = function(){ShowToolTip(this, "hours", event.clientX - leftPaddingToolTip + document.documentElement.scrollLeft, event.clientY + document.documentElement.scrollTop)};
    inp1.onmouseout = function(){ShowBlock("hours");};
    
    var sel2_id = countRow + '||' + 'newProject';
    sel2.setAttribute('id', sel2_id);
    sel2.setAttribute('name', sel2_id);
    sel2.className = "select_project";
    
    var inp4_id = countRow + '||' + 'newHours';
    inp4.setAttribute('id', inp4_id);
    inp4.setAttribute('name', inp4_id);
    inp4.maxLength = "5";
    inp4.className = "input_hours";
    inp4.onblur = function(){CheckHourType(this)};
    inp4.onmouseover = function(){ShowToolTip(this, "hours", event.clientX + document.documentElement.scrollLeft, event.clientY + document.documentElement.scrollTop)};
    inp4.onmouseout = function(){ShowBlock("hours");};
    
    img5.setAttribute('src', 'App_Resources/add.gif');
    img5.style.cursor = "hand";
    img5.onclick = function() {CopyRow(this)} ; 
    img6.setAttribute('src', 'App_Resources/delete.gif');
    img6.style.cursor = "hand";
    img6.onclick = function() {DeleteRow(this)} ; 
    
    for(j=0; j<project.options.length; j++)
        sel2.options[j] = new Option(project.options[j].text, project.options[j].value);
    
    td1.appendChild(inp1);
    td2.appendChild(sel2);
    td4.appendChild(inp4);
    td5.appendChild(img5);
    td6.appendChild(img6);
   
    tr.appendChild(td0);
    tr.appendChild(td1);
    tr.appendChild(td2);
    tr.appendChild(td4);
    tr.appendChild(td5);
    tr.appendChild(td6);
    
    if (countRow > 0)
    {
        var tr_head = table.firstChild;
        table.insertBefore(tr, tr_head.nextSibling);
    }
    else
        table.appendChild(tr);
        
    CountRows(table);

}

function DeleteRow(node)
{
    var td = node.parentNode;
    var tr = td.parentNode;
    tr.parentNode.removeChild(tr);
    
    var table = document.getElementById("times");
    CountRows(table);
}

function CopyRow(node)
{
    var td = node.parentNode;
    var tr = td.parentNode;
    var table = tr.parentNode;
    
    var td_num = tr.firstChild;
    var td_date = td_num.nextSibling;
    var td_inp1 = td_date.firstChild;
    var td_project = td_date.nextSibling;
    var td_sel1 = td_project.firstChild;
    var td_hours = td_project.nextSibling;
    var td_inp2 = td_hours.firstChild;

    tr.style.backgroundColor = "#ACBECA";    
    
    var project = document.getElementById("ddlProject");
    var selected_project = project.options[project.selectedIndex].value;

    var new_tr    = document.createElement('TR');
    var td0   = document.createElement('TD');
    var td1   = document.createElement('TD');
    var inp1  = document.createElement('INPUT');
    var td2   = document.createElement('TD');
    var sel2  = document.createElement('SELECT');
    var td4   = document.createElement('TD');
    var inp4  = document.createElement('INPUT');
    var td5   = document.createElement('TD');
    var img5  = document.createElement('IMG');
    var td6   = document.createElement('TD');
    var img6  = document.createElement('IMG');
    
    new_tr.style.backgroundColor = "#ACBECA";
    td0.style.width = "30px";
    td0.style.borderLeft = "solid 1px #999999";
    td1.style.width = "120px";
    td2.style.width = "500px";
    td4.style.width = "50px";
    td5.style.width = "30px";
    td6.style.width = "30px";
    
    countRow++;
    
    td0.innerHTML = "&nbsp;";
        
    var inp1_id = countRow + '||' + 'newDate';
    inp1.setAttribute('id', inp1_id);
    inp1.setAttribute('name', inp1_id);
    inp1.className = "input_date";
    inp1.value = td_inp1.value;
    inp1.onfocus = function() {ShowCalendar(this)} ; 
    inp1.onmouseover = function(){ShowToolTip(this, "hours", event.clientX - leftPaddingToolTip + document.documentElement.scrollLeft, event.clientY + document.documentElement.scrollTop)};
    inp1.onmouseout = function(){ShowBlock("hours");};
      
    var sel2_id = countRow + '||' + 'newProject';
    sel2.setAttribute('id', sel2_id);
    sel2.setAttribute('name', sel2_id);
    sel2.className = "select_project";
        
    var inp4_id = countRow + '||' + 'newHours';
    inp4.setAttribute('id', inp4_id);
    inp4.setAttribute('name', inp4_id);
    inp4.maxLength = "5";
    inp4.className = "input_hours";
    inp4.value = td_inp2.value;
    inp4.onblur = function(){CheckHourType(this)};
    inp4.onmouseover = function(){ShowToolTip(this, "hours", event.clientX + document.documentElement.scrollLeft, event.clientY + document.documentElement.scrollTop)};
    inp4.onmouseout = function(){ShowBlock("hours");};
    
    img5.setAttribute('src', 'App_Resources/add.gif');
    img5.style.cursor = "hand";
    img5.onclick = function() {CopyRow(this)} ; 
    img6.setAttribute('src', 'App_Resources/delete.gif');
    img6.style.cursor = "hand";
    img6.onclick = function() {DeleteRow(this)} ; 
    
    for(j=0; j<project.options.length; j++)
            sel2.options[j] = new Option(project.options[j].text, project.options[j].value);
    
    sel2.options[td_sel1.selectedIndex].selected = true;
        
    td1.appendChild(inp1);
    td2.appendChild(sel2);
    td4.appendChild(inp4);
    td5.appendChild(img5);
    td6.appendChild(img6);
        
    new_tr.appendChild(td0);   
    new_tr.appendChild(td1);
    new_tr.appendChild(td2);
    new_tr.appendChild(td4);
    new_tr.appendChild(td5);
    new_tr.appendChild(td6);
    
    table.insertBefore(new_tr, tr.nextSibling);
    
    CountRows(table);
}

function CountRows(table)
{
    var len = table.childNodes.length;
    var first = table.firstChild;
    var tr = first;
    var i;
    for(i = 1; i < len; i++)
    {
        tr = tr.nextSibling;
        var td = tr.firstChild;
        td.innerText = i;
    }
}


function ShowTimesheet(id, tableID)
{
    var node = document.getElementById(id);
    node.style.display = "none";
    
    var year = document.getElementById("ddlYear");
    var selected_year = year.options[year.selectedIndex].value;

    var month = document.getElementById("ddlMonth");
    var selected_month = month.options[month.selectedIndex].value;
    
    var project = document.getElementById("ddlProject");
    var selected_project = project.options[project.selectedIndex].value;
    
    var temp_days = document.getElementById("template_days");
    
    var hours = document.getElementById("tbHours");

    var table = document.getElementById("times");

    var days = temp_days.value.split('|');
    var num_days = days.length;

    for(i=0; i < num_days; i++)
    {
                
        var tr    = document.createElement('TR');
        var td0   = document.createElement('TD');
        var td1   = document.createElement('TD');
        var inp1  = document.createElement('INPUT');
        var td2   = document.createElement('TD');
        var sel2  = document.createElement('SELECT');
        var td4   = document.createElement('TD');
        var inp4  = document.createElement('INPUT');
        var td5   = document.createElement('TD');
        var img5  = document.createElement('IMG');
        var td6   = document.createElement('TD');
        var img6  = document.createElement('IMG');
        
        td0.style.width = "30px";
        td0.style.borderLeft = "solid 1px #999999";
        td1.style.width = "120px";
        td2.style.width = "500px";
        td4.style.width = "50px";
        td5.style.width = "30px";
        td6.style.width = "30px";
        
        countRow++;
        
        td0.innerText = "&nbsp;";
        
        var inp1_id = countRow + '||' + 'newDate';
        inp1.setAttribute('id', inp1_id);
        inp1.setAttribute('name', inp1_id);
        inp1.setAttribute('readOnly', 'readonly');
        inp1.className = "input_date";
        inp1.value = getFormatDate(days[i], selected_month, selected_year);
        inp1.onfocus = function() {ShowCalendar(this)} ; 
        inp1.onmouseover = function(){ShowToolTip(this, "hours", event.clientX - leftPaddingToolTip + document.documentElement.scrollLeft, event.clientY + document.documentElement.scrollTop)};
        inp1.onmouseout = function(){ShowBlock("hours");};
        
        var sel2_id = countRow + '||' + 'newProject';
        sel2.setAttribute('id', sel2_id);
        sel2.setAttribute('name', sel2_id);
        sel2.className = "select_project";
        
        var inp4_id = countRow + '||' + 'newHours';
        inp4.setAttribute('id', inp4_id);
        inp4.setAttribute('name', inp4_id);
        inp4.maxLength = "5";
        inp4.className = "input_hours";
        inp4.value = hours.value;
        inp4.onblur = function(){CheckHourType(this)};
        inp4.onmouseover = function(){ShowToolTip(this, "hours", event.clientX + document.documentElement.scrollLeft, event.clientY + document.documentElement.scrollTop)};
        inp4.onmouseout = function(){ShowBlock("hours");};
        
        img5.setAttribute('src', 'App_Resources/add.gif');
        img5.style.cursor = "hand";
        img5.onclick = function() {CopyRow(this)} ; 
        img6.setAttribute('src', 'App_Resources/delete.gif');
        img6.style.cursor = "hand";
        img6.onclick = function() {DeleteRow(this)} ;
        
        
        for(j=0; j<project.options.length; j++)
        {
            sel2.options[j] = new Option(project.options[j].text, project.options[j].value);
            if (project.options[j].selected) 
                sel2.options[j].selected = true;
        }

        td1.appendChild(inp1);
        td2.appendChild(sel2);
        td4.appendChild(inp4);
        td5.appendChild(img5);
        td6.appendChild(img6);
        
        tr.appendChild(td0);
        tr.appendChild(td1);
        tr.appendChild(td2);
        tr.appendChild(td4);
        tr.appendChild(td5);
        tr.appendChild(td6);
        
        table.appendChild(tr);
        

    }
    
    CountRows(table);

}

function ShowToolTip(node, tool_tip, posX, posY)
{   

    var tip = document.getElementById(tool_tip);

    tip.style.left = posX;
    tip.style.top = posY;

    var td = node.parentNode;
    var tr = td.parentNode;
    var tbody = tr.parentNode;
    
    var td_num = tr.firstChild;
    var td_date = td_num.nextSibling;
    var td_project = td_date.nextSibling;
    var td_hours = td_project.nextSibling;
    
    var date = td_date.firstChild.value;
    var hours = td_hours.firstChild.value;
    
    var len = tbody.childNodes.length;
    var first = tbody.firstChild;
    var tr = first.nextSibling;
    
    var i; 
    var sum = 0;
    

    for(i = 1; i < len ; i++)
    {
     
        var td_num1 = tr.firstChild;
        var td_date1 = td_num1.nextSibling;
        var td_project1 = td_date1.nextSibling;
        var td_hours1 = td_project1.nextSibling;
        
        var date1 = td_date1.firstChild.value;
        var hours1 = td_hours1.firstChild.value;
        
        if (date == date1)
        {
            if (hours1 != '')
            {
                if (hours1.indexOf(',') < 0) 
                {
                    var h = hours1 + '.00';
                    sum = sum + parseFloat(h);
                }
                else
                {   
                    var h = hours1.replace(',' , '.');
                    if (h.length == 1) 
                        h = h + '0';
                    sum = sum + parseFloat(h);

                }
            } 
        }
        
        tr = tr.nextSibling;   
        
    }
    
    var sum_cutted = sum.toString();
    if (sum_cutted.indexOf('.') > 0)
        sum_cutted = sum_cutted.replace('.' , ',');

    tip.innerText = "За " + date + " заполнено " + sum_cutted + " часов";
    
    if (tip.style.display == "none")
        tip.style.display = "block";
        
}

