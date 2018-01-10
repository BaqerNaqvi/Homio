// LIST VIEW

$("#docsList").addClass('active');
$(".docStatus").click(function () {
    $(".loading-docs").show();
    var checkBoxId = $(this).attr("Id");
    var server = {};
    server.Id = checkBoxId.split("_")[2]; // doc id 
    server.Status = $("#" + checkBoxId).prop("checked");
    postRequest(server, "ChangeStatus", function() {
        toastr.success("Status changed successfully");
        $(".loading-docs").hide();
    });
});
$("#createNewDocBtn").click(function () {
    window.location.href = "AddNewDoc";
});
$(".editDoc").click(function () {
    var checkBoxId = $(this).attr("Id");
    var id = checkBoxId.split("_")[1]; // doc id  
    window.location.href = "AddNewDoc?docId=" + id;
});



// NEW
$("#addNewDoc").click(function() {
    var fname = $("#fname").val();
    var lname = $("#lname").val();
    var email = $("#email").val();
    var fee = $("#fee").val();

    var title = $("#title").val();
    var degree = $("#degree").val();
    var shiftFrom = $("#shiftHoursFrom").val();
    var shiftTo = $("#shiftHoursTo").val();
    var shiftDays = $("#docs-days").val();

    var PMDCNo = $("#PMDCNo").val();

    if (!ValidateByClassName('docField')) {
        toastr.error("Please add valid data");
        return;
    }
   


    if (!numberValidator(fee)) {
        toastr.error("Fee must be valid digits");
        return;
    }

    if (PMDCNo==="") {
        toastr.error("Please add PMDC No.");
        return;
    }
    $(".loading-docs").show();
    var server = {};
    server.FirstName = fname;
    server.LastName = lname;
    server.Email = email;
    server.Fee = fee;
    server.Degree = degree;
    server.Title = title;
    server.PMDCNo = PMDCNo;

    server.ShiftFrom = shiftFrom;
    server.ShifTo = shiftTo;
    server.ShiftDays = shiftDays.length>0 ? shiftDays.join(','):null;


    server.Id = $("#docId").val();

    postRequest(server, "AddNewDocAjax", function (response) {
        if (response !== null && response !== undefined) {
            if (response.isSuccess) {
                toastr.success(response.data[0]);
                var obj = response.doc;
                $(".form-control").val("");

            } else {
                for (var i = 0; i < response.data.length; i++) {
                    toastr.error(response.data[i]);
                }
            }
            $(".loading-docs").hide();
        } // if ends here
    });
});
$("#backToDocsList").click(function () {
    $(".loading-docs").show();
    window.location.href = "ListOfDocs";
});

$(document).ready(function() {
    $('#docs-days').multiselect({
        includeSelectAllOption: true
    });
    if (selectedDays !== null && selectedDays.length > 0) {
        var days = selectedDays.split(',');
        $('#docs-days').val(days);
        $("#docs-days").multiselect("refresh");
    }

});