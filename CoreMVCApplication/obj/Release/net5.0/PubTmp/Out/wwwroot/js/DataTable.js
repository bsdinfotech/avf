
function DBTable() {
   
    oTable = $("#data-table").DataTable({
        columnDefs: [{
            "defaultContent": "-",
            "targets": "_all"
        }]
    }); 
}

function LoadData(CtrlLink) {
    $.ajax({
        type: "POST",
        url: CtrlLink,
        data: '',
        dataType: "json",
        success: function (d) {
            DrawTable(d);
        },
        beforeSend: function () {
            $('#DataPanel').hide();
            $('#Loader').show();
        },
        complete: function () {
            $('#Loader').hide();
            $('#DataPanel').show();
        }
    });
}

function DrawTable(data) {
    var i = 1;
    var table = $('#data-table').DataTable();
    table.rows().remove().draw();
    $(data).each(function () {
        var newRow = addRow(this, i);
        table.row.add($(newRow)).draw();
        i++;
    });
}


