$('.toggle').on('click', function() {
  $('.container').stop().addClass('active');
});

$('.close').on('click', function() {
  $('.container').stop().removeClass('active');
});

$("#password").on('keyup', function (e) {
    if (e.keyCode === 13) {        
        login();
    }
});


$("#loginBtn").click(function () {
    login();
});

function login() {

    var usaername = $("#username").val();
    var password = $("#password").val();

    if (usaername === "" || password === "") {
        toastr.error("Please enter valid credentials");
        return;
    }
    $(".loading-wheel").show();

    var d = new Date();
    var timezoneOffset = d.getTimezoneOffset();

    var server = {};
    server.Email = usaername;
    server.Password = password;
    server.TimeZoonOfset = timezoneOffset;


    postRequest(server, "login", function (data) {
        if (data.Success) {
            window.location.href = "../home/index";
        }
        else {
            $(".loading-wheel").hide();
            toastr.error("Invalid username / password");
        };
    });

}








/////////////////////////////////
//////////////////////////  FORGOT PASSWORD

$("#resetPassBtn").click(function () {
    
    if (!ValidateByClassName("forgotemailfield")) {
        toastr.error("Invalid email");
        return;
    }
    $(".loading-wheel").show();
    var email = $("#emailData").val();
    postRequest( { Email: email }, "ForgotPassword", function (data) {
        $(".loading-wheel").hide();
        if (data.Success) {
            toastr.success(data.Message);
        }
        else {
            toastr.warning(data.Message);
        };
    });
});