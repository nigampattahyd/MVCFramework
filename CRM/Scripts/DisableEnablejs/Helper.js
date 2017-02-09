//locking all the grids and tables except buttons



function disableForm(theform) {
    
    var divids = document.getElementsByClassName("content-box1")
  
    for (var j = 0; j < divids.length; j++)
    {
        if (divids[j].id != "") {
            var nodes = document.getElementById(divids[j].id).getElementsByTagName('*');
            var textinp = nodes[0].getElementsByTagName('input');
            var selinp = nodes[0].getElementsByTagName('select');

            for (var i = 0; i < textinp.length; i++) {
                //inputcon[i].readonly = true;
                if (textinp[i].id != "") {
                 //  $('#' + textinp[i].id).attr('readOnly', 'readOnly');
                    textinp[i].disabled = true;
                }

            }
            for (var i = 0; i < selinp.length; i++) {
                //inputcon[i].readonly = true;
                if (selinp[i].id != "") {
                    selinp[i].disabled = true;
                }

            }
        }
        else {
            var divclass = divids[j];
            var nodes = divclass.getElementsByTagName('table');
            var textinp = nodes[0].getElementsByTagName('input');
            var selinp = nodes[0].getElementsByTagName('select');
            
            for (var i = 0; i < textinp.length; i++) {
                //inputcon[i].readonly = true;
                if (textinp[i].id != "")
                {
                  $('#' + textinp[i].id).attr('readOnly', 'readOnly');
                    textinp[i].disabled = true;
                }
                
            }
            for (var i = 0; i < selinp.length; i++) {
                //inputcon[i].readonly = true;
                if (selinp[i].id != "") {
                    selinp[i].disabled = true;
                }

            }
        }

    }
    
   
}

function enableForm(theform) {
    
    var divids = document.getElementsByClassName("content-box1")

    for (var j = 0; j < divids.length; j++) {
        if (divids[j].id != "") {
            var nodes = document.getElementById(divids[j].id).getElementsByTagName('*');
            var textinp = nodes[0].getElementsByTagName('input');
            var selinp = nodes[0].getElementsByTagName('select');

            for (var i = 0; i < textinp.length; i++) {
                //inputcon[i].readonly = true;
                if (textinp[i].id != "") {
                    // $('#' + textinp[i].id).attr('readOnly', false);
                    textinp[i].disabled = false;
                }

            }
            for (var i = 0; i < selinp.length; i++) {
                //inputcon[i].readonly = true;
                if (selinp[i].id != "") {
                    selinp[i].disabled = false;
                }

            }
        }
        else {
            var divclass = divids[j];
            var nodes = divclass.getElementsByTagName('table');
            var textinp = nodes[0].getElementsByTagName('input');
            var selinp = nodes[0].getElementsByTagName('select');

            for (var i = 0; i < textinp.length; i++) {
                //inputcon[i].readonly = true;
                if (textinp[i].id != "") {
                    // $('#' + textinp[i].id).attr('readOnly', false);
                    textinp[i].disabled = false;
                }

            }
            for (var i = 0; i < selinp.length; i++) {
                //inputcon[i].readonly = true;
                if (selinp[i].id != "") {
                    selinp[i].disabled = false;
                }

            }
        }

    }


}

