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

            var table = $("#tableFlight").DataTable({
                "scrollCollapse": true,
                "autoWidth": false,
                "responsive": true,
                "processing": true,
                "serverSide": true,
                "searching": false,
                "ordering": false,
                "ajax":
                {
                    url: '/Flight/SearchFlight',
                    type: "GET",
                    data: function (d) {
                        d.txtOrigin = $("#txtOrigin").val(),
                            d.txtDestination = $("#txtDestination").val(),
                            d.txtDate = $("#txtDate").val()
                    }
                },
                aoColumnDefs: [
                    {
                        "mDataProp": "DepartureStation",
                        "aTargets": [0]
                    },
                    {
                        "mDataProp": "ArrivalStation",
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
                        "mDataProp": "Price",
                        "aTargets": [4],
                        "render": function (data, type, row) {
                            return '<a href="/Flight/SaleFlight/?ArrivalStation=' + row.ArrivalStation + '&DepartureStation=' + row.DepartureStation + '&DepartureDate=' + row.DepartureDate + '&Price=' + row.Price + '&Currency=' + row.Currency + '">Comprar vuelo</a>';
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