
$(document).ready(function () {
    var currentdate = new Date();
    var month = (1 + currentdate.getMonth()).toString();
    month = month.length > 1 ? month : '0' + month;
    var twoDigitDate = currentdate.getDate() + ""; 
    if (twoDigitDate.length == 1) twoDigitDate = "0" + twoDigitDate;
    var currentDate = month + "/" + twoDigitDate + "/" + currentdate.getFullYear();
    $('#CreatedDate').val(currentDate);

    //function validatezip(zip) {
    //     var isValid = /^[0-9]{5}(?:-[0-9]{4})?$/;
    //     return isValid.test(zip)
    // }
    // $("#ZIP").blur(function () {
    //     if (!validatezip($("#ZIP"))) {
    //         alert("Invalid ZIP");
    //         $("#ZIP").focus();
    //          return false;
    //     }

    // });
    $("#ZIP").keydown(function (e) {
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
   
    function ValidateEmail(email) {
        var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
        return expr.test(email);
    };
    $("#SecondaryEmail").blur(function () {
        if ($("#SecondaryEmail").val() != "") {
            if (!ValidateEmail($("#SecondaryEmail").val())) {
                alert("Invalid email address.");
                $("#SecondaryEmail").css('border', '1px solid red');
                return false;
            }
            else if ($("#PrimaryEmail").val() == $("#SecondaryEmail").val()) {
                alert("Primary Email and Secondary Email should Not be Same");
                $("#SecondaryEmail").css('border', '1px solid red');
                $("#SecondaryEmail").val("");
                return false;
            }
            return false;
        }
        
    });
    $("#PrimaryEmail").blur(function () {
        if ($("#PrimaryEmail").val() != "") {
            if (!ValidateEmail($("#PrimaryEmail").val())) {
                alert("Invalid email address.");
                return false;
                $("#PrimaryEmail").css('border', '1px solid red');
                $("#PrimaryEmail").val("");
              }
            return false;
        }
    });



    $("#PrimaryEmail").focusout(function () {

        $("#PrimaryEmail").css('border', '1px solid #d6d6d6');
    });
    $("#SecondaryEmail").focusout(function () {
        $("#SecondaryEmail").css('border', '1px solid #d6d6d6');
    });
    $('#PrimaryPhoneNo').keydown(function (e) {
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
    $('#SecondaryPhoneNo').keydown(function (e) {
        
        //PhoneMask(e, '#txtphone');

        var key = e.which || e.charCode || e.keyCode || 0;
        $phone = $(this);

        // Don't let them remove the starting '('
        //if ($phone.val().length === 1 && (key === 8 || key === 46)) {
        //    $phone.val('(');
        //    return false;
        //}
        // Reset if they highlight and type over first char.
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
                (key >= 96 && key <= 105));
    });
    //var venturedetails =JSON.stringify(VentureViewModel);
    //var dbDate = "2012-03-06";
    //var date2 = new Date(dbDate);
    var currentdate=new Date();
    $('#CreatedDate').datepicker({
        dateFormat: 'mm/dd/yy',
        minDate: currentdate
    }).datepicker({ defaultDate: new Date() });
    $("#btnsubmit").click(function () {

        if ($("#CreatedDate").val() == "") {
            alert("Created Date is required");
            $("#CreatedDate").focus();
            return false;
        }
        else if ($("#VentureName").val() == "") {
            alert("Venture name is required");
            $("#VentureName").focus();
            return false;
        }
        else if ($("#PrimaryContact").val() == "") {
            alert("Primary Name is required");
            $("#PrimaryContact").focus();
        }
        else if ($("#PrimaryEmail").val() == "") {
            alert("Primary Email is required");
            $("#PrimaryEmail").focus();
            return false;
        }
        else if (!ValidateEmail($("#PrimaryEmail").val())) {
            alert("Invalid Email address.");
            return false;
        }
        else if ($("#PrimaryPhoneNo").val() == "") {
            alert("Primary PhoneNo is required");
            $("#PrimaryPhoneNo").focus();
            return false;
        }
    
        else {
            var LegalEntityAtEnrollment = ($("#LegalEntityAtEnrollment").is(':checked')) ? true : false;
            var IndustryCategoryName = $("#IndustryCategoryName :selected").val();
            var objVenture = {
                VentureName: $("#VentureName").val(), PrimaryContact: $("#PrimaryContact").val(), PrimaryEmail: $("#PrimaryEmail").val(),
                SecondaryContact: $("#SecondaryContact").val(),
                SecondaryEmail: $("#SecondaryEmail").val(),
                CreatedDate: $("#CreatedDate").val(),
                Comments: $("#Comments").val(),
                LegalEntityAtEnrollment: JSON.stringify(LegalEntityAtEnrollment)
            }
            var Pcollaboration = [];
            $.each($("input[name='PrimaryColaborationAffiliation']:checked"), function () {
                Pcollaboration.push($(this).val());
            });

            var Scollaboration = [];
            $.each($("input[name='ScondaryColaborationAffiliation']:checked"), function () {
                Scollaboration.push($(this).val());
            });

            var advertisement = [];
            $.each($("input[name='Advertisement']:checked"), function () {
                advertisement.push($(this).val());
            });

            var currentstatus = [];
            $.each($("input[name='WhatIsYourCurrentStatus']:checked"), function () {
                currentstatus.push($(this).val());
            });
            var VMShelp = [];
            $.each($("input[name='WhatTypeOfVMSHelpStartUpNeeds']:checked"), function () {
                VMShelp.push($(this).val());
            });

            var venturedetail = {
                VentureDetailsId: "0",
                Street: $("#Street").val(),
                City: $("#City").val(),
                State: $("#State").val(),
                ZIP: $("#ZIP").val(),
                Advertisement: JSON.stringify(advertisement),
                IndustryCategoryName: IndustryCategoryName,
                CategoryOthers: $("#CategoryOthers").val(),
                PrimaryPhoneNo: $("#PrimaryPhoneNo").val(),
                PrimaryRoleInVenture: $("#PrimaryRoleInVenture").val(),
                PrimaryColaborationAffiliation: JSON.stringify(Pcollaboration),
                SecondaryPhoneNo: $("#SecondaryPhoneNo").val(),
                SecondaryRoleInVenture: $("#SecondaryRoleInVenture").val(),
                ScondaryColaborationAffiliation: JSON.stringify(Scollaboration),
                WouldLikeToElaborateSpecifics: $("#WouldLikeToElaborateSpecifics").val(),
                WhatDoYouDo: $("#WhatDoYouDo").val(),
                WhatIsTheProblem: $("#WhatIsTheProblem").val(),
                WhoHasIt: $("#WhoHasIt").val(),
                HowWillUSolveTheProblem: $("#HowWillUSolveTheProblem").val(),
                HowWillUMakeMoney: $("#HowWillUMakeMoney").val(),
                WhtIsTheSecretSauce: $("#WhtIsTheSecretSauce").val(),
                IsThereIP: $("#IsThereIP").val(),
                WhatIsYourCurrentStatus: JSON.stringify(currentstatus),
                WhatTypeOfVMSHelpStartUpNeeds: JSON.stringify(VMShelp)

            };
            var c = confirm("Are you sure you want to Submit? ")
           
            if (c==true) {
                $.ajax({
                    url: "http://priyanet.us/MentoringAPI/Venture/submit",
                   // url: "http://localhost:62599/Venture/submit",
                    type: "POST",
                    data: { venturedetail: venturedetail, objVenture: objVenture },
                    dataType: 'json',
                    async: false
                }).done(function (resp) {
                    alert("Venture " + resp);
                    top.window.location.href = "http://nexttech.digital-55.com/";
                }).error(function (err) {
                    alert("Error " + err.status);
                   
                    //jsonp: 'callback',
                    //jsonpCallback: 'myCallback'
                    //success: function (result) {
                    //    //alert("Record added successfully");
                    //    alert(result.d);
                    //},
                    //error: function (result) {
                    //    
                    //    // alert("An error occurred:" + status);
                    //    //xhr,status
                    //    alert(result.d);
                    //}
                });
            }
        }
    });
    $("#btnclear").click(function () {
        $("#SecondaryEmail").val("")
        $("#PrimaryEmail").val("")
        $("#PrimaryPhoneNo").val("")
        $("#SecondaryPhoneNo").val("") 
        $("#VentureName").val("")
        $("#Street").val("") 
        $("#City").val("")
        $("#State").val("")
        $("#ZIP").val("")
        $("#IndustryCategoryName").val("")
        $("#CategoryOthers").val("")
        $("#PrimaryContact").val("")
        $("#PrimaryRoleInVenture").val("")
        $("#SecondaryContact").val("")
        $("#SecondaryRoleInVenture").val("")
        $("#WouldLikeToElaborateSpecifics").val("")
        $("#LegalEntityAtEnrollment").val("")
        $("#WhatDoYouDo").val("");
        $("#WhatIsTheProblem").val("")
        $("#WhoHasIt").val("")
        $("#HowWillUSolveTheProblem").val("")
        $("#HowWillUMakeMoney").val("")
        $("#WhtIsTheSecretSauce").val("")
        $("#IsThereIP").val("")
        $("#Comments").val("") 
        $('input:checkbox').prop('checked', false);

    });
});
window.myCallback = function (data) {
    alert(JSON.stringify(data));
    alert('hi');
};



