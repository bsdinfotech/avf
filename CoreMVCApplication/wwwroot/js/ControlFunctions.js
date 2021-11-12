// JScript File

function allowCapsOnly(elmID,len) //allows only capital letters and restrict to a given length
{
if(RestrictTextAreaLength(elmID, len))
{
var el=document.getElementById(elmID);
if((event.keyCode==46 || event.keyCode==32) && el.value.length==0)
{
event.keyCode = 0;
}
else
{
    if ((event.keyCode >= 65 && event.keyCode <= 90) || (event.keyCode >= 33 && event.keyCode <= 57) || (event.keyCode >= 97 && event.keyCode <= 122) || window.event.charCode == 8 || event.keyCode == 9 || event.keyCode == 95 || event.keyCode == 16 || event.keyCode == 13){
        if (event.keyCode >= 97 && event.keyCode <= 122) 
           {
            event.keyCode = event.keyCode - 32;
            }
       count=-1;
     }
    else if(event.keyCode==46 || event.keyCode==32)
     {
               if(count!=event.keyCode)
                    {
                    count=event.keyCode;
                    }
               else
                    {
                    event.keyCode=0;
                    }
      }
    else {
        event.keyCode = 0;
         }
}
}
else
{
return false;
}
}

function capsall(fld, len) {
    if (RestrictTextAreaLength(fld, len)) {
        if (event.keyCode >= 97 && event.keyCode <= 122) {
            event.keyCode = event.keyCode - 32;
        }
    }
    else {
        return false;
    }
}

function RestrictTextAreaLength(fld, len) {
val=document.getElementById(fld).value;
if (val.length >= len) {
    return false;
}
else {
    return true;
}
}

function allowdigit(fldID,len) {
    if (RestrictTextAreaLength(fldID, len)) {
        if (event.keyCode == 40 || event.keyCode == 41 || event.keyCode == 43 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 16 || (event.keyCode >= 48 && event.keyCode <= 57)) {

        }
        else {
            event.keyCode = 0;
        }
    }
    else {
        return false;
    }
    
}
function BlockKey() {
    var keyCode = event.which ? event.which : event.keyCode;
    if (keyCode != 9) {
        event.returnValue = false;
    }
}

// required form fields function

function formCheck(fldReq, fldDesc, Msg, chkFlds, chkDesc) {
    fieldRequired = fldReq.split(',');
    fieldDescription = fldDesc.split(',');
    var alertMessage = Msg+"\n\n";
    var amLen = alertMessage.length;
    var flag = '';
    for (var i = 0; i < fieldRequired.length; i++) {
        var obj = document.getElementById(fieldRequired[i]);
        switch (obj.type) {
            case "select-one":
                if (obj.selectedIndex == 0 || obj.options[obj.selectedIndex].text == "") {
                    alertMessage += " -> " + fieldDescription[i] + '\n';
                    if (flag.length == 0) {
                        flag = fieldRequired[i];
                    }
                }
                break;
            case "select-multiple":
                if (obj.selectedIndex == -1) {
                    alertMessage += " -> " + fieldDescription[i] + '\n';
                    if (flag.length == 0) {
                        flag = fieldRequired[i];
                    }
                }
                break;
            case "text":
            case "textarea":
                if (obj.value == null || obj.value == "") {
                    alertMessage += " -> " + fieldDescription[i] + '\n';
                    if (flag.length == 0) {
                        flag = fieldRequired[i];
                    }
                }
                break;
            case "checkbox":

                break;
            default:
                var obj1 = document.getElementsByName(fieldRequired[i]);
                var blnchecked = false;
                for (var k = 1; k < obj1.length; k++) {
                    if (obj1[k].checked) {
                        blnchecked = true;
                        break;
                    }
                }
                if (!blnchecked) {
                    alertMessage += " -> " + fieldDescription[i] + '\n';
                    if (flag.length == 0) {
                        flag = fieldRequired[i];
                    }
                }
        }
    }
    if (chkFlds.length > 0) {
        var chkFields = chkFlds.split(',');
        var chkDescription = chkDesc.split(',');
        for (var x = 0; x < chkFields.length; x++) {
            var obj1 = document.getElementsByTagName("input");
            for (var l = 0; l < obj1.length; l++) {
                k = obj1[l].id + "";
                o = k.indexOf("_", 0);
                k = k.substring(0, o);
                blnchecked = false;
                if (k == chkFields[x]) {
                    if (obj1[l].checked) {
                        blnchecked = true;
                        break;
                    }
                }
            }
            if (!blnchecked) {
                alertMessage += " -> " + chkDescription[x] + '\n';
                if (flag.length == 0) {
                    flag = chkFields[x];
                }
            }
        }
    }
    if (alertMessage.length == amLen) {
        return true;
    }
    else {
        alert(alertMessage);
        document.getElementById(flag).focus();
        return false;
    }
}


//To print value of InvalidCode

function CheckCode(name) {
    var value = "";
    var field = document.getElementById("InvalidCode");

    if (name != "") {
        field = document.getElementById(name);
    }


    if (field.value != null) {
        value = field.value;
        field.value = "";
    }

    if (value != "") {
        alert(value);
    }
}

//function for LOV

function OpenInvoice(name, ele_name, ele_values, field) {
    var curDateTime = new Date()
    var curSec = curDateTime.getSeconds()
    var fieldname = field.split("#");
    var elementname = ele_name.split("#");
    var elementvalue = ele_values.split("#");
    var LovString = new String();

    LovString = name + "?sec=" + curSec;

    for (i = 0; i < elementname.length; i++) {
        var elename = "";
        if (document.getElementById(elementvalue[i]) != null) {
            elename = document.getElementById(elementvalue[i]).value;
        }
        else {
            elename = elementvalue[i];
        }

        LovString = LovString + "&" + elementname[i] + "=" + elename;
    }

    var value = window.showModalDialog(LovString, "", "");

    if (value != null) {
        var i = 0;
        for (i = 0; i < fieldname.length; i++) {
            if (fieldname[i] != "") {
                document.getElementById(fieldname[i]).value = value[i];
            }
        }
        return true;
    }
    else {
        return false;
    }
}

function OpenMulSelectLOV(name, ele_name, ele_values, field) {
    var curDateTime = new Date()
    var curSec = curDateTime.getSeconds()
    var fieldname = field;
    var elementname = ele_name.split("#");
    var elementvalue = ele_values.split("#");
    var LovString = new String();
    LovString = name + "?sec=" + curSec;

    for (i = 0; i < elementname.length; i++) {
        var elename = "";
        if (document.getElementById(elementvalue[i]) != null) {
            elename = document.getElementById(elementvalue[i]).value;
        }
        else {
            elename = elementvalue[i];
        }
        LovString = LovString + "&" + elementname[i] + "=" + elename;
    }

    var val = window.showModalDialog(LovString, "","dialogWidth:250px");
    if (val != null) {
        var i = 0;
        mstr=val.split("#");
        document.getElementById(fieldname.split("#")[0]).value=mstr[1]; 
        document.getElementById(fieldname.split("#")[1]).value=mstr[0]; 
        }
    else {
        return false;
    }
}

//Function to open other aspx in dialog window

function OpenPage(page) {
    window.open(page,'Group Master','width=607,height=500');
}

function numeralOnly(event) {
    evt = event;
    var dt = event.srcElement;
    var charCode = (evt.charCode) ? evt.charCode : ((evt.keyCode) ? evt.keyCode : ((evt.which) ? evt.which : 0));

    if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46) {
        return false;
    }
    else {
        if (dt != null) {
            if (dt.value.lastIndexOf(".") > -1 && charCode == 46) {
                return false;
            }
            else {
                return true;
            }
        }
    }
}

function numeralOnlyOpening(event) {
    evt = event;
    var dt = event.srcElement;
    var charCode = (evt.charCode) ? evt.charCode : ((evt.keyCode) ? evt.keyCode : ((evt.which) ? evt.which : 0));

    if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46 && charCode != 45) {
        return false;
    }
    else {
        if (dt != null) {
            if ((dt.value.lastIndexOf(".") > -1 && charCode == 46) || (dt.value.lastIndexOf("-") > -1 && charCode == 45)) {
                return false;
            }
            else {
                return true;
            }
        }
    }
}


function numeraldigits(event) {
    evt = event;
    var dt = event.srcElement;
    var charCode = (evt.charCode) ? evt.charCode : ((evt.keyCode) ? evt.keyCode : ((evt.which) ? evt.which : 0));

    if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46) {
        return false;
    }
    else {
        if (dt != null) {
            if (dt.value.lastIndexOf(".") > -1 && charCode == 46) {
                return false;
            }
            else {
                return true;
            }
        }
    }
}

function formatDate(fldid)
{
el=document.getElementById(fldid);
date=el.value;
len=date.length;
flg=0;
if(len==2 || len==5)
{
el.value=el.value+"/";
}
if(len==10)
{
return false
}
}

function validateDate(fldid,compfld)
{
el=document.getElementById(fldid);
date=el.value;
len=date.length;
flg=0;
dt1='';
temp='';
var dt=new Array(3);
dt=date.split('/');

if(len ==10)
{
    if ((dt[0]<1 || dt[0] >31) || (dt[1]<1 || dt[1]>12)||(dt[2].length != 4)) 
        {
         alert("Invalid date. Enter date in dd/mm/yyy format.");
         el.focus();
         }
        else
        {
            if(compfld !='N')
                {
                dt1 = document.getElementById(compfld).value.split('/');
                year1 = dt1[2];
                month1 = dt1[1];
                day1 = dt1[0];
                temp='Voucher Date';
                 }
            else
                {
                dt1 = new Date();
                temp='Current Date';
                year1 = dt1.getFullYear(); // Returned year XXXX
                month1 = dt1.getMonth();
                day1 = dt1.getDate();
                month1=(month1+1);
                }
               
            if(parseInt(dt[2]) > parseInt(year1) || parseInt(dt[1]) > parseInt(month1))
              {
               flg=1;
              alert('Date should be less than '+temp);
               el.focus();
               }
            else if(flg==0 && (parseInt(dt[2]) == parseInt(year1)) && (parseInt(dt[1]) == parseInt(month1)) )
               {
                if(parseInt(dt[0]) > parseInt(day1))
                   {
                    alert('Date should be less than '+temp);
                    el.focus();
                    }
              }
        }
    
}
else
{
alert("Invalid date. Enter date in dd/mm/yyy format.");
el.focus();
}
}

function calcAmount(qty, rate, tamount) {
    a = document.getElementById(qty).value;
    b = document.getElementById(rate).value;
    c = a * b;
    c=c.toFixed(2);
    document.getElementById(tamount).value = c + "";
}
function calcRate(qty, rate, tamount) {
    
    
    a = document.getElementById(qty).value;
    b = document.getElementById(tamount).value;
    if(a==0 && b==0)
    {
    c=0;
    }
    else
    {
    c = b / a;
    c=c.toFixed(2);
    }
    document.getElementById(rate).value = c + "";
}
function clearForm(fields,focfld) {
flds =fields.split(',');
var i=0;
for (i = 0; i < flds.length; i++) {
    document.getElementById(flds[i]).value = "";
}
document.getElementById('OrgMode').value = "ADD";
if(focfld!='')
{
document.getElementById(focfld).focus();
}
}

function allowchardigit() {
    if ((event.keyCode >= 65 && event.keyCode <= 90) || (event.keyCode >= 97 && event.keyCode <= 122) || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 16 || (event.keyCode >= 48 && event.keyCode <= 57) || event.keyCode == 46) {
        if (event.keyCode >= 97 && event.keyCode <= 122) {
            event.keyCode = event.keyCode - 32;
        }
    }
    else {
        event.keyCode = 0;
    }
}
        function viewsess(el,o)
        {
        if(o==1)
        {
        document.getElementById(el).style.display='block';
        }
        else
        {
        document.getElementById(el).style.display='none';
        }
    }
    function SetYearEnd(YS, YE) {
        e = document.getElementById(YS).value;
        e = (parseInt(e) + 1);
        document.getElementById(YE).value = e;
    }
    
    
function OpenBill(name, ele_name, ele_values) {
var curDateTime = new Date()
var curSec = curDateTime.getSeconds()
var elementname = ele_name.split("#");
var elementvalue = ele_values.split("#");
var LovString = new String();
LovString = name + "?sec=" + curSec;
for (i = 0; i < elementname.length; i++) 
    {
     var elename = "";
     if (document.getElementById(elementvalue[i]) != null) 
        {
            elename = document.getElementById(elementvalue[i]).value;
        }
     else 
        {
            elename = elementvalue[i];
        }

        LovString = LovString + "&" + elementname[i] + "=" + elename;
    }
    var value = window.showModalDialog(LovString,"Bills","dialogWidth:420px");
}

function hideTab(ids,Dflds)
{
flds=ids.split("#");
dispflds=Dflds.split("#");
for (i = 0; i < flds.length; i++) 
{
document.getElementById(flds[i]).style.display='none';
}
for (i = 0; i < dispflds.length; i++) 
{
document.getElementById(dispflds[i]).style.display='block';
}
}

function numOnly(event) {
    evt = event;
    var dt = event.srcElement;
    var charCode = (evt.charCode) ? evt.charCode : ((evt.keyCode) ? evt.keyCode : ((evt.which) ? evt.which : 0));

    if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46) {
        return false;
    }
    else {
        if (dt != null) {
            if (dt.value > -1 && charCode == 46) {
                return false;
            }
            else {
                return true;
            }
        }
    }
}


function allowNegativeNumberWithDecimal(event) {
    evt = event;
    var dt = event.srcElement;
    var charCode = (evt.charCode) ? evt.charCode : ((evt.keyCode) ? evt.keyCode : ((evt.which) ? evt.which : 0));

    if (charCode != 45 && charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46) {
        return false;
    }
    else {
        if (dt != null) {
            if (dt.value.lastIndexOf(".") > -1 && charCode == 46) {
                return false;
            }
            else {
                return true;
            }
        }
    }
}


function ConDbl(value, dots) {
    var field = value;
    var value = 0.00;
    if (isNaN(parseFloat(field)) || field == "") {
        field = "0.00";
    }
    value = parseFloat(parseFloat((parseFloat(field) * Math.pow(10, dots)) / Math.pow(10, dots)).toFixed(dots));
    return value;
}