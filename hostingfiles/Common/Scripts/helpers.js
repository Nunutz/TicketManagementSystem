var App = App || {};
(function () {

    var appLocalizationSource = abp.localization.getSource('RMATicketing');
    App.localize = function () {
        return appLocalizationSource.apply(this, arguments);
    };

})(App);