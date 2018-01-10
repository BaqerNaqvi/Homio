$("#labMgmtList").addClass('active');


$("#searchField").keyup(function() {
    pagging.Current = 0;
    pagging.TotalItems = 0;
    getLabTest();
});

function getLabTest() {
    var url = "/IpForms/CreateIpForm";

    $(".loading-docs").show();
    var term = $("#searchField").val();
    postRequest({ SearchString: term, Pagging: pagging }, "SearchLabs", function(serverData) {
        $("#paggingDiv").pagination('updateItems', serverData.Pagging.TotalItems);
        var items = serverData.LabTest;
        var html = "";
        for (var i = 0; i < items.length; i++) {
            var row= paingLabListItem(items[i]);
            html = html + row;
        }
        $("#tableBody").html(html);
        $(".loading-docs").hide();
    });
}

function paingLabListItem(item) {
    var status = item.Status ? "checked" : "";
    var row = "<tr>" + "<td>" + item.Id + "</td>" +
                                        "<td>" + item.Name + "</td>" +
                                        "<td>" + item.Fee + "</td>" +
                                         "<td>" +
                                                "<div class='checkbox'>" +
                                                    "<label>" +
                                                        "<input class='labtestStatus' id='lab_check_" + item.Id + "' type='checkbox' " + status + ">" +
                                                    "</label>" +
                                                "</div>" +
                                           " </td>" +
                                          "<td title='Edit' class='cursorPointer editlab' id=" + item.Id + "'><i class='material-icons'>edit</i></td>" +
                                    "</tr>";
    return row;
}

$('body').on('click', '.labtestStatus', function () {
    $(".loading-docs").show();
    var checkBoxId = $(this).attr("Id");
    var server = {};
    server.Id = checkBoxId.split("_")[2]; // test id 
    server.Status = $("#" + checkBoxId).prop("checked");
    postRequest(server, "ChangeStatus", function () {
        toastr.success("Status changed successfully");
        $(".loading-docs").hide();
    });
});

$('#addlab').click(function () {
    if (!ValidateByClassName('labcls')) {
        toastr.error("Please add values");
        return false;
    }
    var name = $('#labname').val();
    var fee = $('#labfee').val();
    var interval = $('#labInterval').val();

    var server = {};
    server.Name = name;
    server.Fee = fee;
    server.Interval = interval;

    $(".loading-docs").show();
    postRequest(server, "AddNew", function (lab) {
        var row = paingLabListItem(lab);
        $("#tableBody").prepend(row);
        toastr.success("New Lab Test Added");
        $(".loading-docs").hide();
        $('#exampleModal').modal('toggle');
    });
});