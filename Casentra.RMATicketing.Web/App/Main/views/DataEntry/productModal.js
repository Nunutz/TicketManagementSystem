(function () {
    angular.module('app').controller('app.views.dataentry.productModal', [
        '$scope', '$modalInstance', 'abp.services.app.ticket',
        function ($scope, $modalInstance, ticketService) {
            var vm = this;

            vm.product = {
                name: '',
                brand:'',
            };

            vm.brands = [];

            function getDateEntries() {
                ticketService.getDataEntries({}).success(function (result) {
                    vm.brands = result.brands;
                    
                });
            }

            vm.save = function () {
                abp.ui.setBusy();
                ticketService.createProductAsync(vm.product)
                    .success(function () {
                        abp.notify.info(App.localize('SavedSuccessfully'));
                        $modalInstance.close();
                    }).finally(function () {
                        abp.ui.clearBusy();
                    });
            };

            vm.cancel = function () {
                $modalInstance.dismiss();
            };

            getDateEntries();
        }
    ]);
})();