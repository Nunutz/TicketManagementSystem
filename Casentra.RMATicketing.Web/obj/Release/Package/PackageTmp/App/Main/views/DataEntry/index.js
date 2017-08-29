(function() {
    angular.module('app').controller('app.views.dataentry.index', [
        '$scope', '$modal', 'abp.services.app.ticket',
        function ($scope, $modal, ticketService) {
            var vm = this;

            vm.brands = [];
            vm.products = [];
            vm.problems = [];
            vm.accessories = [];
            vm.boughtats = [];
            vm.spares = [];

            vm.editingData = {};
            vm.editingBrandData = {};
            vm.editingProductData = {};
            vm.editingAccesoryData = {};
            vm.editingBoughtAtData = {};
            vm.editingSparesData = {};

            //de select all- phone problems
            for (var i = 0, length = vm.problems.length; i < length; i++) {
                vm.editingData[vm.problems[i].id] = false;
            }
                       
            //de select all- brands
            for (var i = 0, length = vm.brands.length; i < length; i++) {
                vm.editingBrandData[vm.brands[i].id] = false;
            }

            //de select all- products
            for (var i = 0, length = vm.products.length; i < length; i++) {
                vm.editingProductData[vm.products[i].id] = false;
            }

            //de select all- accessories
            for (var i = 0, length = vm.accessories.length; i < length; i++) {
                vm.editingAccesoryData[vm.accessories[i].id] = false;
            }
            //de select all- bought ats
            for (var i = 0, length = vm.boughtats.length; i < length; i++) {
                vm.editingBoughtAtData[vm.boughtats[i].id] = false;
            }

            //de select all- spares
            for (var i = 0, length = vm.spares.length; i < length; i++) {
                vm.editingSparesData[vm.spares[i].id] = false;
            }


            function getDateEntries() {
                ticketService.getDataEntries({}).success(function (result) {
                    vm.brands = result.brands;
                    vm.products = result.products;
                    vm.problems = result.problems;
                    vm.accessories = result.accessories;
                    vm.boughtats = result.boughtats;
                    vm.spares = result.spares;
                });
            }

            vm.openBrandCreationModal = function() {
                var modalInstance = $modal.open({
                    templateUrl: '/App/Main/views/dataentry/brand.cshtml',
                    controller: 'app.views.dataentry.brandModal as vm',
                    backdrop: 'static'
                });

                modalInstance.result.then(function () {
                    getDateEntries();
                });
            };


            vm.openProductCreationModal = function () {
                var modalInstance = $modal.open({
                    templateUrl: '/App/Main/views/dataentry/product.cshtml',
                    controller: 'app.views.dataentry.productModal as vm',
                    backdrop: 'static'
                });

                modalInstance.result.then(function () {
                    getDateEntries();
                });
            };

            vm.openProblemCreationModal = function () {
                var modalInstance = $modal.open({
                    templateUrl: '/App/Main/views/dataentry/problem.cshtml',
                    controller: 'app.views.dataentry.problemModal as vm',
                    backdrop: 'static',
                    

                });

                modalInstance.result.then(function () {
                    getDateEntries();
                });
            };

            vm.openaccessoryCreationModal = function () {
                var modalInstance = $modal.open({
                    templateUrl: '/App/Main/views/dataentry/accessory.cshtml',
                    controller: 'app.views.dataentry.accessoryModal as vm',
                    backdrop: 'static'
                });

                modalInstance.result.then(function () {
                    getDateEntries();
                });
            };

            vm.openboughAtCreationModal = function () {
                var modalInstance = $modal.open({
                    templateUrl: '/App/Main/views/dataentry/boughtat.cshtml',
                    controller: 'app.views.dataentry.boughtatModal as vm',
                    backdrop: 'static'
                });

                modalInstance.result.then(function () {
                    getDateEntries();
                });
            };
               
            vm.openSpareCreationModal = function () {
                var modalInstance = $modal.open({
                    templateUrl: '/App/Main/views/dataentry/spare.cshtml',
                    controller: 'app.views.dataentry.spareModal as vm',
                    backdrop: 'static'
                });

                modalInstance.result.then(function () {
                    getDateEntries();
                });
            };

            //phone problems
            vm.modify = function (prob) {
                 vm.editingData[prob.id] = true;
            };

            vm.update = function (prob) {
               
                vm.editingData[prob.id] = false;
                abp.ui.setBusy();
                ticketService.updatePhoneProblemAsync(prob)
                    .success(function () {
                        abp.notify.info(App.localize('SavedSuccessfully'));
                        getDateEntries();
                    }).finally(function () {
                        abp.ui.clearBusy();
                    });
            };

            vm.remove = function (prob) {
                abp.ui.setBusy();
                ticketService.deletePhoneProblemAsync({ id: prob.id })
                     .success(function () {
                         abp.notify.info(App.localize('SavedSuccessfully'));
                         getDateEntries();
                     }).finally(function () {
                         abp.ui.clearBusy();
                     });
            };
            //end phone problems

            //brand
            vm.modifyBrand = function (brand) {
                vm.editingBrandData[brand.id] = true;
            };

            vm.updateBrand = function (brand) {

                vm.editingBrandData[brand.id] = false;
                abp.ui.setBusy();
                ticketService.updateBrandAsync(brand)
                    .success(function () {
                        abp.notify.info(App.localize('SavedSuccessfully'));
                        getDateEntries();
                    }).finally(function () {
                        abp.ui.clearBusy();
                    });
            };

            vm.removeBrand = function (brand) {
                abp.ui.setBusy();
                ticketService.deleteBrandAsync({ id: brand.id })
                     .success(function () {
                         abp.notify.info(App.localize('SavedSuccessfully'));
                         getDateEntries();
                     }).finally(function () {
                         abp.ui.clearBusy();
                     });
            };
            //end brands


            //products
            vm.modifyProduct = function (product) {
                vm.editingProductData[product.id] = true;
            };

            vm.updateProduct = function (product) {

                vm.editingProductData[product.id] = false;
                abp.ui.setBusy();
                ticketService.updateProductAsync(product)
                    .success(function () {
                        abp.notify.info(App.localize('SavedSuccessfully'));
                        getDateEntries();
                    }).finally(function () {
                        abp.ui.clearBusy();
                    });
            };

            vm.removeProduct = function (product) {
                abp.ui.setBusy();
                ticketService.deleteProductAsync({ id: product.id })
                     .success(function () {
                         abp.notify.info(App.localize('SavedSuccessfully'));
                         getDateEntries();
                     }).finally(function () {
                         abp.ui.clearBusy();
                     });
            };
            //end products


            //Accessories
            vm.modifyAccesory = function (accesory) {
                vm.editingAccesoryData[accesory.id] = true;
            };

            vm.updateAccesory = function (accesory) {

                vm.editingAccesoryData[accesory.id] = false;
                abp.ui.setBusy();
                ticketService.updateAccessoryAsync(accesory)
                    .success(function () {
                        abp.notify.info(App.localize('SavedSuccessfully'));
                        getDateEntries();
                    }).finally(function () {
                        abp.ui.clearBusy();
                    });
            };

            vm.removeAccesory = function (accesory) {
                abp.ui.setBusy();
                ticketService.deleteAccessoryAsync({ id: accesory.id })
                     .success(function () {
                         abp.notify.info(App.localize('SavedSuccessfully'));
                         getDateEntries();
                     }).finally(function () {
                         abp.ui.clearBusy();
                     });
            };
            //end Accessories



            //boughtats
            vm.modifyBoughtAt = function (boughtat) {
                vm.editingBoughtAtData[boughtat.id] = true;
            };

            vm.updateBoughtAt = function (boughtat) {

                vm.editingBoughtAtData[boughtat.id] = false;
                abp.ui.setBusy();
                ticketService.updateBoughtAtAsync(boughtat)
                    .success(function () {
                        abp.notify.info(App.localize('SavedSuccessfully'));
                        getDateEntries();
                    }).finally(function () {
                        abp.ui.clearBusy();
                    });
            };

            vm.removeBoughtAt = function (boughtat) {
                abp.ui.setBusy();
                ticketService.deleteBoughtAtAsync({ id: boughtat.id })
                     .success(function () {
                         abp.notify.info(App.localize('SavedSuccessfully'));
                         getDateEntries();
                     }).finally(function () {
                         abp.ui.clearBusy();
                     });
            };

            //end boughtats

            //spares
            vm.modifySpare = function (spare) {
                vm.editingSparesData[spare.id] = true;
            };

            vm.updateSpare = function (spare) {

                vm.editingSparesData[spare.id] = false;
                abp.ui.setBusy();
                ticketService.updateSpareAsync(spare)
                    .success(function () {
                        abp.notify.info(App.localize('SavedSuccessfully'));
                        getDateEntries();
                    }).finally(function () {
                        abp.ui.clearBusy();
                    });
            };

            vm.removeSpare = function (spare) {
                abp.ui.setBusy();
                ticketService.deleteSpareAsync({ id: spare.id })
                     .success(function () {
                         abp.notify.info(App.localize('SavedSuccessfully'));
                         getDateEntries();
                     }).finally(function () {
                         abp.ui.clearBusy();
                     });
            };
            //end boughtats

            //load all entries
            getDateEntries();
        }
    ]);
})();