(function () {
    'use strict';
    
    var app = angular.module('app', [
        'ngAnimate',
        'ngSanitize',
        'ui.router',
        'ui.bootstrap',
        'ui.jq',
        //'ui.grid',
        'ui.date',
       // 'summernote',
        'abp'
    ]);

    //Configuration for Angular UI routing.
    app.config([
        '$stateProvider', '$urlRouterProvider',
        function($stateProvider, $urlRouterProvider) {
            $urlRouterProvider.otherwise('/');

            //if (abp.auth.hasPermission('Pages.Users')) {
            //    $stateProvider
            //        .state('users', {
            //            url: '/users',
            //            templateUrl: '/App/Main/views/users/index.cshtml',
            //            menu: 'Users' //Matches to name of 'Users' menu in RMATicketingNavigationProvider
            //        });
            //    $urlRouterProvider.otherwise('/users');
            //}

            //if (abp.auth.hasPermission('Pages.Tenants')) {
            //    $stateProvider
            //        .state('tenants', {
            //            url: '/tenants',
            //            templateUrl: '/App/Main/views/tenants/index.cshtml',
            //            menu: 'Tenants' //Matches to name of 'Tenants' menu in RMATicketingNavigationProvider
            //        });
            //    $urlRouterProvider.otherwise('/tenants');
            //}

            $stateProvider
                   .state('home', {
                       url: '/',
                       templateUrl: '/App/Main/views/home/home.cshtml',
                       menu: 'Home' //Matches to name of 'Home' menu in RMATicketingNavigationProvider
                   })
                   .state('private', {
                       url: '/dashboard',
                       templateUrl: '/App/Main/views/dashboard/dashboard.cshtml',
                       menu: 'Private' //Matches to name of 'Home' menu in RMATicketingNavigationProvider
                   })
                   .state('professional', {
                       url: '/professional',
                       templateUrl: '/App/Main/views/dashboard/professional.cshtml',
                       menu: 'Professional' //Matches to name of 'Home' menu in RMATicketingNavigationProvider
                   })
                   .state('trading', {
                       url: '/trading',
                       templateUrl: '/App/Main/views/dashboard/trading.cshtml',
                       menu: 'Trading' //Matches to name of 'Home' menu in RMATicketingNavigationProvider
                   })
                  .state('imei', {
                      url: '/imei',
                      templateUrl: '/App/Main/views/imei/imeilist.cshtml',
                      menu: 'Imei Number' //Matches to name of 'Home' menu in RMATicketingNavigationProvider
                  })
                  .state('dataentry', {
                      url: '/dataentry',
                      templateUrl: '/App/Main/views/dataentry/index.cshtml',
                      menu: 'Data Entry' //Matches to name of 'About' menu in RMATicketingNavigationProvider
                  })
                 .state('report', {
                     url: '/report',
                     templateUrl: '/App/Main/views/report/index.cshtml',
                     menu: 'Report' //Matches to name of 'About' menu in RMATicketingNavigationProvider
                 });
            
        }
    ]);
})();