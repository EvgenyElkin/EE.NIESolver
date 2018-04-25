(function() {
    "use strict";

    angular
        .module("NIESolverApp")
        .directive("menu", menu);

    menu.$inject = ["$config", "$location"];

    function menu($config, $location) {
        return {
            restrict: "E",
            scope: {},
            templateUrl: "js/directives/menu/menu.tmpl.html",
            link: function($scope) {
                $scope.appname = $config.appname;
                $scope.menu = $config.menu;
            }
        };
    }
})();