(function() {
    var controllerId = 'app.views.imei.imeilist';
    angular.module('app').controller(controllerId, [
        '$scope', 'abp.services.app.imeiNumber', function ($scope, imeiNumberService) {

            var vm = this;
            vm.imeiList = [];
            
            function getImeiNumbers() {
                abp.ui.setBusy();
                imeiNumberService.getImeiNumberArray({})
               .success(function (result)
                {
                   vm.imeiList = result.imeiData;
                   loadImeiNumbers(result.imeiData);

               }).finally(function () {
                   abp.ui.clearBusy();
               });
            }

            //load the imei numbers
            function loadImeiNumbers(dataSet) {
                var table= $('#datatable-responsive').DataTable({
                    data: dataSet,
                    columns: [
                        { data: "imeiNumber" },                  
                        { data: "product" },
                        { data: "purchasedDate" },
                        
                    ],
                    
                });

            }

            //load at initial stage
            getImeiNumbers();
                       
            
        }
    ]);
})();
