(function() {
    var controllerId = 'app.views.professional.ticket';
    angular.module('app').controller(controllerId, [
        '$scope', '$modalInstance', 'abp.services.app.ticket', 'ticketId', function ($scope, $modalInstance, ticketService, ticketId) {

            var vm = this;
            
            $scope.dateOptions = {
                dateFormat: 'mm/dd/yy',
            }
                       
            vm.ticketStatuses = [];
            vm.boughtAts = [];
            vm.boughtAtClients = [];
            vm.brands = [];
            vm.accessories = [];
            vm.products = [];
            vm.phoneConditions = [];
            vm.productColors = [];
            vm.phoneProblems = [];
            vm.capacities = [];
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
                phoneNumber: '',
                mobileNumber: '',
                emailId: '',

                ticketStatusId: '',
                ticketPriorityId: '',
                ticketBoardId: '',
                productId: '',
                brandId: '',
                colorId: '',
                capacityId: '',
                phoneConditionId: '',
                PhoneProblemId: '',
                accessoryId: '',
                boughtAtId: '',
                password: '',
                icloudAddress: '',
                icloudPassword: '',
                note: '',
                imeiNumber: '',
                createdDate:'',
                purchasedDate: '',
                issueSummary: '',

                tickeStatus: '',
                ticketPriority: '',
                ticketBoard: '',
                previousNote:'',
                previousNotes: [],
                closedDate: '',
                accessories: '',
                phoneProblems: '',
                phoneProblemsInfrench: '',
                phoneProblemsInChinese: '',
                productName: '',
                TrackingNumber:'',
            };
            
            function getLookUps() {
                ticketService.getLookups({}).success(function (result) {

                    vm.ticketStatuses = result.ticketStatus;
                    vm.ticketPriorities = result.ticketPriority;
                    vm.boughtAts = result.boughtAts;
                    vm.brands = result.brands;
                    vm.products = result.products;
                    vm.phoneConditions = result.phoneConditions;
                    vm.productColors = result.productColors;
                    vm.phoneProblems = result.phoneProblems;
                    vm.capacities = result.capacities;
                    vm.ticketBoards = result.ticketBoard;
                    vm.accessories = result.accesseries;
                    vm.boughtAtClients = result.boughtAtClients;

                    
                });
            }

            function getTicket(ticketId) {
                //alert(ticketId)
                var data = {
                    id: ticketId
                }
                ticketService.getTicketObject(data).success(function (result) {

                    vm.ticket.ticketId=result.ticketId;
                    vm.ticket.customerId = result.customerId;
                    vm.ticket.firstName = result.firstName;
                    vm.ticket.lastName = result.lastName;
                    vm.ticket.address = result.address;
                    vm.ticket.city = result.city;
                    vm.ticket.zipcode = result.zipcode;
                    vm.ticket.phoneNumber = result.phoneNumber;
                    vm.ticket.mobileNumber = result.mobileNumber;
                    vm.ticket.email = result.email;

                    vm.ticket.ticketStatusId = result.ticketStatusId;
                    vm.ticket.ticketPriorityId = result.ticketPriorityId;
                    vm.ticket.ticketBoardId = result.ticketBoardId;
                    vm.ticket.productId = result.productId;
                    vm.ticket.brandId = result.brandId;
                    vm.ticket.colorId = result.colorId;
                    vm.ticket.capacityId = result.capacityId;
                    vm.ticket.phoneConditionId = result.phoneConditionId;
                    vm.ticket.phoneProblemId = result.phoneProblemId;
                    vm.ticket.accessoryId = result.accessoryId;
                    vm.ticket.boughtAtId = result.boughtAtId;
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
                    
                    vm.ticket.previousNotes == result.previousNotes;
                    vm.ticket.previousNote = result.previousNote;
                    vm.ticket.ticketNo = result.ticketNo;
                    vm.ticket.closedDate = result.closedDate;
                    vm.ticket.accessories=result.accessories;
                    vm.ticket.phoneProblems = result.phoneProblems;
                    vm.ticket.phoneProblemsInfrench = result.phoneProblemsInFrench;
                    vm.ticket.phoneProblemsInChinese = result.phoneProblemsInChinese;
                    vm.ticket.productName = result.productName;
                    vm.ticket.trackingNumber = result.trackingNumber;
                    vm.ticket.isProfessional = true;
                });
            }

            vm.cancel = function () {
                $modalInstance.dismiss();
            };
            
            //update ticket
            vm.save = function () {
                abp.ui.setBusy();
                ticketService.updateTicket(vm.ticket)
                    .success(function () {
                        abp.notify.info(App.localize('SavedSuccessfully'));
                        $modalInstance.close();
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
