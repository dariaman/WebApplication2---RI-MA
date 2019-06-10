var xmlHttpRequest;
function addCommas(clientID) {
    var nStr = document.getElementById(clientID.id).value;
    nStr += '';
    x = nStr.split(',');
    if (!x[0]) {
        x[0] = "0";
    }
    x1 = x[0];
    if (!x[1]) {
        x[1] = "00";
    }
    x2 = x.length > 1 ? ',' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + '.' + '$2');
    }
    document.getElementById(clientID.id).value = x1 + x2;
    return true;
}

function addCommas2(clientID) {
    var nStr = document.getElementById(clientID.id).value;
    nStr += '';
    x = nStr.split(',');
    if (!x[0]) {
        x[0] = "0";
    }
    x1 = x[0];
    if (!x[1]) {
        x[1] = "00";
    }
    x2 = x.length > 1 ? ',' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + '.' + '$2');
    }
    document.getElementById(clientID.id).value = x1 ;
    return true;
}

function removeCommas(clientID) {
    var nStr = document.getElementById(clientID.id).value;
    nStr = nStr.replace(/\./g, '');
    document.getElementById(clientID.id).value = nStr;
    $(clientID).select();
    return true;
}

function Check(textBox, maxLength) {
    if (textBox.value.length > maxLength) {
        alert("Max characters ALLowed are " + maxLength);
        textBox.value = textBox.value.substr(0, maxLength);
    }
}
function isKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode != 39)
        return true;
    return false;
}

function isKeyNumber(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode = 48 && charCode <= 57)
        return true;
    return false;
}

function isALLKeyDisable(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode >= 0 && charCode <= 128)
        return false;
    return false;
}

function Color_Changed(sender) {
    sender.get_element().value = "#" + sender.get_selectedColor();
}


function SelectALL(CheckBoxControl,grid) {
    if (CheckBoxControl.checked == true) {
        var i;
        for (i = 0; i < document.forms[0].elements.length; i++) {
            if ((document.forms[0].elements[i].name.indexOf(grid) > -1)) {
                document.forms[0].elements[i].checked = true;
            }
        }
    }
    else {
        var i;
        for (i = 0; i < document.forms[0].elements.length; i++) {
            if ((document.forms[0].elements[i].name.indexOf(grid) > -1)) {
                document.forms[0].elements[i].checked = false;
            }
        }
    }
}

function fuchange() {
    if (this.files[0].size > 8651000) {
        alert("File to big");
        document.getElementById('FrameContent_LinkUpload').style.visibility = "hidden";
    } else {
        document.getElementById('FrameContent_LinkUpload').style.visibility = "visible";
    }
}

$(".up,.down").click(function () {
        var row = $(this).parents("tr:first");
        if ($(this).is(".up")) {
            row.insertBefore(row.prev());
        } else {
            row.insertAfter(row.next());
        }
    });

$('[id*=FileUpload1]').live("change", function () {

    if (this.files[0].size > 8651000) {
        alert("Can't Attach file more than 8000.000 Bytes, file size are " + BytesFormat(this.files[0].size));
        document.getElementById('FrameContent_LinkUpload').style.visibility = "hidden";
    } else {
        document.getElementById("FrameContent_LinkUpload").style.visibility = "visible";
    }
});

$('[id*=FileUpload2]').live("change", function () {

    if (this.files[0].size > 8651000) {
        alert("Can't Attach file more than 8000.000 Bytes, file size are " + BytesFormat(this.files[0].size));
        document.getElementById('frame_btnAdd0').style.visibility = "hidden";
        document.getElementById('frame_LinkAdd0').style.visibility = "hidden";
    } else {
        document.getElementById("frame_btnAdd0").style.visibility = "visible";
        document.getElementById('frame_LinkAdd0').style.visibility = "visible";
    }
});

function BytesFormat(num) {
    return  num.toFixed(0).replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,") + " Bytes"
}

function Yes() {
    {
        var messageBox = document.getElementById('messageBox').style.display = 'none';
        var wholePage = document.getElementById('wholePage').style.display = 'none';
    }
}

function ismaxlength(objTxtCtrl, nLength, objTxtnext) {
    if (objTxtCtrl.value.length == nLength) {
        document.getElementById(objTxtnext).value = "";
        document.getElementById(objTxtnext).focus();
    }
}

function ismaxlengthless(objTxtCtrl, nLength, objTxtnext) {
    if (objTxtCtrl.value.length < nLength) {
        document.getElementById(objTxtnext).value = "";
    }
}

function keyCode13() {
    var keyCode = (event.which) ? event.which : event.keyCode;
    if ((keyCode == 13))
        event.returnValue = false;
}

//function replaceFocus(obj) {
//    //alert(obj.value);
//    if(obj.value == "" || obj.value == "Please input password..."){
//        obj.type = 'password';
//        document.getElementById("txtpassword").className = "textbox";
//        obj.style.height = "22px";
//        obj.style.width = "220px";
//        obj.style.fontStyle = "'Trebuchet MS', Verdana, sans-serif"
//        document.getElementById("txtpassword").style.fontWeight = '25px';
//    }
//    obj.focus
//}

//function replaceBlur(obj) {
//    if (obj.value == "Please input password..." || obj.value == "") {
//        obj.type = 'input';
//        document.getElementById("txtpassword").className = "textbox";
//        //obj.setAttribute("class", "textbox");
//        obj.style.height = "22px";
//        obj.style.width = "225px";
//        obj.style.fontStyle = "'Trebuchet MS', Verdana, sans-serif"
//        document.getElementById("txtpassword").style.border.width = "1px";
//        document.getElementById("txtpassword").style.border.style = "solid";
//        document.getElementById("txtpassword").style.fontWeight = '25px';
//    }
//}


function printDiv(divID) {
    //Get the HTML of div
    var divElements = document.getElementById(divID).innerHTML;
    //Get the HTML of whole page
    var oldPage = document.body.innerHTML;

    //Reset the page's HTML with div's HTML only
    document.body.innerHTML =
      "<html><head><title></title></head><body>" +
      divElements + "</body>";

    //Print Page
    window.print();

    //Restore orignal HTML
    document.body.innerHTML = oldPage;


}


var openwin = "";

function getLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition);
    } else {
        x.innerHTML = "Geolocation is not supported by this browser.";
    }
   // if (navigator.geolocation) {
   // navigator.geolocation.getCurrentPosition(function(position){
   //     getUserCoordinates(position);
   //     showLocation(position);
   //});
}

function showPosition(position) {

    openwin = "http://localhost/RI-MA/Provider.aspx?lat=" + position.coords.latitude + "&longt=" + position.coords.longitude;
    window.open(openwin, '_self');
 
 
}
