var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $("#tblArticles").DataTable({
        "ajax": {
            "url": "/Admin/Articles/GetAll",
            "type": "GET",
            "dataType": "json"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "name", "width": "20%" },
            { "data": "category.name", "width": "15%" },
            {
                "data": "urlImage",
                "render": function (image) {
                    return `<img src="../${image}" width="60%" height="60%" />`
                }
            },
            { "data": "createdDate", "widht": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/Articles/Edit/${data}" class="btn btn-success text-white" style="cursor:pointer; width:100px;">
                                    <i class="far fa-edit"></i> Editar Articulo
                                </a>
                                &nbsp;
                                <a onclick="Delete('/Admin/Articles/Delete/${data}')" class="btn btn-danger text-white" style="cursor:pointer; width:150px;">
                                    <i class="far fa-trash-alt"></i> Eliminar Articulo
                                </a>
                            </div>`;
                }, "width": "30%"
            }
        ],
        "language": {
            "decimal": "",
            "emptyTable": "No ahi registros guardados",
            "info": "Mostrando START a END de TOTAL Entradas",
            "infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
            "infoFiltered": "(Filtrado de MAX total entradas)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar MENU Entradas",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            "search": "Buscar:",
            "zeroRecords": "Sin resultados encontrados",
            "paginate": {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
        "width": "100%"
    });
}

function Delete(url) {
    swal({
        title: "¿Estás seguro de borrar este articulo?",
        text: "¡Este contenido no podrá ser recuperado!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "¡Sí, Borrar!",
        closeOnConfirm: false
    }, function () {
        $.ajax({
            type: 'DELETE',
            url: url,
            success: function (data) {
                if (data.success) {
                    swal("¡Eliminado!", "El articulo ha sido eliminado.", "success");
                    dataTable.ajax.reload();
                }
                else {
                    swal("¡Error!", data.message, "error");
                }
            },
            error: function () {
                swal("¡Error!", "Ocurrió un error al eliminar el articulo.", "error");
            }
        });
    });
}