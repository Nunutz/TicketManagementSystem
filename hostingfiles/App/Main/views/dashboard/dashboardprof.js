(function() {
    var controllerId = 'app.views.dashboard.dashboardprof';
    angular.module('app').controller(controllerId, [
        '$scope', '$modal', 'abp.services.app.batchTicket', function ($scope, $modal, batchTicketService) {

            var vm = this;
            vm.tickets = [];
            $scope.ticketId = 0;
            // call ticket service
            function getTickets() {
                abp.ui.setBusy();
                batchTicketService.getBatchTicketsArray({})
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
                        { data: "mobileNumber" },
                        { data: "noofMobiles" },
                        //{ data: "status" },                       
                        { data: "createdDate" },
                        { data: "action" },
                        
                    ],

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
                    templateUrl: '/App/Main/views/professional/ticket.cshtml',
                    controller: 'app.views.profticket.ticket as vm',
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
