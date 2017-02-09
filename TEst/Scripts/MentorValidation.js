function Captcha() {
    var alpha = new Array('A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z');
    var i;
    for (i = 0; i < 6; i++) {
        var a = alpha[Math.floor(Math.random() * alpha.length)];
        var b = alpha[Math.floor(Math.random() * alpha.length)];
        var c = alpha[Math.floor(Math.random() * alpha.length)];
        var d = alpha[Math.floor(Math.random() * alpha.length)];
        var e = alpha[Math.floor(Math.random() * alpha.length)];
        var f = alpha[Math.floor(Math.random() * alpha.length)];
        var g = alpha[Math.floor(Math.random() * alpha.length)];
    }
    var code = a + ' ' + b + ' ' + ' ' + c + ' ' + d + ' ' + e + ' ' + f + ' ' + g;
    $("#maincaptcha").val(code);
}
function ValidCaptcha() {
    var string1 = removeSpaces($('#maincaptcha').val());
    var string2 = removeSpaces($('#Captcha').val());
    if (string1 == string2) {
        return true;
    }
    else {
        return false;
    }
}
function removeSpaces(string) {
    return string.split(' ').join('');
}

$(document).ready(function () {
    
    Captcha();
    $("#ZIP1").keydown(function (e) {
        // Allow: backspace, delete, tab, escape, enter and .
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
            // Allow: Ctrl+A, Command+A
            (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            // Allow: home, end, left, right, down, up
            (e.keyCode >= 35 && e.keyCode <= 40)) {
            // let it happen, don't do anything
            return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });
    $("#refresh").click(function () {
        Captcha();
    });
    $.ajax({
      // url: "http://priyanet.us/MentoringAPI/Mentor/GetState",
        url: "http://localhost:62599/Mentor/GetState",
        type: "GET",
        data: {},
        dataType: 'json',
        async: false
    }).done(function (resp) {
        for (var i = 0; i < resp.length; i++) {
            var opt = new Option(resp[i].stateName, resp[i].stateCode)
            $('#StateProvinceRegion1').append(opt);
        }
    }).error(function (err) {
        alert("Error " + err.status);
    });

    //$('#FirstName').blur(function () {
       
    //    var firstname = $('#FirstName').val();
    //    
    //    if ($('#FirstName').val() != "") {
    //        $.ajax({
    //            url: "http://localhost:62599/Mentor/FirstNameExists",
    //            type: "GET",
    //            data: { FirstName: firstname },
    //            dataType: 'json',
    //            async: false
    //        }).done(function (resp) {
    //            if (resp == 1) {
    //                alert("FirstName Already Exists");
    //                $('#FirstName').val("");
    //                $('#FirstName').focus();
    //            }
    //        }).error(function (err) {
    //            alert("ERROR:" + err.status);
    //        });
    //    }
    //});

    function ValidateWebsite(objElement) {
        
        var isvalid = false;
        var Strurl = objElement;
        var re = /^(http|https):\/\/(([a-zA-Z0-9$\-_.+!*'(),;:&=]|%[0-9a-fA-F]{2})+@)?(((25[0-5]|2[0-4][0-9]|[0-1][0-9][0-9]|[1-9][0-9]|[0-9])(\.(25[0-5]|2[0-4][0-9]|[0-1][0-9][0-9]|[1-9][0-9]|[0-9])){3})|localhost|([a-zA-Z0-9\-\u00C0-\u017F]+\.)+([a-zA-Z]{2,}))(:[0-9]+)?(\/(([a-zA-Z0-9$\-_.+!*'(),;:@&=]|%[0-9a-fA-F]{2})*(\/([a-zA-Z0-9$\-_.+!*'(),;:@&=]|%[0-9a-fA-F]{2})*)*)?(\?([a-zA-Z0-9$\-_.+!*'(),;:@&=\/?]|%[0-9a-fA-F]{2})*)?(\#([a-zA-Z0-9$\-_.+!*'(),;:@&=\/?]|%[0-9a-fA-F]{2})*)?)?$/;
        if (!re.test(Strurl)) {

            isvalid = false;
        } else {
            isvalid = true;
        }
        return isvalid;
    }
    $("#LinkedIn").blur(function () {
        
        var isvalid;
        isvalid = ValidateWebsite($("#LinkedIn").val());
        ValidateUrls($("#LinkedIn").val(), isvalid, $("#LinkedIn"));
    });
    function ValidateUrls(objurlvalue, isvalid, websiteid) {

        if (isvalid == true) {

        }
        else {
            if (objurlvalue != "") {
                alert("LinkedIn url is not valid.");
                websiteid.val('');
                websiteid.focus();
            }

        }
    }
    function ValidateEmail(email) {
        var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
        return expr.test(email);
    };
    $("#PrimaryEmail").blur(function () {
        
        if ($("#PrimaryEmail").val() == "") {
        }
        else {
            if (!ValidateEmail($("#PrimaryEmail").val())) {
                alert("Invalid email address.");
                $("#PrimaryEmail").css('border', '1px solid red');
                return false;
            }
            return false;
        }
     
         if ($("#PrimaryEmail").val()!="")
        {
            var email = $("#PrimaryEmail").val();
            $.ajax({
                // url: "http://priyanet.us/MentoringAPI/Mentor/EmailExists",
                url: "http://localhost:62599/Mentor/EmailExists",
              type:"GET",
              data: { EmailId: email },
              datatype:'json',
              async:false
                
            }).done(function (resp) {
                if (resp == 1) {
                    alert("Primary Email already Exsits");
                    $("#PrimaryEmail").val("");
                    $("#PrimaryEmail").focus();
                }
            }).error(function (err) {
                alert("Error"+err.status);
            });
        }
        return false;
    });
    $("#PrimaryEmail").focusout(function () {
        $("#PrimaryEmail").css('border', '1px solid #d6d6d6');
    });
    $('#Telephone1').keydown(function (e) {
        //PhoneMask(e, '#txtphone');

        var key = e.which || e.charCode || e.keyCode || e.ctrlKey || e.metaKey || 0;
        $phone = $(this);

        if (key >= 65 && key <= 90 ) {

        }
        else if (key == 9) {
        }
        else {
            if ($phone.val().charAt(0) !== '(') {
                $phone.val('(' + $phone.val());
            }
        }
        // Auto-format- do not expose the mask as the user begins to type
        if (key !== 8 && key !== 9) {
            if ($phone.val().length === 4) {
                $phone.val($phone.val() + ')');
            }
            if ($phone.val().length === 5) {
                $phone.val($phone.val() + ' ');
            }
            if ($phone.val().length === 9) {
                $phone.val($phone.val() + '-');
            }
        }

        // Allow numeric (and tab, backspace, delete) keys only
        return (key == 8 ||
                key == 9 ||
                key == 46 ||
                (key >= 48 && key <= 57) ||
                (key >= 96 && key <= 105) ||
                ((key == 65 || key == 86 || key == 67) && (key === true)));
    });
    $('#Telephone2').keydown(function (e) {
        //PhoneMask(e, '#txtphone');

        var key = e.which || e.charCode || e.keyCode || e.ctrlKey || e.metaKey || 0;
        $phone = $(this);

        if (key >= 65 && key <= 90) {

        }
        else if (key == 9) {
        }
        else {
            if ($phone.val().charAt(0) !== '(') {
                $phone.val('(' + $phone.val());
            }
        }
        // Auto-format- do not expose the mask as the user begins to type
        if (key !== 8 && key !== 9) {
            if ($phone.val().length === 4) {
                $phone.val($phone.val() + ')');
            }
            if ($phone.val().length === 5) {
                $phone.val($phone.val() + ' ');
            }
            if ($phone.val().length === 9) {
                $phone.val($phone.val() + '-');
            }
        }

        // Allow numeric (and tab, backspace, delete) keys only
        return (key == 8 ||
                key == 9 ||
                key == 46 ||
                (key >= 48 && key <= 57) ||
                (key >= 96 && key <= 105) ||
                ((key == 65 || key == 86 || key == 67) && (key === true)));
    });
    $("#btnsubmit").click(function () {
        var valid = false;
        
        if($("#FirstName").val()==""){
            alert(" Please enter FirstName");
            $("#FirstName").focus();
            return false;
        }
        else if ($("#LastName").val() == "") {
            alert(" Please enter LastName");
            $("#FirstName").focus();
            return false;
        }
        else if ($("#Telephone1").val() == "") {
            alert(" Please enter Phone");
            $("#Telephone1").focus();
            return false;
        }
        else if ($("#PrimaryEmail").val() == "") {
            alert(" Please enter Email");
            $("#PrimaryEmail").focus();
            return false;
        }
            //else if ($("#Resume").val() == "") {
            //    alert("Please enter Resume");
            //    $("#Resume").focus();
            //    return false;
            //}
        else if ($("#Captcha").val() == "") {
            alert(" Please enter Captcha");
            $("#Captcha").focus();
            return false;
        }
        else {
            
            valid = ValidCaptcha();
            if (valid == true) {
                var StateName = $("#StateProvinceRegion1 :selected").text();
                var ID = $("#StateProvinceRegion1 :selected").val();
                var resumedoc = $('#Resume')[0];
                var formdata = new FormData($('#form1'));
                if (resumedoc.value == "") {
                    formdata.append('Resume', "");
                }
                else {
                    formdata.append('Resume', resumedoc.files[0], resumedoc.files[0].name);
                }
                formdata.append('FirstName', $("#FirstName").val());
                formdata.append('LastName', $("#LastName").val());
                formdata.append('Organization', $("#Organization").val());
                formdata.append('Title', $("#Title").val());
                formdata.append('Street1', $("#Street1").val());
                formdata.append('AddressLine1', $("#AddressLine1").val());
                formdata.append('City1', $("#City1").val());
                formdata.append('StateProvinceRegion1', $("#StateProvinceRegion1").val());
                formdata.append('ZIP1', $("#ZIP1").val());
                formdata.append('Telephone1', $("#Telephone1").val());
                formdata.append('Telephone2', $("#Telephone2").val());
                formdata.append('PrimaryEmail', $("#PrimaryEmail").val());
                formdata.append('LinkedIn', $("#LinkedIn").val());

                $.ajax({
                    //url: "http://priyanet.us/MentoringAPI/Mentor/Submit",
                    url: "http://localhost:62599/Mentor/Submit",
                    type: "POST",
                    enctype: 'multipart/form-data',
                    async: false,
                    contentType: false,
                    processData: false,
                    data: formdata
                }).done(function (resp) {
                    if (resp == "success") {
                    alert("Mentor " + resp);
                    //window.location.href = "http://nexttech.digital-55.com/";
                    // window.location.href = "file:///D:/PROJECTS/mentoring/TEst/Mentorform.html";
                    window.location.href = "http://priyanet.us/Nexttech/Mentorform.html";
                }
                else{
                 alert("Mentor " + resp);
                    }
                }).error(function (err) {
                    alert("Error " + err.status);
                });
                return false;
            }
            else {
                valid = false;
                $('#Captcha').val('');
                alert('Please Enter Valid Captcha');
                return valid;
            }
        }
        return valid;
    });
    $("#btnclear").click(function () {
        $("#FirstName").val("");
        $("#LastName").val("");
        $("#Organization").val("");
        $("#Title").val("");
        $("#Street1").val("");
        $("#AddressLine1").val("");
        $("#City1").val("");
        $("#ZIP1").val("");
        $("#Telephone1").val("");
        $("#Telephone2").val("");
        $("#PrimaryEmail").val("");
        $("#LinkedIn").val("");
        $("#Resume").val("");
        $("#Captcha").val("");
       

    });
    $("#get_file").click(function () {
        $('#Resume').trigger('click');
    });
    $("#Resume").on('change',function (e) {
        
      
        var myfile = $('#Resume').val();
        var ext = myfile.split('.').pop();
        //var extension = myfile.substr( (myfile.lastIndexOf('.') +1) );

        if (ext == "pdf" || ext == "docx" || ext == "doc") {
            $('#customfileupload').html($(this).val().split('\\').pop());
        }
        else {
            alert("Please choose only pdf,docx,doc format files");
        }
    });
});

