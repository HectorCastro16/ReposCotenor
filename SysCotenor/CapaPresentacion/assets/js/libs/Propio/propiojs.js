


    function cambiar(tecla){
        tecla.value = tecla.value.replace(".", ",");
    }


        function valida(e) {
            tecla = (document.all) ? e.keyCode : e.which;

            //Tecla de retroceso para borrar, siempre la permite
            if (tecla == 8) {
                return true;
            }

            // Patron de entrada, en este caso solo acepta numeros
            patron = /[0-9]/;
            tecla_final = String.fromCharCode(tecla);
            return patron.test(tecla_final);
        }

        jQuery(function ($) {
            $('#myModalEliminar').on('show.bs.modal', function (e) {
                var id = $(e.relatedTarget).data().id;
                $(e.currentTarget).find('#Id').val(id);
            });
        });
    jQuery(function ($) {
        $('#myModalBloquear').on('show.bs.modal', function (e) {
            var id = $(e.relatedTarget).data().id;
            $(e.currentTarget).find('#Id').val(id);
        });
    });
    jQuery(function ($) {
        $('#myModalActivar').on('show.bs.modal', function (e) {
            var id = $(e.relatedTarget).data().id;
            $(e.currentTarget).find('#Id').val(id);
        });
    });

    $("#Acccom").change(function () {
        detAccCom()
        var opciones = '<option value="0"><<<<<-Seleccionar->>>>></option>';
        $("#grupCom").html(opciones)
        $("#Cate").html(opciones)
        $("#Products").html(opciones)


    })

var detAccCom = function () {
    $.getJSON('@Url.Action("LlenarDetAccComJSON", "AsesorVentasCall")', { idAccCom: $('#Acccom').val() },
        function (data) {
            var opciones = '<option value="0"><<<<<-Seleccionar->>>>></option>';
            $.each(data, function (i, item) {
                opciones += '<option value="' + item.DetAcc_ID + '">' + item.DetAcc_Nombre + '</option>'
            });
            $("#detAccCom").html(opciones);
        })
}
    $("#detAccCom").change(function () {
        grupCom()
        var opciones = '<option value="0"><<<<<-Seleccionar->>>>></option>';
        $("#Cate").html(opciones)
        $("#Products").html(opciones)
    })

var grupCom = function () {
    $.getJSON('@Url.Action("LlenarGrup_ComJSON", "AsesorVentasCall")', { id_detaCC_Com: $('#detAccCom').val() },
    function (data) {
        var opciones = '<option value="0"><<<<<-Seleccionar->>>>></option>';
        $.each(data, function (i, item) {
            opciones += '<option value="' + item.Gru_ID + '">' + item.Gru_Nombre + '</option>'
        });
        $("#grupCom").html(opciones);
    })
}
    $("#grupCom").change(function () {
        Cate()
        var opciones = '<option value="0"><<<<<-Seleccionar->>>>></option>';
        $("#Products").html(opciones)
    })

var Cate = function () {
    $.getJSON('@Url.Action("LlenarCatJSON", "AsesorVentasCall")', { id_Grup: $('#grupCom').val() },
 function (data) {
     var opciones = '<option value="0"><<<<<-Seleccionar->>>>></option>';
     $.each(data, function (i, item) {
         opciones += '<option value="' + item.Cat_Id + '">' + item.Cat_Nombre + '</option>'
     });
     $("#Cate").html(opciones);
 })
}
    $("#Cate").change(function () {
        Products();

    })

var Products = function () {
    $.getJSON('@Url.Action("LlenarProdJSON", "AsesorVentasCall")', { id_Cat: $('#Cate').val() },
 function (data) {
     var opciones = '<option value="0"><<<<<-Seleccionar->>>>></option>';
     $.each(data, function (i, item) {
         opciones += '<option value="' + item.Pro_ID + '">' + item.Pro_Nombre + '</option>'
     });
     $("#Products").html(opciones);
 })
}

    $("#MostrarForm").hide(true);
$("#Result").change(function () {
    if ($("#Result").val() == "1") {
        //  var result = '<div class="col-md-4 col-sm-6"><div class="card"><div class="card-body"><div class="form-group floating-label"><label for="regular2">Cliente</label><input type="text" class="form-control" id="regular2" name="cliente"></div><div class="form-group floating-label"><label for="regular2">RUC</label><input type="text" class="form-control" id="regular2" name="ruc"></div><div class="form-group floating-label"><label for="regular2">Empresa</label><input type="text" class="form-control" id="regular2" name="empresa"></div><div class="form-group floating-label"><label for="regular2">Direccion</label><input type="text" class="form-control" id="regular2" name="direccion"></div><div class="form-group"><label for="regular1">Accion</label><select id="dept" name="depto" class="form-control"><option value="0"><<<<<-Seleccionar->>>>></option></select></div><div class="form-group"><label for="regular1">Detalle</label><select id="prov" name="provin" class="form-control"><option value="0"><<<<<-Seleccionar->>>>></option></select></div><div class="form-group"><label for="regular1">Grupo</label><select id="dist" name="distrit" class="form-control"><option value="0"><<<<<-Seleccionar->>>>></option></select></div><div class="form-group floating-label"><label for="regular2">Fecha Nacimiento</label><input type="text" class="form-control" id="regular2" name="fnacim"></div><div class="form-group floating-label"><label for="regular2">Lugar Nacimiento</label><input type="text" class="form-control" id="regular2" name="lugnacimi"></div><div class="form-group floating-label"><label for="regular2">Correo</label><input type="text" class="form-control" id="regular2" name="correo"></div><div class="form-group floating-label"><label for="regular2">Telefono Ref</label><input type="text" class="form-control" id="regular2" name="telefref"></div></div></div></div>'
        $("#MostrarForm").show();
        //dept();
    } else {

        $("#MostrarForm").hide(true);
    }
});

/*<script type="text/javascript">
        var dept = function () {
            $.getJSON('@Url.Action("LlenarDeptJSON", "AsesorVentasCall")',
         function (data) {
             var opciones ='<option value="0"><<<<<-Seleccionar->>>>></option>';
             $.each(data, function (i, item) {

                 opciones += '<option value="' + item.idDepa + '">' + item.departamento + '</option>'
             });
             $("#dept").html(opciones);
         })
        }
    </script>*/

    $("#dept").change(function () {
        prov();
        var opciones = '<option value="0"><<<<<-Seleccionar->>>>></option>';
        $("#prov").html(opciones)

    })

var prov = function () {
    $.getJSON('@Url.Action("LlenarProvJSON", "AsesorVentasCall")', { iddepat: $('#dept').val() },
 function (data) {
     var opciones = '<option value="0"><<<<<-Seleccionar->>>>></option>';
     $.each(data, function (i, item) {
         opciones += '<option value="' + item.idProv + '">' + item.provincia + '</option>'
     });
     $("#prov").html(opciones);
 })
}

    $("#prov").change(function () {
        dist();
        var opciones = '<option value="0"><<<<<-Seleccionar->>>>></option>';
        $("#dist").html(opciones)
    })

var dist = function () {
    $.getJSON('@Url.Action("LlenarDistJSON", "AsesorVentasCall")', { idprov: $('#prov').val() },
 function (data) {
     var opciones = '<option value="0"><<<<<-Seleccionar->>>>></option>';
     $.each(data, function (i, item) {
         opciones += '<option value="' + item.idDist + '">' + item.distrito + '</option>'
     });
     $("#dist").html(opciones);
 })
}

/*<script type="text/javascript">
    function Add() {
        $("#tblData tbody").append(
            "<tr>" + "<td><input type='text'/></td>" +
            "<td><input type='text'/></td>" +
            "<td><input type='text'/></td>" +
            "<td><img src='/assets/img/delete.png' class='btnDelete'/></td>" + "</tr>");
        $(".btnSave").bind("click", Save);
        $(".btnDelete").bind("click", Delete);
    };
</script>

<script type="text/javascript">
    function Save() {
        var par = $(this).parent().parent();
        var tdName = par.children("td:nth-child(1)");
        var tdPhone = par.children("td:nth-child(2)");
        var tdEmail = par.children("td:nth-child(3)");
        var tdButtons = par.children("td:nth-child(4)");

        tdName.html(tdName.children("input[type=text]").val());
        tdPhone.html(tdPhone.children("input[type=text]").val());
        tdEmail.html(tdEmail.children("input[type=text]").val());
        tdButtons.html("<img src='/assets/img/delete.png' class='btnDelete'/>");

        $(".btnEdit").bind("click", Edit);
        $(".btnDelete").bind("click", Delete);
    };


</script>

<script type="text/javascript">
    function Edit() {
        var par = $(this).parent().parent();
        var tdName = par.children("td:nth-child(1)");
        var tdPhone = par.children("td:nth-child(2)");
        var tdEmail = par.children("td:nth-child(3)");
        var tdButtons = par.children("td:nth-child(4)");

        tdName.html("<input type='text' id='txtName' value='" + tdName.html() + "'/>");
        tdPhone.html("<input type='text' id='txtPhone' value='" + tdPhone.html() + "'/>");
        tdEmail.html("<input type='text' id='txtEmail' value='" + tdEmail.html() + "'/>");
        tdButtons.html("");

        $(".btnSave").bind("click", Save);
        $(".btnEdit").bind("click", Edit);
        $(".btnDelete").bind("click", Delete);
    };
</script>

<script type="text/javascript">
    function Delete() {
        var par = $(this).parent().parent(); 
        par.remove();
    };
</script>

<script type="text/javascript">
    $(function(){ //Add, Save, Edit and Delete functions code 
        $(".btnEdit").bind("click", Edit);
        $(".btnDelete").bind("click", Delete);
        $("#btnAdd").bind("click", Add);
    });
</script>*/

$(document).ready(function ($) {
    // delegate calls to data-toggle="lightbox"
    $(document).delegate('*[data-toggle="lightbox"]:not([data-gallery="navigateTo"])', 'click', function (event) {
        event.preventDefault();
        return $(this).ekkoLightbox({
            onShown: function () {
                if (window.console) {
                    return console.log('Checking our the events huh?');
                }
            },
            onNavigate: function (direction, itemIndex) {
                if (window.console) {
                    return console.log('Navigating ' + direction + '. Current item: ' + itemIndex);
                }
            }
        });
    });
});