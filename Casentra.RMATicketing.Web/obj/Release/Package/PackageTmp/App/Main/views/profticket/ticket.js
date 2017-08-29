(function() {
    var controllerId = 'app.views.profticket.ticket';
    angular.module('app').controller(controllerId, [
        '$scope', '$modal', '$modalInstance', 'abp.services.app.batchTicket', 'ticketId', function ($scope, $modal,$modalInstance, batchTicketService, ticketId) {

            var vm = this;
                                             
            vm.ticketStatuses = [];
            vm.ticketPriorities = [];
            vm.ticketBoards = [];

            vm.batchItems = [];
            vm.spareParts = [];

            vm.ticket = {

                ticketId: '',
                ticketNo: '',
                customerId: '',
                firstName: '',
                lastName: '',
                address: '',
                city: '',
                zipcode: '',
                
                mobileNumber: '',
                emailId: '',
                issueSummary: '',

                tickeStatus: '',
                ticketStatusName: '',
                ticketPriority: '',
                ticketBoard: '',
                createdDate:'',
                
            };
            
            function getLookUps() {
                batchTicketService.getLookups({}).success(function (result) {
                    vm.ticketStatuses = result.ticketStatus;
                    vm.ticketPriorities = result.ticketPriority;                    
                });
            }

            function getTicket(ticketId) {
                
                var data = {
                    id: ticketId
                }
                batchTicketService.getBatchTicketObject(data).success(function (result) {

                    vm.ticket.ticketId=result.ticketId;
                    vm.ticket.customerId = result.customerId;
                    vm.ticket.firstName = result.firstName;
                    vm.ticket.lastName = result.lastName;
                    vm.ticket.address = result.address;
                    vm.ticket.city = result.city;
                    vm.ticket.zipcode = result.zipcode;
                    vm.ticket.mobileNumber = result.mobileNumber;
                    vm.ticket.email = result.email;

                    vm.ticket.issueSummary = result.issueSummary;

                    vm.ticket.ticketStatus = result.ticketStatus;
                    vm.ticket.ticketPriority = result.ticketPriority;
                    vm.ticket.ticketBoard = result.ticketBoard;

                    vm.ticket.createdDate = result.createdDate;
                    vm.ticket.ticketNo = result.ticketNo;
              
                    vm.batchItems = result.batchItems;
                    vm.spareParts = result.spareParts;

                });
            }

            vm.cancel = function () {
                $modalInstance.dismiss();
            };
            //open the popup
            vm.openTicketModal = function (batchItemId) {

                var modalInstance = $modal.open({
                    templateUrl: '/App/Main/views/profticket/updateticket.cshtml',
                    controller: 'app.views.profticket.updateticket as vm',
                    backdrop: 'static',
                    resolve:
                        {
                            ticketId: function () { return batchItemId; }
                        },
                });
                            
            };
                           
            //update ticket
            vm.updateSpare = function (spare) {
                abp.ui.setBusy();
                batchTicketService.updateSpare(spare)
                    .success(function () {
                        abp.notify.info(App.localize('SavedSuccessfully'));
                        $modalInstance.dismiss();
                    }).finally(function () {
                        abp.ui.clearBusy();

                    });
            }


            //load at initial stage
            getLookUps();
            getTicket(ticketId);
        }
    ]);
})();
