(function () {
    angular.module('app').controller('app.views.dataentry.spareModal', [
        '$scope', '$modalInstance', 'abp.services.app.ticket',
        function ($scope, $modalInstance, ticketService) {
            var vm = this;

            vm.spare = {
                name: '',
                frenchName:'',
            };

            vm.save = function () {
                abp.ui.setBusy();
                ticketService.createSpareAsync(vm.spare)
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