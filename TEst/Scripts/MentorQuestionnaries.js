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
    var MentorId = GetParameterValues('ID');


    function GetParameterValues(param) {
        
        var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < url.length; i++) {
            var urlparam = url[i].split('=');
            if (urlparam[0] == param) {
                return urlparam[1];
            }
        }
    }
   
    $("#refresh").click(function () {
        Captcha();
    });
    function ValidateEmail(email) {
        var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
        return expr.test(email);
    };
    $("#PrimaryEmail").blur(function () {
        if ($("#PrimaryEmail").val() == "") {

        } else {
            if (!ValidateEmail($("#PrimaryEmail").val())) {
                alert("Invalid email address.");
                $("#PrimaryEmail").css('border', '1px solid red');
                return false;
            }
        }
        return false;
    });
    $("#SecondaryEmail").blur(function () {
        if ($("#SecondaryEmail").val() == "") {
        } else {
            if (!ValidateEmail($("#SecondaryEmail").val())) {
                alert("Invalid email address.");
                $("#SecondaryEmail").css('border', '1px solid red');
                return false;
            }
            else if ($("#SecondaryEmail").val() == $("#PrimaryEmail").val()) {
                alert("Primary Email and Secondary Email should Not be Same");
                $("#SecondaryEmail").css('border', '1px solid red');
                $("#SecondaryEmail").val("");
                return false;
            }
        }
        return false;
    });
    $("#PrimaryEmail").focusout(function () {
        $("#PrimaryEmail").css('border', '1px solid #d6d6d6');
    });

    $("#SecondaryEmail").focusout(function () {
        $("#SecondaryEmail").css('border', '1px solid #d6d6d6');
    });

    $('#Telephone1').keydown(function (e) {
        Phone();
    });

    //PhoneMask(e, '#txtphone');
    function Phone(e) {
        var key = e.which || e.charCode || e.keyCode || e.ctrlKey || e.metaKey || 0;
        $phone = $(this);

        if (key >= 65 && key <= 90) {

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
    }

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
    $('#Telephone3').keydown(function (e) {

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
    $("#ZIP2").keydown(function (e) {

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
    
    $.ajax({
        //url: "http://priyanet.us/MentoringAPI/Mentor/GetCountry",
        url: "http://localhost:62599/Mentor/GetCountry",
        type: "GET",
        data: {},
        dataType: 'json',
        async: false
    }).done(function (resp) {
        for (var i = 0; i < resp.length; i++) {
            var opt = new Option(resp[i].countryName, resp[i].countryID);
            var opt1 = new Option(resp[i].countryName, resp[i].countryID);
            $('#Country1').append(opt);
            $('#Country2').append(opt1);
        }
    }).error(function (err) {
        alert("Error " + err.status);

    });
    
    $.ajax({
        //url: "http://priyanet.us/MentoringAPI/Mentor/GetMentorDetails",
        url: "http://localhost:62599/Mentor/GetMentorDetails",
        type: "GET",
        data: { MentorId: MentorId },
        dataType: 'json',
        async: false
    }).done(function (resp) {

        $('#FirstName').val(resp.firstName);
        $('#LastName').val(resp["lastName"]);
        $('#Organization').val(resp["organization"]);
        $('#Title').val(resp["title"]);
        $('#Street1').val(resp["street1"]);
        $('#AddressLine1').val(resp["addressLine1"]);
        $('#City1').val(resp["city1"]);
        $('#StateProvinceRegion1').val(resp["stateProvinceRegion1"]);
        $('#ZIP1').val(resp["ziP1"]);
        $('#PrimaryEmail').val(resp["primaryEmail"]);
        $('#LinkedIn').val(resp["linkedIn"]);
        $('#Telephone1').val(resp["telephone1"]);
        $('#Telephone2').val(resp["telephone2"]);

    }).error(function (err) {
        alert("Error " + err.status);

    });


    $("#btnsubmit").click(function () {

        

        var Startups = [];
        $.each($("input[name='Startups']:checked"), function () {
            Startups.push($(this).val());
        });

        var BusinessModel = [];
        $.each($("input[name='BusinessModel']:checked"), function () {
            BusinessModel.push($(this).val());
        });

        var Funding = [];
        $.each($("input[name='Funding']:checked"), function () {
            Funding.push($(this).val());
        });

        var Software = [];
        $.each($("input[name='Software']:checked"), function () {
            Software.push($(this).val());
        });
        var LifeSciences = [];
        $.each($("input[name='LifeSciences']:checked"), function () {
            LifeSciences.push($(this).val());
        });

        var IndustryOther = [];
        $.each($("input[name='IndustryOther']:checked"), function () {
            IndustryOther.push($(this).val());
        });
        var FunctionalAreas = [];
        $.each($("input[name='FunctionalAreas']:checked"), function () {
            FunctionalAreas.push($(this).val());
        });


        var objmentordetail = {
            MentorId: MentorId,
            Prefix: $("#Prefix :selected").val(),
            FirstName: $("#FirstName").val(),
            MiddleName: $("#MiddleName").val(),
            LastName: $("#LastName").val(),
            Suffix: $("#Suffix").val(),
            NickName: $("#NickName").val(),
            Organization: $("#Organization").val(),
            Title: $("#Title").val(),
            Street1: $("#Street1").val(),
            AddressLine1: $("#AddressLine1").val(),
            City1: $("#City1").val(),
            StateProvinceRegion1: $("#StateProvinceRegion1").val(),
            ZIP1: $("#ZIP1").val(),
            Country1: $("#Country1").val(),
            Street2: $("#Street2").val(),
            AddressLine2: $("#AddressLine2").val(),
            City2: $("#City2").val(),
            StateProvinceRegion2: $("#StateProvinceRegion2").val(),
            ZIP2: $("#ZIP2").val(),
            Country2: $("#Country2").val(),
            PrimaryEmail: $("#PrimaryEmail").val(),
            SecondaryEmail: $("#SecondaryEmail").val(),
            SkypeAddress: $("#SkypeAddress").val(),
            TwitterAddress: $("#TwitterAddress").val(),
            LinkedIn: $("#LinkedIn").val(),
            Telephone1: $("#Telephone1").val(),
            Telephone2: $("#Telephone2").val(),
            Telephone3: $("#Telephone3").val(),
            publicprofile: $("#publicprofile").val(),

        };

        var objmentorquestionnary = {
            IndustryOther: JSON.stringify(IndustryOther),
            Startups: JSON.stringify(Startups),
            BusinessModel: JSON.stringify(BusinessModel),
            Funding: JSON.stringify(Funding),
            Software: JSON.stringify(Software),
            LifeSciences: JSON.stringify(LifeSciences),
            FunctionalAreas: JSON.stringify(FunctionalAreas),
            Others1: $("#Others1").val(),
            Others2: $("#Others2").val(),
            Others3: $("#Others3").val(),
        }
        
        var valid = false;
        if ($("#Captcha").val() == "") {
            alert("Please enter Captcha");
            $("#Captcha").focus();
            return false;
        }
        else {
            
            valid = ValidCaptcha();
            if (valid == true) {
                var c = confirm("Are you sure you want to Submit? ")

                if (c == true) {
                    $.ajax({
                        //url: "http://priyanet.us/MentoringAPI/Mentor/MentorDetails",
                        url: "http://localhost:62599/Mentor/MentorDetails",
                        type: "POST",
                        data: { objmentordetail: objmentordetail, objmentorquestionnary: objmentorquestionnary },
                        dataType: 'json',
                        async: false
                    }).done(function (resp) {
                        alert("Mentor " + resp);
                        window.location.replace("http://nexttech.digital-55.com");
                    }).error(function (err) {
                        alert("Error " + err.status);

                    });
                    return false;
                }
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

        $("#MiddleName").val("");
        $("#Prefix").val("");
        $("#Suffix").val("");
        $("#NickName").val("");
        $("#StateProvinceRegion1").val("");
        $("#Country1").val("");
        $("#Street2").val("");
        $("#AddressLine2").val("");
        $("#City2").val("");
        $("#ZIP2").val("");
        $("#Telephone3").val("");
        $("#StateProvinceRegion2").val("");
        $("#Country2").val("");
        $("#SecondaryEmail").val("");
        $("#SkypeAddress").val("");
        $("#TwitterAddress").val("");
        $("#publicprofile").val("");
        $("#Others1").val("");
        $("#Others2").val("");
        $("#Others3").val("");
        $('input:checkbox').prop('checked', false);

    });

});