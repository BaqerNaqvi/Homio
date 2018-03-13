$(document).ready(function() {
    $("#backToOpdpList").click(function() {
        window.location.href = "PatientLabs";
    });

    if (PatientLabIdObj!=0) {
        $('#addnewtest').show();
        $('#createNewTest').show();
        $('#printLab').show();
    }


    var stat = mStatus === true ? 1 : 0;
    var gen1 = gen === true ? 1 : 0;


    $("#phNo").mask("0399-9999999");

    $('#BloodGroupDd').val(bGroup);
    $('#mstatus').val(stat);
    $('#gender').val(gen1);
    $('#assignLabs').addClass('active');


    var remoteDataConfig = {
        placeholder: "Search for lab test...",
        dropdownParent: $("#createNewTestModal"),
        minimumInputLength: 2,
        ajax: {
            url: '/Labs/SearchLabsForMapping',
            dataType: 'json',
            processResults: function (data) {
                // Transforms the top-level key of the response object from 'items' to 'results'
                return {
                    results: data.LabTests
                };
            }
        },
        templateResult: function (result) {
            return result.Name;
        },
        templateSelection: function (selection) {
            return selection.Name;
        },
    };

    $("#testNameTb").select2(remoteDataConfig);
    $('#parmMix').select2();
    $('#parmMix').on('select2:select', function (e) {
        var data = e.params.data;
        var results  = $.grep(labParmsList, function (item, i) {
            return item.id == data.id;
        });
        if (results != undefined && results.length > 0) {
            addedPrice = addedPrice + results[0].Price;
            $('#labfee').val(addedPrice);
        }
        
    });
    $('#parmMix').on('select2:unselect', function (e) {
        var data = e.params.data;
        var results = $.grep(labParmsList, function (item, i) {
            return item.id == data.id;
        });
        if (results != undefined && results.length>0) {
            addedPrice = addedPrice - results[0].Price;
            $('#labfee').val(addedPrice);
        }
    });
});

var selectedTestId = 0;
var addedPrice = 0;
$("#labDd").change(function () {
    var selected = $("#labDd").val();
    selectedTestId = selected;
    postRequest({ labId: selected }, "GetLabParmsByLabId", function (serverData) {
       
        var items = serverData.LabParms;
        var html = "";
        for (var i = 0; i < items.length; i++) {

            var accVal = "<tr><td>" + items[i].Name + "</td>" +
                                "<td>" + items[i].NorVal + "</td>" +
            "<td><input type='text' id='parm_" + items[i].id + "_"+selected+"'  class='plabedit' value=''/></td></tr>";
            html = html + accVal;
        }
        $("#customers").find("tr:gt(0)").remove();
        $("#customers").append(html);
       // $(".loading-docs").hide();
    });
});

$("#addPlab").click(function () {

    $('#exampleModalLabel').text('ADD NEW TEST');
    var allParms = $(".plabedit");

    if (allParms.length === 0) {
        toastr.error("Please select valid Lab Test");
        return;
    }

    var server = [];
    for (var i = 0; i < allParms.length; i++) {
        var obj = {};

        obj.Id = 0;
        obj.ParmId = allParms[i].id.split('_')[1];  // parmId
        obj.TestId = allParms[i].id.split('_')[2];
        obj.ActualVal = $(allParms[i]).val();
        obj.PatientLabId = PatientLabIdObj;//  window.location.href.split('=')[1];
        server.push(obj);
    }

    var isExisted = false;
    if (LabTestsDdModelData !== null) {
        for (var i = 0; i < LabTestsDdModelData.length; i++) {

            if (LabTestsDdModelData[i].TestId === parseInt(server[0].TestId)) {
                isExisted = true;
            }
        }
    }

    if (isExisted) {
        toastr.error("Lab Test already existed");
        return false;
    }
    

    postRequest({ labs: server }, "AddPatientLabs", function (serverData) {
        toastr.success("Lab Test Added");
        $('#exampleModal').modal('toggle');
        // location.reload();
        repaintLi(serverData);

        // paint fee
        $('#tamount').val(serverData.TotalFee);

    });


   
});


function repaintLi(serverData) {
    $('#sp_' + serverData.Id).remove();

    var lab = "<li id='sp_" + serverData.Id + "' ><a data-toggle='modal' data-target='#exampleModal' onclick='showpLab(" + JSON.stringify(serverData) + ")' style='cursor: pointer' href=''>" + serverData.TestName + " (Rs. "+serverData.Lab_Test.Fee+") </a>" +
         "<span style='cursor: pointer' title='remove this lab'><a onclick='removeLab(" + JSON.stringify(serverData) + ")' style='color:red'> &nbsp;  &nbsp;Remove</a></span>" +
        "</li>";

    $('#assoLabUl').append(lab);
}

$("#updatePlabPe").click(function () {

    var allParms = $(".plabedit");
    var server = [];
    for (var i = 0; i < allParms.length; i++) {
        var obj = {};

        obj.Id = allParms[i].id.split('_')[3];
        obj.ParmId = allParms[i].id.split('_')[1];  // parmId
        obj.TestId = allParms[i].id.split('_')[2];
        obj.ActualVal = $(allParms[i]).val();
        obj.PatientLabId = PatientLabIdObj;
        server.push(obj);
    }


    postRequest({ labs: server }, "UpdatePatientLabs", function (serverData) {
        //
        toastr.success("Lab Test Updated");
        $('#exampleModal').modal('toggle');
        repaintLi(serverData);


    });
});


var plab_labId = 0;

function showpLab(item) {

    plab_labId = item.Id;
   

    $('#exampleModalLabel').text(item.TestName+" - Details");

    $('#addPlab').hide();
    $('#labddpat').hide();
    $('#updatePlabPe').show();

    var html = "";
    var items = item.ParmForPatientLabs;
    for (var i = 0; i < items.length; i++) {

        var accVal = "<tr><td>" + items[i].Name + "</td>" +
                            "<td>" + items[i].NormarVal + "</td>" +
        "<td><input type='text' id='parm_" + items[i].ParmId + "_" + items[i].TestId + "_" + items[i].Id + "' class='plabedit'  value='" + items[i].ActualVal + "'/></td></tr>";
        html = html + accVal;
    }
    $("#customers").find("tr:gt(0)").remove();
    $("#customers").append(html);
}


$("#addnewtest").click(function () {


     if (PatientLabIdObj==0) {
         toastr.error("Please Update Data First");
         return;
     }

     $('#labDd').val(0);
    $("#exampleModal").modal('toggle');
    $("#customers").find("tr:gt(0)").remove();
    $('#addPlab').show();
    $('#labddpat').show();
    $('#updatePlabPe').hide();
});


$("#createNewTest").click(function () {


    if (PatientLabIdObj == 0) {
        toastr.error("Please Update Data First");
        return;
    }
    $("#createNewTestModal").modal('toggle');
   

});



$("#createNewOpdFormBtn").click(function () {
    var server = {};
    server.Id = PatientLabIdObj;
    server.PatientNo = $('#ptid').val();
    server.Name = $('#fname').val();
    server.Age = $('#age').val();
    server.Gender = $('#gender').val() ==="1";
    server.RegisteredBy = $('#Regis').val();
    server.PerformedBy = $('#Perfor').val();
    server.RequestedOn = $().val();
    server.ReportedOn = $().val();
    server.BloodGroup = $('#BloodGroupDd').val();
    server.GuardianName = $('#gname').val();
    server.Phone = $('#phNo').val();
    server.Address = $('#address').val();
    server.MaritalStatus = $('#mstatus').val() ==="1";

    var amount = $('#tamount').val();
    server.Discount = $('#discount').val();
    server.DiscountBy = $('#discountBy').val();

   


    if (server.Discount !== "") {

        if (amount === "") {
            toastr.error("Can not add discount amount.");
            return;
        }

        var dis = parseInt(server.Discount);
        var act = parseInt(amount);

        if (dis < 0 || dis > act) {
            toastr.error("Discount amount should be less than total amount");
            return;
        } else {
            var percentage = dis / act * 100;
            if (percentage > 10) {
                toastr.error("Maxium allowed discount is 10% (Rs. " + Math.round((0.1 * act)-1) + ")");
                return;
            }
        }
    }


    if (!ValidateByClassName('pLabRequired')) {
        toastr.error("Please fill up data");
        return;
    }

    if (PatientLabIdObj==0) {
        // NEW

        postRequest({ model: server }, "AddNewPatientToLab", function (serverData) {
            toastr.success("Please add lab tests now!");
            $('#addnewtest').show();
            $('#LabNo').val(serverData);
            PatientLabIdObj = serverData;

            //

            $('#addnewtest').show();
            $('#createNewTest').show();
            $('#printLab').show();


        });

    } else {
        //UPDATE 
        postRequest({ model: server }, "UpdatePatientToLab", function (serverData) {
            if (serverData==false) {
                toastr.error("Please review discount amount");
                return;
            }

            toastr.success("Data updated");
        });
    }
});


$('#ptid').blur(function() {
    var val = $(this).val();
    if (val !== "") {
        postRequest({ mrNo: val }, "GetPatientByMrNo", function (server) {

            var stat = server.MaritalStatus === true ? 1 : 0;
            var gen = server.Gender === true ? 1 : 0;

            debugger;

            PatientLabIdObj = 0;

            $('#fname').val(server.Name);
            $('#age').val(server.Age);
            $('#gender').val(gen);

            $('#gname').val(server.GuardianName);

            $('#phNo').val(server.Phone);
            $('#address').val( server.Address);
            $('#mstatus').val(stat);


        });
    }
});

var labParmsList;
$('#testNameTb').on("change", function (e) {
   var testId = $("#testNameTb").val();
    $(".loading-docs").show();
    postRequest({ labId: testId }, "GetLabParmsByLabId", function (labParms) {
        $(".loading-docs").hide();
        var html = "";
        if (labParms != null && labParms.LabParms.length > 0) {
            labParmsList = labParms.LabParms;

            for (var i = 0; i < labParms.LabParms.length ; i++) {
                html = html + "<option value='" + labParms.LabParms[i].id + "'>" + labParms.LabParms[i].Name+' (Rs. ' +  labParms.LabParms[i].Price+")</option>";
            }
            $('#parmMix').append(html);
        }
    });

});

$('#addMixlab').click(function() {
    if (!ValidateByClassName('labclsMix')) {
        toastr.error("Please fill up data");
        return;
    }

    var server = {};
    server.Name = $('#labname').val();
    server.Fee = $('#labfee').val();
    server.PatientLabId = PatientLabIdObj;

    server.ParmIds = [];
    var parms = $('#parmMix').val();
    for (var j = 0; j < parms.length; j++) {       
        server.ParmIds.push(parms[j]);
    }

    postRequest(server, "CreateMixReport", function (lab) {
        toastr.success("Mix Lab Test Added");
        $('#createNewTestModal').modal('toggle');
       
        $('#labDd').append("<option value=" + lab.Id + ">" + lab.Name + "</option>");
        $("#addnewtest").click();
        $('#labDd').val(0);
        $('#labDd').val(lab.Id);
        $('#labDd').change();
    });
});

function removeLab(lab) {
   
    var server = {};
    server.TestId = lab.TestId;
    server.PatientLabId = lab.PatientLabId;


    postRequest(server, "RemovePatientLab", function (response) {
        toastr.success("Lab Removed");
        $("#sp_" + lab.Id).remove();

        var existedDiscount = $('#discount').val();
        $('#tamount').val(response);

        if (existedDiscount !== "") {

            

            var dis = parseInt(existedDiscount);
            var act = parseInt($('#tamount').val());

            if (dis < 0 || dis > act) {
                $('#discount').val(0);
                $('#discountBy').val("");
               // toastr.error("Discount amount should be less than total amount");
            }
        }

    });
}


$('#printLab').click(function () {

    if (PatientLabIdObj == 0) {
        toastr.error("Please Update Data First");
        return;
    }

    window.location = "../Print/PrintLabSlip?patientId=" + PatientLabIdObj;
});

$('#backTolabMgmt').click(function () {
    location.href = '../Labs/PatientLabs'
});
