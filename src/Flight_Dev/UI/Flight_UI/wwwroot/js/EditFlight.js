(function (page) {
    var saleFlightController = function () {
        var initializeEvents = function () {
        }
        return {
            Initialize: function () {
                initializeEvents();
            }
        };
    }();

    var saleFlightView = function () {

        function bindButtons() {

            $('#btnSave').click(function () {

                debugger
                var client = [];
                $("#tableEdit > tbody > tr").each(function (indx, tr) {

                    var input = $(this).find('input');
                    var id = input.eq(0).val();
                    var phone = input.eq(3).val();
                    var email = input.eq(4).val();
                    client.push({
                        Id: id,
                        Phone: phone,
                        Email: email,
                    });

                    swal({
                        title: 'Venta de vuelos',
                        text: "¿Desea registrar la compra?",
                        icon: 'warning',
                        showCancelButton: true,
                        confirmButtonColor: '#3085d6',
                        cancelButtonColor: '#d33',
                        confirmButtonText: 'Si, deseo comprar',
                        cancelButtonText: 'Cancelar'
                    }).then((result) => {
                        if (result.value) {


                            $.blockUI({ message: '<h1>Procesando...</h1>' });

                            debugger

                            $.ajax({
                                url: '/Flight/EditFlight',
                                type: "POST",
                                contentType: "application/json",
                                data: JSON.stringify(client),
                                success: function (result) {

                                    debugger

                                    if (result.success) {

                                        $.unblockUI();
                                        swal({
                                            title: "Exito!",
                                            text: "Cambios guardados",
                                            type: "success"
                                        }).then(function () {
                                            window.location.href = '/Flight/Index';
                                        });
                                    }
                                    else {

                                        $.unblockUI();
                                        swal({
                                            title: "Error!",
                                            text: result.mensaje,
                                            type: "warning"
                                        });
                                    }
                                }
                            });



                        }
                    });



                });

            });

        }

        var initialize = function () {
            bindButtons();
        };

        return {
            Initialize: initialize
        };
    }();

    page(window.jQuery, window, document, saleFlightController, saleFlightView);
}(function ($, window, document, saleFlightController, saleFlightView) {
    $(function () {
        saleFlightView.Initialize();
        saleFlightController.Initialize();
    });
}));