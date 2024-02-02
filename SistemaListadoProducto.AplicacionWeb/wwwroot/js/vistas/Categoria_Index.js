let tablaData;

const MODELO_BASE = {
    idCategoria: 0,
    descripcion: '',
    isActive: 1,
}

$(document).ready(function () {
    tablaData = $('#tbdata').DataTable({
        responsive: true,
         "ajax": {
             "url": "/Categoria/ListaCategoria",
             "type": "GET",
             "datatype": "json"
         },
         "columns": [
             { "data": "idCategoria","visible": false, "searchable": false },
             { "data": "descripcion" },
             {
                 "data": "isActive", render: function (data) {
                     if (data == true) {
                         return '<span class="badge badge-info">Activo </span>'
                     } else {
                         return '<span class="badge badge-danger">Desactivado </span>'
                     }
                 }
             },
             {
                 "defaultContent": '<button class="btn btn-primary btn-editar btn-sm mr-2"><i class="fas fa-pencil-alt"></i></button>' +
                     '<button class="btn btn-danger btn-eliminar btn-sm"><i class="fas fa-trash-alt"></i></button>',
                 "orderable": false,
                 "searchable": false,
                 "width": "80px"
             }
         ],
         order: [[0, "desc"]],
        dom: "Bfrtip",
        buttons: [
            {
                text: 'Exportar Excel',
                extend: 'excelHtml5',
                title: '',
                filename: 'Reporte Categoria',
                exportOptions: {
                    columns: [1, 2]
                }
            }, 'pageLength'
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        },
    });
});

function mostrarModal(modelo = MODELO_BASE) {
    $("#txtId").val(modelo.idCategoria)
    $("#txtDescripcion").val(modelo.descripcion)
    $("#cboEstado").val(modelo.isActive)
    $('#modalData').modal("show")
}

$('#btnNuevo').click(function () {
    mostrarModal();
});

$('#btnGuardar').click(function () {
    const inputs = $("input.input-validar").serializeArray();
    const inputs_sin_valor = inputs.filter((item) => item.value.trim() == "");

    if (inputs_sin_valor.length > 0) {
        const mensaje = `Debe completar el campo: "${inputs_sin_valor[0].name}"`;
        toastr.warning("", mensaje);
        $(`input[name="${inputs_sin_valor[0].name}"]`).focus();
        return;
    }

    const modelo = structuredClone(MODELO_BASE);
    modelo["idCategoria"] = parseInt($("#txtId").val());
    modelo["descripcion"] = $("#txtDescripcion").val();
    modelo["isActive"] = parseInt($("#cboEstado").val());

    const formData = new FormData();

    formData.append("modelo", JSON.stringify(modelo));

    $("#modalData").find("div.modal-content").LoadingOverlay("show");

    if (modelo.idCategoria == 0) {
        fetch("/Categoria/Crear", {
            method: "POST",
            body: formData,
        }).then(response => {
            $("#modalData").find("div.modal-content").LoadingOverlay("hide");
            return response.ok ? response.json() : Promise.reject(response);
        }).then(responseJSON => {
            if (responseJSON.estado) {
                tablaData.row.add(responseJSON.objeto).draw(false);
                $("#modalData").modal("hide");
                swal("Listo", "El usuario fue creado", "success");
            } else {
                swal("Error", responseJSON.mensaje, "error");
            }
        });
    } else {
        fetch("/Categoria/Editar", {
            method: "PUT",
            body: formData,
        }).then(response => {
            $("#modalData").find("div.modal-content").LoadingOverlay("hide");
            return response.ok ? response.json() : Promise.reject(response);
        }).then(responseJSON => {
            if (responseJSON.estado) {
                tablaData.row(filaSeleccionada).data(responseJSON.objeto).draw(false);
                filaSeleccionada = null;
                $("#modalData").modal("hide");
                swal("Listo", "El usuario fue modificado", "success");
            } else {
                swal("Error", responseJSON.mensaje, "error");
            }
        });
    }
})

let filaSeleccionada;

$("#tbdata tbody").on("click", ".btn-editar", function () {
    if ($(this).closest("tr").hasClass("child")) {
        filaSeleccionada = $(this).closest("tr").prev();
    } else {
        filaSeleccionada = $(this).closest("tr");
    }

    const data = tablaData.row(filaSeleccionada).data();

    mostrarModal(data);

});

$("#tbdata tbody").on("click", ".btn-eliminar", function () {
    let fila;
    if ($(this).closest("tr").hasClass("child")) {
        fila = $(this).closest("tr").prev();
    } else {
        fila = $(this).closest("tr");
    }

    const data = tablaData.row(fila).data();

    swal({
        title: "¿Está seguro?",
        text: `Eliminar la categoria "${data.descripcion}"`,
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Si, eliminar",
        cancelButtonText: "No, cancelar",
        closeOnConfirm: false,
        closeOnCancel: true,
    },
        function (respuesta) {
            if (respuesta) {
                $(".showSweetAlert").LoadingOverlay("show");

                fetch(`/Categoria/Eliminar?idCategoria=${data.idCategoria}`, {
                    method: "DELETE",
                }).then(response => {
                    $(".showSweetAlert").LoadingOverlay("hide");
                    return response.ok ? response.json() : Promise.reject(response);
                }).then(responseJSON => {
                    if (responseJSON.estado) {
                        tablaData.row(fila).remove().draw();
                        swal("Listo", "la categoria fue eliminado", "success");
                    } else {
                        swal("Error", responseJSON.mensaje, "error");
                    }
                });
            }
        }
    );
});