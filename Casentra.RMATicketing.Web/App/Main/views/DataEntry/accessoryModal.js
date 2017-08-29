(function () {
    angular.module('app').controller('app.views.dataentry.accessoryModal', [
        '$scope', '$modalInstance', 'abp.services.app.ticket',
        function ($scope, $modalInstance, ticketService) {
            var vm = this;

            vm.accessory = {
                name: '',
                frenchName:'',
            };

            vm.save = function () {
                abp.ui.setBusy();
                ticketService.createAccessoryAsync(vm.accessory)
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