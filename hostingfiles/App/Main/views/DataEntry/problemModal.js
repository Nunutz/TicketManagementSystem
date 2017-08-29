(function () {
    angular.module('app').controller('app.views.dataentry.problemModal', [
        '$scope', '$modalInstance', 'abp.services.app.ticket',
        function ($scope, $modalInstance, ticketService) {
            var vm = this;

            
            vm.problem = {
                    id: '',   
                    name: '',
                    frenchName: '',
                    chineseName: '',
            };

            vm.save = function () {
                abp.ui.setBusy();

                ticketService.createProblemAsync(vm.problem)
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