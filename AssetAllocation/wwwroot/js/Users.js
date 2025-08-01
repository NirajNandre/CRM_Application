﻿
var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/users",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "username", "width": "10%" },
            { "data": "password", "width": "10%" },
            { "data": "role", "width": "10%" },
            { "data": "fullname", "width": "20%" },
            { "data": "isactive", "width": "10%" },
            { "data": "lastlogin", "width": "10%" },
            { "data": "createdon", "width": "10%" },
            
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                         <a href="/Users/Upsert?id=${data}" class='btn btn-success text-white' style='cursor:pointer; width:70px;'>
                            Edit
                        </a>
                        &nbsp;
                        <a class='btn btn-danger text-white' style='cursor:pointer; width:70px;'
                           onclick=Delete('/api/Users?id=+${data}')>
                            Delete
                        </a>
                        </div>`;
                }, "width": "20%"
            }
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%"
    });
}
function Delete(url) {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}