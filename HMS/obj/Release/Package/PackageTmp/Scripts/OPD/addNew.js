$(document).ready(function() {
    $("#backToOpdpList").click(function () {
        window.location.href = "Index";
    });

    $("#createNewOpdFormBtn").click(function () {

        var phone = $("#phNo").val().trim();

        var docid = $("#docDd option:selected").val();
        if (docid === "0" && modeObj !== "cusinfo") {
            toastr.error("Please select doctor");
            return;
        }

        if (!ValidateByClassName("opdRequired") && modeObj !== "cusinfo") {
            toastr.error("Please add mandatory data");
            return;
        }

        var ptid = $("#ptid").val().trim();
        if (ptid === "" && modeObj !== "cusinfo") {
            toastr.error("Invalid Patient Id");
            return;
        }

        var age = $("#age").val().trim();
        if (age === "" || !numberValidator(age)) {
            toastr.error("Age must be valid digits");
            return;
        }

        debugger;

        var fee = $('#fee').val();
        var discount = $("#discount").val();

        if (discount !== "") {

            var dis = parseInt(discount);
            var act = parseInt(fee);

            if (dis <0 || dis > act)
            {
                toastr.error("Discount amount should be less than total amount");
                return;
            }
        }

        var server = {};
        server.DailyNo = $("#dailyNo").val();
        server.PatientNo = ptid;
        server.Name = $("#fullname").val();
        server.GuardianName = $("#gname").val();
        server.Age = age;
        server.Gender = $("#gender").val()==="1";
        server.CNIC = $("#cnicNo").val();
        server.Address = $("#addredd").val();
        server.Phone = phone;
        server.VisitNo = $("#visitNo").val();
        server.Id = idObj;

        server.Discount = $("#discount").val();
        server.DiscountBy = $("#discountBy").val();

        server.Mode = modeObj;

        server.DoctorId = docid;
        server.MartialStatus = $("#mstatus").val()==="1";

        $(".loading-docs").show();
        postRequest(server, "CreateForm", function (item) {
            if (item == null) {
                toastr.error("Failed to create opd record");
                $(".loading-docs").hide();
                return;
            }

            toastr.success("Operation performed");
            $(".loading-docs").hide();
            clearData();
            var r = confirm("Would you like to print?");
            if (r === true) {
                debugger;
                //   alert("No printer installed");
                $('#printLink').attr('href', '/Print/PrintOpdSlip?opdId=' + item.Id);
                $('a#printLink')[0].click();
            } else {

            }
        });

    });

    $("#opdForms").addClass('active');

    $("#docDd").change(function () {
        var selectedDocObj = $("#docDd option:selected");
        var docId = $(selectedDocObj).val();
        var docName = $(selectedDocObj).text();
        var fee = $(selectedDocObj).attr('data-fee');
        $("#fee").val(fee);
        if (docId !== "0") {
            checkMetaData(docId);
        } else {
            $(".other-cons").val(" ");
            $("#visitNo").val("");
        }
    });

    function checkMetaData(docId) {

        $(".loading-docs").show();
        var server = {};
        server.PatientNo = $("#ptid").val();
        server.DoctorId = docId;
        postRequest(server, "GetFormMetadata", function (serverdata) {
            $(".loading-docs").hide();
            if (serverdata !== null && serverdata !== undefined) {
                $("#dailyNo").val(serverdata.DailyNo);
                $("#dateTime").val(serverdata.DateTime);
                $("#serialNo").val(serverdata.SerialNo);
                $("#ptid").val(serverdata.PatientNo);
                $("#visitNo").val(serverdata.VisitNo);
                if (serverdata.OpdForm !== null) {
                    $("#fullname").val(serverdata.OpdForm.Name);
                    $("#gname").val(serverdata.OpdForm.GuardianName);
                    $("#age").val(serverdata.OpdForm.Age);
                    $("#gender").val(serverdata.OpdForm.Gender === true ? 1 : 0);
                    $("#cnicNo").val(serverdata.OpdForm.CNIC);
                    $("#addredd").val(serverdata.OpdForm.Address);
                    $("#phNo").val(serverdata.OpdForm.Phone);
                    $("#mstatus").val(serverdata.OpdForm.MartialStatus === true ? 1 : 0);
                }
            }
        });
    }

    $("#clearOpdFormBtn").click(function () {
        clearData();
    });

    function clearData() {
        $("#docDd")[0].selectedIndex = 0;
        $(".opdRequired").val(" ");
        $(".other-cons").val(" ");// read only fields
        $("#cnicNo").val(" ");
        $("#phNo").val(" ");
    }

    $("#cnicNo").mask("99999-9999999-9");
    $("#phNo").mask("0399-9999999");



    var shVal = "0";
    var gnVal = "0";
    if (shaadiObj) {
        shVal = "1";
    }
    if (genderObj) {
        gnVal = "1";
    }

    $("#gender").val(gnVal);
    $("#mstatus").val(shVal);
});