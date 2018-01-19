
var parmsList = [];

setTimeout(function(parameters) {


    $(document).ready(
        function() {
            var searchTerm = null;
            // Remote data example
            var remoteDataConfig = {
                placeholder: "Search for an option...",
                minimumInputLength: 2,
                ajax: {
                    url: '/Labs/SearchParmsForMapping',
                    dataType: 'json',
                    data: function(term, page) {
                        return {
                            searchTerm: term
                        };
                    },
                    results: function(data, page) {
                        parmsList= data.LabParms;
                        // Normally server side logic would parse your JSON string from your data returned above then return results here that match your search term. In this case just returning 2 mock options.
                        return {
                            results: data.LabParms
                        };
                    }
                },
                formatResult: function(option) {
                    return "<div>" + option.Name + "</div>";
                },
                formatSelection: function(option) {
                    return option.Name;
                }
            };

            $("#remoteDataExample").select2(remoteDataConfig);
        });

}, 1000);

$('#addParm').click(function () {

    var url = new URL(window.location.href);
    var labId = url.searchParams.get("labId");
    var parmId = $("#remoteDataExample").val();

    if (dataList!==null) {
        for (var i = 0; i < dataList.length; i++) {
            if (dataList[i].ParmId === parseInt(parmId)) {
                toastr.error("Parameter already existed");
                return false;
            }
        }
    }

    if (parmId === "") {
        toastr.error("Enter Parameter");
        return;
    }

    var server = {};
    server.ParmId = parmId;
    server.TestId = labId;
   

    $(".loading-docs").show();
    postRequest(server, "AddNewMapping", function (lab) {
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