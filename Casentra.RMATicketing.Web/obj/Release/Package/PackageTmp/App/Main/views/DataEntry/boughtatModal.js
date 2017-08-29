(function () {
    angular.module('app').controller('app.views.dataentry.boughtatModal', [
        '$scope', '$modalInstance', 'abp.services.app.ticket',
        function ($scope, $modalInstance, ticketService) {
            var vm = this;

            vm.boughtat = {
                name: '',
                frenchName:'',
            };

            vm.save = function () {
                abp.ui.setBusy();
                ticketService.createBoughtAtAsync(vm.boughtat)
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