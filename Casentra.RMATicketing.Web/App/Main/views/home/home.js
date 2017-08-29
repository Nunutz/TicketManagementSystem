(function () {
    angular.module('app').controller('app.views.home', [
        '$scope','abp.services.app.report',
        function ($scope, reportService) {
            var vm = this;

            vm.ticketCounts = [];
            vm.ticketBatchItemCounts = [];
            vm.ticketData = [];
            vm.ticketBatchData = [];

            function getReportData() {

                reportService.getTicketCountByStatus({})
                    .success(function (result) {
                        vm.ticketCounts = result.ticketCounts;
                    });

                reportService.getBatchTicketCountByStatus({})
                    .success(function (result) {
                        vm.ticketBatchItemCounts = result.ticketCounts;
                    });
            }

            //load all report data
            getReportData();
        }
    ]);
})();