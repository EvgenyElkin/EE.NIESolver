;(function() {
    "use strict";

    angular
        .module("NIESolverApp")
        .controller("HomeController", controller);

    controller.$inject = [];
    
    function controller() {
        var self = this;
        self.model = {
            header: "Hello world"
        };
    }
})();