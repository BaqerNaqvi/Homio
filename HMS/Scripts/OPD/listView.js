$(document).ready(function() {
    $("#createNewSlipBtn").click(function () {
        if (window.location.href.toLowerCase().indexOf("home") > 0) {
            window.location.href = "OpdpForms";
        } else {
            window.location.href = "home/OpdpForms";
        }
       
    });
});