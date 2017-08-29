(function() {
    var controllerId = 'app.views.dashboard.dashboard';
    angular.module('app').controller(controllerId, [
        '$scope', '$modal', 'abp.services.app.ticket', function ($scope, $modal,ticketService) {

            var vm = this;
            vm.tickets = [];
            $scope.ticketId = 0;
            // call ticket service
            function getTickets() {
                abp.ui.setBusy();
                ticketService.getTicketsArray({})
               .success(function (result)
                {
                   vm.tickets = result.ticketData;
                   loadTickets(result.ticketData);

               }).finally(function () {
                   abp.ui.clearBusy();
               });
            }

            //load the tickets
            function loadTickets(dataSet) {
                var table= $('#datatable-responsive').DataTable({
                    data: dataSet,
                    columns: [
                        { data: "ticketNo" },
                        { data: "customerName" },
                        { data: "email" },
                        { data: "imeiNumber" },
                        { data: "mobileNumber" },
                        { data: "product" },
                        { data: "status" },
                        { data: "brand" },
                        
                        { data: "phoneLocation" },
                        { data: "createdDate" },
                        { data: "action" },
                        
                    ],
                    
                    //"columnDefs": [{
                    //    "targets": -1,
                    //    "data": null,
                    //    "defaultContent": "<button>View Ticket!</button>"
                    //}]
                    //"columnDefs": [
                    //        {
                    //            "targets": [0],
                    //            "visible": false,
                                
                    //        },
                    //        {
                    //            "targets": [2],
                    //            "visible": false
                    //        }
                    //]
                });

               
                $('#datatable-responsive tbody').on('click', 'button', function () {
                    var ticketId = $(this).attr("data-id");
                     //alert(ticketId);
                    abp.ui.setBusy();
                    vm.openTicketModal(ticketId);
                    abp.ui.clearBusy();
                });

            }

            vm.openTicketModal = function (ticketId) {
                var modalInstance = $modal.open({
                    templateUrl: '/App/Main/views/tickets/ticket.cshtml',
                    controller: 'app.views.tickets.ticket as vm',
                    backdrop: 'static',
                    resolve:
                        {
                            ticketId: function () { return ticketId; }
                        },
                    
                });

                modalInstance.result.then(function () {
                    getTickets();
                });
            };


            //load at initial stage
            getTickets();
                       
            
        }
    ]);
})();
