(function (page) {
    var searchFlightController = function () {
        var initializeEvents = function () {
        }
        return {
            Initialize: function () {
                initializeEvents();
            }
        };
    }();

    var searchFlightView = function () {

        var oSearchTable;

        var updateTable = function () {
            oSearchTable.ajax.reload();
        };

        function bindButtons() {

            $('#btnBuscar').click(function () {
                updateTable();
            });

        }


        var createTable = function () {

            var table = $("#tableSaleFlight").DataTable({
                "scrollCollapse": true,
                "autoWidth": false,
                "responsive": true,
                "processing": true,
                "serverSide": true,
                "searching": false,
                "ordering": false,
                "ajax":
                {
                    url: '/Flight/GetAllFlightSale',
                    type: "GET",
                    data: function (d) {
                        d.txtOrigin = $("#txtCode").val(),
                            d.txtDestination = $("#txtCode").val(),
                            d.txtDate = $("#txtCode").val()
                    }
                },
                aoColumnDefs: [
                    {
                        "mDataProp": "ArrivalStation",
                        "aTargets": [0]
                    },
                    {
                        "mDataProp": "DepartureStation",
                        "aTargets": [1]
                    },
                    {
                        "mDataProp": "DepartureDate",
                        "aTargets": [2]
                    },
                    {
                        "mDataProp": "Price",
                        "aTargets": [3]
                    },
                    {
                        "mDataProp": "TransportNumber",
                        "aTargets": [4]
                    },
                    {
                        "mDataProp": "Price",
                        "aTargets": [5],
                        "render": function (data, type, row) {
                            debugger
                            return '<a href="/Flight/EditSaleFlight/?ArrivalStation=' + row.ArrivalStation + '&DepartureStation=' + row.DepartureStation + '&DepartureDate=' + row.DepartureDate + '&Price=' + row.Price + '&Currency=' + row.Currency + '&FlightNumber=' + row.TransportNumber+'">editar vuelo</a>' +
                                ' | <a href="/Flight/SaleFlight/?ArrivalStation=' + row.ArrivalStation + '&DepartureStation=' + row.DepartureStation + '&DepartureDate=' + row.DepartureDate + '&Price=' + row.Price + '&Currency=' + row.Currency + '">descargar</a>';
                        }
                    },
                ],
                "fnDrawCallback": function (oSettings) {
                    $('tbody tr').attr('class', '');
                },
            });

            return table;

        }

        var initialize = function () {
            bindButtons();
            oSearchTable = createTable();
        };

        return {
            Initialize: initialize
        };
    }();

    page(window.jQuery, window, document, searchFlightController, searchFlightView);
}(function ($, window, document, searchFlightController, searchFlightView) {
    $(function () {
        searchFlightView.Initialize();
        searchFlightController.Initialize();
    });
}));