(function () {
    angular.module('app').controller('app.views.dataentry.brandModal', [
        '$scope', '$modalInstance', 'abp.services.app.ticket',
        function ($scope, $modalInstance, ticketService) {
            var vm = this;

            vm.brand = {
                   name: '',              
            };

            vm.save = function () {
                abp.ui.setBusy();
                ticketService.createBrandAsync(vm.brand)
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
        }
    ]);
})();