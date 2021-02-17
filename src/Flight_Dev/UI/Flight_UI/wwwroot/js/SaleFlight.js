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

            $('#btnAdd').click(function () {

                debugger

                var firstName = $("#txtFirstName").val();
                var lastName = $("#txtLastName").val();
                var phone = $("#txtPhone").val();
                var email = $("#txtEmail").val();

                if (firstName === "") {
                    alert("Ingresar nombres");
                }

                if (lastName === "") {
                    alert("Ingresar apellidos");
                }

                if (phone === "") {
                    alert("Ingresar telefono");
                }

                if (email === "") {
                    alert("Ingresar correo electrónico");
                }

                var newRowContent = '<tr>' +
                    '<td>' + firstName + '</td>' +
                    '<td>' + lastName + '</td>' +
                    '<td>' + phone + '</td>' +
                    '<td>' + email + '</td>' +
                    '</tr>';

                $("#tableClient tbody").append(newRowContent);

            });

            $("#btnCheckOut").click(function () {

                var origin = $("#txtOrigin").val();
                var destination = $("#txtDestination").val();
                var date = $("#txtDate").val();
                var price = $("#txtPrice").val();
                var currency = $("#txtCurrency").val();

                debugger

                var clients = [];
                $("#tableClient > tbody > tr").each(function (indx, tr) {

                    debugger
                    var FirstName = $(this).find("td").eq(0).html();
                    var lastName = $(this).find("td").eq(1).html();
                    var phone = $(this).find("td").eq(2).html();
                    var email = $(this).find("td").eq(3).html();

                    clients.push({
                        FirstName: FirstName,
                        LastName: lastName,
                        Phone: phone,
                        Email: email
                    });
                });

                debugger

                var dataJson = {
                    DepartureStation: origin,
                    ArrivalStation: destination,
                    DepartureDate: date,
                    Price: price,
                    Currency: currency,
                    ListClient: clients
                }

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
                            url: '/Flight/SaleFlighRegistration',
                            type: "POST",
                            contentType: "application/json",
                            data: JSON.stringify(dataJson),
                            success: function (result) {

                                debugger

                                if (result.success) {

                                    $.unblockUI();
                                    swal({
                                        title: "Exito!",
                                        text: "Compra exitosa",
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