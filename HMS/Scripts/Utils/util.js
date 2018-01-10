function postRequest(serverObj, url, successFunc) {
    $('#loadingGif').show();
    $.ajax({
        url: url,
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(serverObj),
        success: successFunc,
        error: function (data,dfsd,sdfsd) {
            $('#loadingGif').hide();
            toastr.error("Something bad happened");
        }
    });
}


function emailValidator(email) {
    var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
}

function numberValidator(number) {
    return $.isNumeric(number);
}

function phoneValidator(phoneNumber) {
    var phoneNumberPattern = /^([+]92)((3[\d]{2})([ ,\-,\/]){0,1}([\d, ]{6,9}))|(((0[\d]{1,4}))([ ,\-,\/]){0,1}([\d, ]{5,10}))$/;
    return phoneNumberPattern.test(phoneNumber);
}

function ValidateByClassName(className) {
    var requiredControls = $('.' + className);
    var counter = 0;
    for (var i = 0; i < requiredControls.length; i++) {
        var requiredField = $(requiredControls[i]);
        var fieldvalue = requiredField.val().trim();
        var fieldType = requiredField.attr('type');
        var isError = false;
        switch (fieldType) {
            case "email":
                isError = !emailValidator(requiredField.val());
                break;
        }
        if (fieldvalue === null || fieldvalue === undefined || (fieldvalue === "" || (fieldvalue.length >= requiredField.data('length'))) || isError) {
            counter++;
            requiredField.addClass('input-validation-error');
        } else {
            requiredField.removeClass('input-validation-error');
        }
    }
    if (counter > 0) {
        //implementing focus back to error
        var divId = $('.' + className + ".input-validation-error")[0].id;
        if ($("#" + divId).length > 0)
            $("#" + divId).focus();
        return false;
    }
    return true;
}

$(".numOnly").keypress(function (e) {
    //if the letter is not digit then display error and don't type anything
    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {      
        return false;
    }
});