function varticalCenterStuff() {
    var windowHeight = $(window).height();
    var loginBoxHeight = $('.login-box').innerHeight();
    var welcomeTextHeight = $('.welcome-text').innerHeight();
    var loggedIn = $('.logged-in').innerHeight();

    var mathLogin = (windowHeight / 2) - (loginBoxHeight / 2);
    var mathWelcomeText = (windowHeight / 2) - (welcomeTextHeight / 2);
    var mathLoggedIn = (windowHeight / 2) - (loggedIn / 2);
    $('.login-box').css('margin-top', mathLogin);
    $('.welcome-text').css('margin-top', mathWelcomeText);
    $('.logged-in').css('margin-top', mathLoggedIn);
}
$(window).resize(function () {
    varticalCenterStuff();
});
varticalCenterStuff();

// awesomeness
$(document).ready(function ()
{ 

$('.btn-login').click(function () {
    $(this).parent().fadeOut(function () {
        $('#login-box').fadeIn();
    })
});

//$('.btn-cancel-action').click(function (e) {
//    e.preventDefault();
//    $(this).parent().parent().parent().parent().fadeOut(function () {
//       $('.welcome-text').fadeIn();
//    })
//});

$('.btn-login-submit').click(function (e) {
   
    

    var element = $(this).parent().parent().parent().parent();

    var usernameEmail = $('#username_id').val();
    var password = $('#password_id').val();
    var clintId = $('#Client_id').val();
   var  Isvalid = true;
    if (usernameEmail == '' || password == ''|| clintId=='') {
        
        // wigle and show notification
        // Wigle
        var element = $(this).parent().parent().parent().parent();
        $(element).addClass('animated rubberBand');
        $(element).one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
            $(element).removeClass('animated rubberBand');
        });

        // Notification
        // reset
        $('.notification.login-alert').removeClass('bounceOutRight notification-show animated bounceInRight');
        // show notification
        $('.notification.login-alert').addClass('notification-show animated bounceInRight');

        $('.notification.login-alert').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
            setTimeout(function () {
                $('.notification.login-alert').addClass('animated bounceOutRight');
            }, 2000);
        });
        Isvalid = false;
    } else {
     
        $(element).fadeOut(function () {
            
            // $('.logged-in').fadeIn();
            Isvalid = true;
        });
    }//endif
    return Isvalid;
});


$('.btn-logout').click(function () {
    $('.logged-in').fadeOut(function () {
        $('.welcome-text').fadeIn();
        // Notification
        $('.notification.logged-out').removeClass('bounceOutRight notification-show animated bounceInRight');
        // show notification
        $('.notification.logged-out').addClass('notification-show animated bounceInRight');

        $('.notification.logged-out').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
            setTimeout(function () {
                $('.notification.logged-out').addClass('animated bounceOutRight');
            }, 2000);
        });

    });
});
});