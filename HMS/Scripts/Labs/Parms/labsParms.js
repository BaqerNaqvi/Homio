$("#labMgmtList").addClass('active');


$("#searchField").keyup(function () {
    pagging.Current = 0;
    pagging.TotalItems = 0;
    getLabTest();
});

function getLabTest() {

    $(".loading-docs").show();
    var term = $("#searchField").val();
    postRequest({ SearchString: term, Pagging: pagging }, "SearchParms", function (serverData) {
        $("#paggingDiv").pagination('updateItems', serverData.Pagging.TotalItems);
        var items = serverData.LabParms;
        var html = "";
        for (var i = 0; i < items.length; i++) {
            var row = paingLabListItem(items[i]);
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
                                        "<td>" + item.Unit + "</td>" +
                                         "<td>" + item.NormarVal + "</td>" +
                                         "<td>" +
                                                "<div class='checkbox'>" +
                                                    "<label>" +
                                                        "<input class='labtestStatus' id='lab_check_" + item.Id + "' type='checkbox' " + status + ">" +
                                                    "</label>" +
                                                "</div>" +
                                           " </td>" +
                                          "<td title='Edit' onclick='editParm("+JSON.stringify(item)+")' class='cursorPointer editlab' id=" + item.Id + "'><i class='material-icons'>edit</i></td>" +
                                    "</tr>";
    return row;
}

$('body').on('click', '.labtestStatus', function () {
    $(".loading-docs").show();
    var checkBoxId = $(this).attr("Id");
    var server = {};
    server.Id = checkBoxId.split("_")[2]; // test id 
    server.Status = $("#" + checkBoxId).prop("checked");
    postRequest(server, "ChangeParmStatus", function () {
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
    var unit = $('#labUnit').val();
    var normal = $('#range').val();

    var server = {};
    server.Name = name;
    server.Unit = unit;
    server.NormarVal = normal;
    server.Id = itemId;

    $(".loading-docs").show();
    postRequest(server, "AddNewParm", function (lab) {
        var row = paingLabListItem(lab);
        toastr.success("Parameter Updated");
        $(".loading-docs").hide();
        $('#exampleModal').modal('toggle');
        if (itemId !== 0) {
            location.reload();
        } else {
            $("#tableBody").prepend(row);

        }
    });
});

var itemId = 0;
function editParm(item) {
    $('#exampleModal').modal('toggle');
    $('#labname').val(item.Name);
    $('#labUnit').val(item.Unit);
    $('#range').val(item.NormarVal);
    itemId = item.Id;
}

$('#createNewTest').click(function() {
    itemId = 0;
    $('.labcls').val('');
});