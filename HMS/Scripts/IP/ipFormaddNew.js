

$("#wardDd").change(function () {
    var selectedDocObj = $("#wardDd option:selected");
    var wardId = $(selectedDocObj).val();
    var result = $.grep(allWards, function (e) { return e.Parent == wardId; });
    $("#roomDd").html("");
    if (result != undefined && result.length > 0) {
        $("#roomDd").empty();
        $("#roomDd").append("<option value='0'>Select Bed</option>");
        for (var i = 0; i < result.length; i++) {
            var op = "<option value='" + result[i].Id + "'>" + result[i].Name + " </option>";
            $("#roomDd").append(op);
        }
    }
});

$(document).ready(function () {
    var stat = mStatus === true ? 1 : 0;
    var gen1 = gen === true ? 1 : 0;


    $("#cnicNo").mask("99999-9999999-9");
    $("#phNo").mask("0399-9999999");

    $('#BloodGroupDd').val(bGroup);
    $('#mstatus').val(stat);
    $('#gender').val(gen1);
    $("#ipForms").addClass('active');

});


$("#createNewIpFormBtn").click(function () {

  

    var wardDd = $("#wardDd option:selected").val();
    if (wardDd === "0") {
        toastr.error("Please select ward");
        return;
    }

    var roomDd = $("#roomDd option:selected").val();
    if (roomDd === "0" || roomDd==undefined) {
        toastr.error("Please select bed");
        return;
    }



    var docDd = $("#docDd option:selected").val();
    if (docDd === "0" || docDd=="") {
        toastr.error("Please select doctor");
        return;
    }

    if (!ValidateByClassName("opdRequired")) {
        toastr.error("Please add mandatory data");
        return;
    }
   

    var age = $("#age").val().trim();
    if (age === "" || !numberValidator(age)) {
        toastr.error("Age must be valid digits");
        return;
    }

    var adminFee = $("#adminFee").val().trim();
    if (adminFee === "" || !numberValidator(adminFee)) {
        toastr.error("Admission Fee should be valid digits");
        return;
    }

    var phone = $("#phNo").val().trim();

    var server = {};

    server.PatientNo = $('#patId').val();
    server.Name = $("#fullname").val();
    server.GuardianName = $("#gname").val();
    server.Age = age;
    server.Gender = $("#gender").val();
    server.CNIC = $("#cnicNo").val();
    server.Address = $("#address").val();
    server.Phone = phone;
    server.AdvanceAmount = $("#advAmount").val();

    server.SerialNo = $("#serialNo").val();
    server.MonthlyNo = $("#monthlyptid").val();
    server.BloodGroup = $("#BloodGroupDd").val();
    server.Caste = $("#caste").val();
    server.OpdId = opId;
    server.RoomId = $('#roomDd').val();
    server.AdmissionFee = adminFee;

  

    server.DoctorId = docDd;
    server.MartialStatus = $("#mstatus").val();

    $(".loading-docs").show();
    postRequest(server, "CreateFormAjax", function (slip) {
        toastr.success("Operation performed");
        $(".loading-docs").hide();
        window.location.href = "IpFormsList";
    });

});

$("#backToIpList").click(function () {
    window.location.href = "IpFormsList";
});


