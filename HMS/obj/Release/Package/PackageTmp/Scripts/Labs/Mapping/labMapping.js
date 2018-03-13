
var parmsList = [];

setTimeout(function(parameters) {


    $(document).ready(
        function () {
            $("#labMgmtList").addClass('active');

            var remoteDataConfig = {
                placeholder: "Search for an option...",
                dropdownParent: $("#exampleModal"),
                minimumInputLength: 2,
                ajax: {
                    url: '/Labs/SearchParmsForMapping',
                    dataType: 'json',
                    processResults: function (data) {
                        // Transforms the top-level key of the response object from 'items' to 'results'
                        return {
                            results: data.LabParms
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

            $("#parmNormal").select2(remoteDataConfig);


            remoteDataConfig = {
                placeholder: "Search for lab test...",
                dropdownParent: $("#exampleModal"),
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

        });

}, 1000);

var testId = 0;

$('#testNameTb').on("change", function (e) {
    testId = $("#testNameTb").val();
    $(".loading-docs").show();
    postRequest({ labId : testId}, "GetLabParmsByLabId", function (labParms) {
        $(".loading-docs").hide();
        var html = "";
        if (labParms != null && labParms.LabParms.length > 0) {
            for (var i = 0; i < labParms.LabParms.length ; i++) {
                html = html + "<option value='"+labParms.LabParms[i].id+"'>"+labParms.LabParms[i].Name+"</option>";
            }
            $('#parmMix').html(html);
        }
    });

});

var serverList = [];


$('#addParm').click(function () {


    var url = new URL(window.location.href);
    var labId = url.searchParams.get("labId");

  

    var isMixed = $("#mix_report").is(':checked');
    if (isMixed) {
        var parms = $('#parmMix').val();
        for (var j = 0; j < parms.length; j++) {
            var serverObj = {};
            serverObj.ParmId = parms[j];
            serverObj.TestId = labId;
            serverList.push(serverObj);
        }
    } else {
        var parmId = $("#parmNormal").val();
        var serverObjNew = {};
        serverObjNew.ParmId = parmId;
        serverObjNew.TestId = labId;
        serverList.push(serverObjNew);
    }

   



    
    var isExisted = false;
    if (dataList !== null) {
        for (var i = 0; i < dataList.length; i++) {

            for (var k = 0; k < serverList.length; k++) {
               
                if (dataList[i].ParmId === parseInt(serverList[k].ParmId)) {
                    isExisted = true;
                }                
            }           
        }
    }

    if (isExisted) {
        toastr.error("Parameter(s) already existed");
        return false;
    }

    if (serverList.length === 0) {
        toastr.error("Enter Parameter");
        return;
    }

   
   

    $(".loading-docs").show();
    postRequest(serverList, "AddNewMapping", function (lab) {
        location.reload();
    });
});

function deleteMaping(item) {
    $(".loading-docs").show();
    postRequest(item, "DeleteMapping", function (lab) {
        toastr.success("Mapping deleted");
        location.reload();
    });
}

$("#mix_report").change(function() {
    var status = $(this).is(':checked');
    if (status) {
        $("#mixDiv").show();
        $("#normalDiv").hide();
        serverList = [];
    } else {
        $("#normalDiv").show();
        $("#mixDiv").hide();
        serverList = [];
    }
});



$(document).ready(function () {
    $('#parm-nameMix').multiselect({
        includeSelectAllOption: true
    });
});