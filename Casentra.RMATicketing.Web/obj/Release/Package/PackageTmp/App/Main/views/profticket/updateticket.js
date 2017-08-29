(function () {
    var controllerId = 'app.views.profticket.updateticket';
    angular.module('app').controller(controllerId, [
        '$scope', '$modalInstance', 'abp.services.app.batchTicket', 'ticketId', function ($scope, $modalInstance, ticketBatchService, ticketId) {

            var vm = this;

            $scope.dateOptions = {
                dateFormat: 'mm/dd/yy',
            }
            
            vm.ticketStatuses = [];
            vm.ticketPriorities = [];
            vm.ticketBoards = [];
            vm.ticketNotes = [];

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

                ticketStatus: '',
                ticketPriority: '',
                ticketBoard: '',
                product: '',
                brand: '',
                color: '',
                capacity: '',
                phoneCondition: '',
                accessory: '',
                boughtAt: '',
                password: '',
                icloudAddress: '',
                icloudPassword: '',
                note: '',
                imeiNumber: '',
                createdDate: '',
                purchasedDate: '',
                issueSummary: '',
                closedDate: '',
             
                phoneProblem: '',
                phoneProblemsInfrench: '',
                phoneProblemsInChinese: '',
               
                ticketPriorityId: '',
                ticketStatusId: '',
                batchItemId: '',
                trackingNumber:''
            };

            function getLookUps() {
                ticketBatchService.getLookups({}).success(function (result) {

                    vm.ticketStatuses = result.ticketStatus;
                    vm.ticketPriorities = result.ticketPriority;
                    vm.ticketBoards = result.ticketBoard;
                   
                });
            }

            function getTicket(ticketId) {
                
                var data = {
                    id: ticketId
                }
                ticketBatchService.getTicketObject(data).success(function (result) {

                    vm.ticket.batchItemId = result.batchItemId;
                    vm.ticket.ticketId = result.ticketId;
                    vm.ticket.customerId = result.customerId;

                    vm.ticket.product = result.product;
                    vm.ticket.brand = result.brand;
                    vm.ticket.color = result.color;
                    vm.ticket.capacity= result.capacity;
                    vm.ticket.phoneCondition = result.phoneCondition;
                 
                    vm.ticket.accessory = result.accessory;
                    vm.ticket.boughtAt = result.boughtAt;
                    vm.ticket.password = result.password;
                    vm.ticket.icloudAddress = result.icloudAddress;
                    vm.ticket.icloudPassword = result.icloudPassword;
                    vm.ticket.previousNotes = result.notes;
                    vm.ticket.imeiNumber = result.imeiNumber;
                    vm.ticket.createdDate = result.createdDate;
                    vm.ticket.purchasedDate = result.purchasedDate;
                    vm.ticket.issueSummary = result.issueSummary;

                    vm.ticket.ticketStatus = result.ticketStatus;
                    vm.ticket.ticketPriority = result.ticketPriority;
                    vm.ticket.ticketBoard = result.ticketBoard;

                    vm.ticket.ticketNo = result.ticketNo;
                    vm.ticket.closedDate = result.closedDate;
                    vm.ticket.phoneProblem = result.phoneProblem;
                    vm.ticket.phoneProblemInfrench = result.phoneProblemInFrench;
                    vm.ticket.phoneProblemInChinese = result.phoneProblemInChinese;
                
                    vm.ticketNotes = result.notes;
                    vm.ticket.trackingNumber = result.trackingNumber;

                    vm.ticket.ticketStatusId = result.ticketStatusId;
                    
                });
            }

            vm.cancel = function () {
                $modalInstance.dismiss();
            };

            //update ticket
            vm.save = function () {
                abp.ui.setBusy();
                ticketBatchService.updateTicket(vm.ticket)
                    .success(function () {
                        abp.notify.info(App.localize('SavedSuccessfully'));
                        $modalInstance.dismiss();
                    }).finally(function () {
                        abp.ui.clearBusy();

                    });
            };

            vm.printEng = function () {

                var printContents = document.getElementById('divPrintEng').innerHTML;
                var popupWin = window.open('', '_blank', 'width=300,height=300,scrollbars=no,menubar=no,toolbar=no,location=no,status=no,titlebar=no,date=no');
                popupWin.document.open();
                popupWin.document.write('<html><head><link rel="stylesheet" type="text/css" href="https://suivi-rma.com/content/bootstrap.min.css" /></head><body onload="window.print()">' + printContents + '</body></html>');
                popupWin.document.close();

            };

            //load at initial stage
            getLookUps();
            getTicket(ticketId);
        }
    ]);
})();
