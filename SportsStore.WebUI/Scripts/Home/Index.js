function selectView(view) {
    $('.display').not("#" + view + "Display").hide();
    $("#" + view + "Display").show(); 
}

function getData() {
    $.ajax({
        type: "GET",
        url: "/api/reservation",
        success: function(data) {
            $("#tableBody").empty();
            alert(data.length);
            var dataLength = data.length;
            for (var i = 0; i < dataLength; i++) {
                $('#tableBody').append('<tr><td><input id="id" name="id" type="radio"'
                    + 'value="' + data[i].ReservationId + '" /></td>'
                    + '<td>' + data[i].ClientName + '</td>'
                    + '<td>' + data[i].Location + '</td></tr>');
            }
            $('input:radio')[0].checked = "checked";
            selectView("summary");
        }
    });
}

$(document).ready(function() {
    selectView("summary");
    getData();
    $("button").click(function(e) {
        var selectedRadio = $('input:radio:checked')
        switch (e.target.id) {
        case "refresh":
            getData();
            break;
        case "delete":
            break;
        case "add":
            selectView("add");
            break;
        case "edit":
            selectView("edit");
            break;
        case "submitEdit":
            break;
        }
    });
});