function Validate(objForm) {
    
    var ArrayValidated = new Array();

    for (i = 0; i < objForm.elements.length; i++) {
     
        var element = objForm.elements[i];
        var EleName = element.id;
        // alert(EleName);

        if ((!EleName) || (EleName.length == 0) || (ArrayValidated[EleName]))
            continue;

        ArrayValidated[EleName] = true;
        var ValidationType = element.getAttribute("validate");

        if ((!ValidationType) || (ValidationType.length == 0))
            ValidationType = "No Validation";
        var StrMessages = element.getAttribute("Message");

        if (!StrMessages)
            StrMessages = "";

        var arrayMessages = StrMessages.split(",");
        var arrayValidationTypes = ValidationType.split(",");

        for (var j = 0; j < arrayValidationTypes.length; j++) {
     
            var CurrentValidationType = arrayValidationTypes[j];
            var booleanValid = true;

            switch (CurrentValidationType) {
                case "Required":
                    booleanValid = ValidateRequired(element);
                    break;

                case "integer":
                    booleanValid = ValidateInteger(element);
                    break;
                case "number":
                    booleanValid = ValidateNumber(element);
                    break;
                case "Email":
                    booleanValid = ValidateEmail(element);
                    break;
                case "phone":
                    booleanValid = ValidatePhone(element);
                    break;
                case "dropdown":
                    booleanValid = Validatedropdown(element);
                    break;
                case "ZipCode":
                    booleanValid = ValidatedZipCode(element);
                    break;
                case "Checkbox":
                    booleanValid = ValidatedCheckbox(element);
                    break;
                case "Website":
                    booleanValid = ValidateWebsite(element);
                    break;
                case "No Validation":
                    booleanValid = validateNonValidateAttributes(element);
                    break;
                default:
                    try {
                        booleanValid = eval(CurrentValidationType + "(element)");
                    }
                    catch (ex) {
                        booleanValid = true;
                    }
            }

            if (booleanValid == false) {

                var Message;
                if (CurrentValidationType == "Required") {
                    Message = "Please Enter " + $(element.attributes["data-val-required"]).val();
                }
                if (CurrentValidationType == "dropdown") {
                    Message = "Please Select " + $(element.attributes["data-val-required"]).val();
                }
                if (CurrentValidationType == "Email") {
                    Message = "Please Enter Correct Email Format";
                    
                }

                if (CurrentValidationType == "phone") {
                    Message = "Invalid phone Number";
                }

                if (CurrentValidationType == "ZipCode") {
                    Message = "Please enter your 5 digit or 5 digit+4 zip code in  this format '12345-6789'";
                }
                if ((j < arrayMessages) && (arrayMessages[j].length > 0))
                    Message = arrayMessages[j];

                InsertError(element, Message);//Insert Error is a function to insert Error messages.
                if ((typeof element.focus() == "function") || element.focus) {
                    element.focus();
                    element.style.borderColor = "red";
                    element.style.borderStyle = "solid";
                }
                return false;
            }
            else {
            
                ClearError(element);//Function used to clear errors.
            }

        }

    }
    return booleanValid;
}



function InsertError(element, strmessage) {
    
    //Function for InsertError.
    if (element.form.getAttribute("Show Alert") && (element.form.getAttribute("Show Alert") != "0")) {
       // alert(strmessage);
        return false;
    }
    var StrSpanId = element.id + "Val_Error";
    var objspan = document.getElementById(StrSpanId);
    if (!objspan) {

        if ((element.type == "checkbox") || (element.type == "radio")) {
            for (var i = 0; i < element.form.elements.length; i++) {
                if (element.form.elements[i] == element.name) {
                    element = element.form.elements[i];
                }
            }
        }
        objspan = document.getElementById("ErrorMessages");
        //objspan.id = strSpanID;
        //objspan.className ='validation_error';
    //    var nodeAfter=0;
    //    var nodeParent = element.parentNode;
    //    for (var i=0; i<nodeParent.childNodes.length; i++) {
    //        if (nodeParent.childNodes[i] == element) {
    //            if (i < (nodeParent.childNodes.length-1))
    //                nodeAfter = nodeParent.childNodes[i+1];
    //            break;
    //        }
    //    }
    //    if ((!nodeAfter)&&(nodeParent.parentNode)) {
    //        nodeParent = nodeParent.parentNode;
    //        for (var i=0; i<nodeParent.childNodes.length; i++) {
    //            if (nodeParent.childNodes[i] == element.parentNode) {
    //                if (i < (nodeParent.childNodes.length-1))
    //                    nodeAfter = nodeParent.childNodes[i+1];
    //                break;
    //            }
    //        }
    //    }
    //    if (nodeAfter)
    //        nodeParent.insertBefore(objSpan, nodeAfter);
    //    else
    //        document.body.appendChild(objSpan);
    //}
    //objSpan.innerHTML = strMessage;

        
        objspan.innerHTML = strmessage;
        $("#errorsymbol").show();
        $("#ErrorMessages").show();
        //objspan;
    }
}

//Function For Clearing Errors,ClearError.
function ClearError(element) {


    var StrSpanId = element.name + "Val_Error";
    var objSpan = document.getElementById(StrSpanId);
    if (objSpan) {
        objSpan.innerHTML = "";

    }
    $("#errorsymbol").hide();
    $("#ErrorMessages").hide();
    element.removeAttribute("style");
}

function validateNonValidateAttributes(objElement) {
    var value = GetElementValue(objElement);
    
    var blnresut = true;
    return blnresut;
}

//Function to validate Empty or Not

function ValidateRequired(objElement) {

    var Strvalue = GetElementValue(objElement);
    var blnResult = true;
    if (Alltrim(Strvalue) == "") { //It Checks for nothing.
        blnResult = false;
    }
    return blnResult;
}

//function to validate Email
function ValidateEmail(objElement) {
    
    var StrEmail = GetElementValue(objElement);// In this first we check @, and then period after @ and text in between.

    var in_space = StrEmail.indexOf(" ");
    var Isvalid = true;
    if (in_space != -1) {
        Isvalid = false;
    }

    var strlen = StrEmail.length;
    var Alpha = StrEmail.indexOf("@");
    var lastAlpha = StrEmail.lastIndexOf("@");

    if (Alpha != lastAlpha) {
        Isvalid = false;
    }
    //Checking the position of @,whether it is in the firstposition  or the email length is less than 6.
    if (Alpha == -1 || Alpha == 0 || strlen < 6) {
        Isvalid = false;
    }

    var last_point = StrEmail.lastIndexOf(".");

    // Be sure period at least two spaces after @, but not last char.
    if (last_point - Alpha < 2 || last_point == (strlen - 1)) {
        Isvalid = false;
    }
    return Isvalid;
}

//function to validate PhoneNumber
function ValidatePhone(objElement) {
 
    //declaring of non digit phonenumbers which are allowed in phone Numbers
    var phoneNumberDelimiters = "()-";

    //characters that are allowed in International phone numbers.
    var ValidWorldphonechars = phoneNumberDelimiters + "+";

    //minimum digits for a phone number
    var minimumDigitsForPhone = 10;

    var strphonenum = GetElementValue(objElement);

    s = stripCharsInBag(strphonenum, ValidWorldphonechars);
    s = trim(s);
    return (isInteger(s) && s.length >= minimumDigitsForPhone);

}

function trim(s) {
    var i;
    var returnString = "";
    // Search through string's characters one by one.
    // If character is not a whitespace, append to returnString.
    for (i = 0; i < s.length; i++) {
        // Check that current character isn't whitespace.
        var c = s.charAt(i);
        if (c != " ") returnString += c;
    }
    return returnString;
}

function isInteger(s) {

    var i;
    for (i = 0; i < s.length; i++) {
        // Check that current character is number.
        var c = s.charAt(i);
        if (((c < "0") || (c > "9"))) return false;
    }
    // All characters are numbers.
    return true;
}





//function to validate whether integer or not
function ValidateInteger(objElement) {
    //Check for valid Numeric strings.
 
    var StrString = GetElementValue(objElement);
    var StrValidChars = "0123456789";//decimal 
    var StrChar;
    var blnResult = true;

    //Checking that strstring contains the values listed as the above.
    for (i = 0; i < StrString.length && blnResult == true; i++) {
        StrChar = StrString.charAt[i];

        if (StrValidChars.indexOf(StrChar) == -1) {
            blnResult == false;
        }
    }
    return blnResult;
}

//function to validate number.
function ValidateNumber(objElement) {

    //Checking for validating number strings

    var Strvalue = GetElementValue(objElement);
    var StrValidChars = ".0123456789";//decimal 
    var StrChar;
    var blnResult = true;
    var StrChar;
    var blnResult = true;

    //Checking that strstring contains the values listed as the above.
    for (i = 0; i < Strvalue.length && blnResult == true; i++) {
        StrChar = Strvalue.charAt[i];

        if (StrValidChars.indexOf(StrChar) == -1) {
            blnResult == false;
        }
    }
    return blnResult;

}

//function to validate dropdowns
function Validatedropdown(objElement) {
 
    var strdrpvalue = GetElementValue(objElement);
    var blnResult = true;

    if (strdrpvalue=="")
    {
        strdrpvalue = -1;
    }
    
  
    if(strdrpvalue == -1 || strdrpvalue == "AA" ||strdrpvalue=="Select") 
    {
        return blnResult=false;
    }
          
  else{
        return blnResult=true;
     }
    return blnResult;
}

function ValidatedZipCode(objElement) {

    var valid = "0123456789-";
    var hyphencount = 0;
    var name = GetElementValue(objElement);
    var isvalid = true;
    if (name.length != 5 && name.length != 10) {

        isvalid = false;
    }
    for (var i = 0; i < name.length; i++) {
    
        temp = "" + name.substring(i, i + 1);
        if (temp == "-") hyphencount++;
        if (valid.indexOf(temp) == "-1") {
            //alert("Invalid characters in your zip code.  Please try again.");
            isvalid = false;
        }
        if ((hyphencount > 1) || ((name.length == 10) && "" + name.charAt(5) != "-")) {
            //alert("The hyphen character should be used with a properly formatted 5 digit+four zip code, like '12345-6789'.");
            isvalid = false;
        }
    }
    return isvalid;
}

function ValidatedCheckbox(objElement) {

    var chkvalue = GetElementValue(objElement);
    
    if (objElement.id == "chkisAllowsedingSMS")
    {
        if ($('input[type=checkbox]').is(':checked') == true)
        {
            $("#txtSMTPEmail").attr("validate", "Required,Email");
            $("#txtSMTPPassword").attr("validate", "Required");
            $("#txtSMTPAddress").attr("validate", "Required");
            $("#txtSMTPPort").attr("validate", "Required");
            return true;
        }
       
    }

    
    else
    {
        return false;
    }
   
}




function GetElementValue(objElement) {

    var result = "";
    switch (objElement.type) {

        case "text":
        case "hidden":
        case "textarea":
        case "password":
        case "email":
            result = objElement.value;
            break;
        case "url":
            result = objElement.value;
            break;
        case "select-one":
        case "select":
           
            if (objElement.selectedIndex >= 0)
                result = objElement.options[objElement.selectedIndex].value;
            break;
        case "checkbox":
        case "radio":
            for (var i = 0; i < objElement.form.elements.length; i++) {
                if (objElement.form.elements[i].name == objElement.name) {
                    if (objElement.form.elements[i].checked) {
                        result += objElement.form.elements[i].value + ",";
                    }
                }
            }
            break;
    }
    return result;
}



function Alltrim(stringvalue) {

    var lDone = false;
    while (lDone == false) {
        if (stringvalue.length == 0) { return stringvalue; }
        if (stringvalue.indexOf('') != 0) { stringvalue = stringvalue.substring(1); lDone = false; continue; }
        else { lDone = true; }
        if (stringvalue.lastIndexOf('') == stringvalue.length - 1) { stringvalue = stringvalue.substring(0, stringvalue.length - 1); lDone = false; continue; }
        else { lDone = true; }
    }
    return stringvalue;
}



function stripCharsInBag(s, bag) {
 
    var i = "";

    var returnstring = "";

    for (i = 0; i < s.length; i++) {

        var charcter = s.charAt(i);

        if (bag.indexOf(charcter) == -1)
            returnstring += charcter;
    }

    return returnstring;
}

function validateNumerics(event) {

    // Get the ASCII value of the key that the user entered
    var key = event.keyCode;
    // Verify if the key entered was a numeric character (0-9) or a decimal (.) or BackSpace
    if ((key > 47 && key < 58) || key == 46 || key == 8)
        // If it was, then allow the entry to continue
        return true;
    else
        // If it was not, then dispose the key and continue with entry
        //                event.returnValue = null; 
        return false;
}

function PhoneFormat(event, control) {
 
    var result = false;
    if (validateNumerics(event)) {
        
        if (control.value.length == 3)
            control.value = "(" + control.value + ") ";
        if (control.value.length == 5)
            control.value = control.value + " ";
        if (control.value.length == 9)
            control.value = control.value + '-';
        if (control.value.length > 14)
            result = false;
    }
    else
        result = false;

    return result

}

function ValidateWebsite(objElement) {
    
    var isvalid = false;
    var Strurl = GetElementValue(objElement);
    var re = /^(http|https):\/\/(([a-zA-Z0-9$\-_.+!*'(),;:&=]|%[0-9a-fA-F]{2})+@)?(((25[0-5]|2[0-4][0-9]|[0-1][0-9][0-9]|[1-9][0-9]|[0-9])(\.(25[0-5]|2[0-4][0-9]|[0-1][0-9][0-9]|[1-9][0-9]|[0-9])){3})|localhost|([a-zA-Z0-9\-\u00C0-\u017F]+\.)+([a-zA-Z]{2,}))(:[0-9]+)?(\/(([a-zA-Z0-9$\-_.+!*'(),;:@&=]|%[0-9a-fA-F]{2})*(\/([a-zA-Z0-9$\-_.+!*'(),;:@&=]|%[0-9a-fA-F]{2})*)*)?(\?([a-zA-Z0-9$\-_.+!*'(),;:@&=\/?]|%[0-9a-fA-F]{2})*)?(\#([a-zA-Z0-9$\-_.+!*'(),;:@&=\/?]|%[0-9a-fA-F]{2})*)?)?$/;
    if (!re.test(Strurl)) {
     
        isvalid= false;
    } else {
        isvalid= true;
    }
    return isvalid;
}


         




