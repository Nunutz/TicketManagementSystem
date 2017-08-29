(function() {
    angular.module('app').controller('app.views.report.index', [
        '$scope', '$modal', 'abp.services.app.report',
        function ($scope, $modal, reportService) {
            var vm = this;

            $scope.dateOptions = {
                dateFormat: 'dd/mm/yy',
            }

            vm.ticketCounts = [];
            vm.products = [];
            vm.ticketStatus = [];

            vm.ticket = {
                modelId: '',
                statusId: '',
                fromDate: '',
                toDate:'',
            };
                        

            vm.ticketBatchItemCounts = [];
            vm.ticketData = [];          
            vm.ticketBatchData = [];            

            function getReportData() {

                reportService.getSearchObjects({})
                    .success(function (result) {
                        vm.products = result.products;
                        vm.ticketStatus = result.ticketStatus;
                    });

            }         
            
            //search data
            vm.searchData = function (ticket) {
                abp.ui.setBusy();
                reportService.getTicketsArray(ticket)
                    .success(function (result) {
                        vm.ticketData = result.searchResult;
                        //loadTickets(result.searchResult);
                    }).finally(function () {
                        abp.ui.clearBusy();
                    });
            }

            //load the report
            function loadTickets(dataSet) {
                var table = $('#datatable').DataTable({
                    data: dataSet,
                    columns: [
                        { data: "ticketNo" },
                        { data: "customerName" },
                        { data: "email" },
                        { data: "imeiNumber" },
                        { data: "mobileNumber" },
                        { data: "product" },                       
                        { data: "brand" },                        
                        { data: "createdDate" },

                    ],
                });

            }

            //load all report data
            getReportData();
        }
    ]);
})();