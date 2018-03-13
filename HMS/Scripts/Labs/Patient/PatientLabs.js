$("#searchField").keyup(function () {
    pagging.Current = 0;
    pagging.TotalItems = 0;
    getLabTest();
});

function getLabTest() {

    $(".loading-docs").show();
    var term = $("#searchField").val();
    postRequest({ SearchString: term, Pagging: pagging }, "SearchPatientLabs", function (serverData) {
        $("#paggingDiv").pagination('updateItems', serverData.Pagging.TotalItems);
        var items = serverData.PatientWithLabs;
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
    var url = "../labs/PatientLabDetails"
    var row = "<tr>" +
        "<td>" + item.Id + "</td>" +
         "<td>" + item.PatientNo + "</td>" +
        "<td><a href='"+url+"?patientLabId="+item.Id+"'>"+item.Name+"</a></td>"+
        "<td>" + item.GuardianName + "</td>" +
        "<td>" + item.RegisteredBy + "</td>" +
         "<td>" + item.RequestedOn + "</td>" +
        "<td>" + item.ReportedOn + "</td>" +
        "<td>" + item.Phone + "</td>" +
        " <td>"+
           "<a class='icon-hand'  href='../Print/PrintLabSlip?patientId=" + item.Id + "'>" +
                "<i title='Printe Report' class='material-icons'>printer</i>"+
            "</a>"+
          "</td>"+
        "</tr>";
    return row;
}

$('#assignLabs').addClass('active');