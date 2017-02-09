


$(document).ready(function () {
    
  
    //function validatezip(zip) {
    //     var isValid = /^[0-9]{5}(?:-[0-9]{4})?$/;
    //     return isValid.test(zip)
    // }
    // $("#ZIP").on("blur", function () {
    //     if (!validatezip($("#ZIP"))) {
    //         alert("Invalid ZIP");
    //         $("#ZIP").focus();
    //          return false;
    //     }

    // });

    function ValidateEmail(email) {
        var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
        return expr.test(email);
    };
    $("#SecondaryEmail").blur( function () {
        if (!ValidateEmail($("#SecondaryEmail").val())) {
            alert("Invalid email address.");
            $("#SecondaryEmail").css('border','1px solid red');
            return false;
        }
        return false;
    });
    $("#PrimaryEmail").blur(function () {
        
        if (!ValidateEmail($("#PrimaryEmail").val())) {
            alert("Invalid email address.");
            $("#PrimaryEmail").css('border', '1px solid red');
            return false;
        }
        return false;
    });

    $("#PrimaryEmail").focusout(function () {
        
        $("#PrimaryEmail").css('border', '1px solid #d6d6d6');
    });
    $("#SecondaryEmail").focusout(function () {
        $("#SecondaryEmail").css('border', '1px solid #d6d6d6');
    });
    $('#PrimaryContact').keydown(function (e) {
        //PhoneMask(e, '#txtphone');

        var key = e.which || e.charCode || e.keyCode || 0;
        $phone = $(this);

        // Don't let them remove the starting '('
        if ($phone.val().length === 1 && (key === 8 || key === 46)) {
            $phone.val('(');
            return false;
        }
            // Reset if they highlight and type over first char.
        else if ($phone.val().charAt(0) !== '(') {
            $phone.val('(' + $phone.val());
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
    $('#SecondaryContact').keydown(function (e) {
        //PhoneMask(e, '#txtphone');

        var key = e.which || e.charCode || e.keyCode || 0;
        $phone = $(this);

        // Don't let them remove the starting '('
        if ($phone.val().length === 1 && (key === 8 || key === 46)) {
            $phone.val('(');
            return false;
        }
            // Reset if they highlight and type over first char.
        else if ($phone.val().charAt(0) !== '(') {
            $phone.val('(' + $phone.val());
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
    $('#CreatedDate').datepicker();
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
            alert("Primary contact is required");
            $("#PrimaryContact").focus();
            return false;
        }
        else if ($("#PrimaryEmail").val() == "") {
            alert("Primary email name is required");
            return false;
        }
        else if (!ValidateEmail($("#PrimaryEmail").val())) {
            alert("Invalid email address.");
            return false;
        }

        else {
            var LegalEntityAtEnrollment = ($("#LegalEntityAtEnrollment").is(':checked')) ? true : false;
            var IndustryCategoryName = $("#IndustryCategoryName :selected").text();
            var objVenture = {
                VentureName: $("#VentureName").val(), PrimaryContact: $("#PrimaryContact").val(), PrimaryEmail: $("#PrimaryEmail").val(),
                SecondaryContact: $("#SecondaryContact").val(),
                SecondaryEmail: $("#SecondaryEmail").val(),
                CreatedDate: $("#CreatedDate").val(),
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
            $.each($("input[name='IndustryCategoryName']:checked"), function () {
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
                PrimaryName: $("#PrimaryName").val(),
                PrimaryRoleInVenture: $("#PrimaryRoleInVenture").val(),
                PrimaryColaborationAffiliation: JSON.stringify(Pcollaboration),
                SecondaryName: $("#SecondaryName").val(),
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
                WhatTypeOfVMSHelpStartUpNeeds: JSON.stringify(VMShelp),
                Comments: $("#Comments").val()
            };
            $.ajax({
                url: "http://localhost:64713/api/VentureDetails/",
                type: "POST",
                data: { venturedetail: venturedetail, objVenture: objVenture },
                dataType: 'json',
                async: false
            }).done(function (resp) {
                alert("Successful " + resp);
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
    });
});
window.myCallback = function (data) {
    alert(JSON.stringify(data));
    alert('hi');
};

