;(function() {
    "use strict";

    angular
        .module("NIESolverApp")
        .controller("ExperimentIndexController", controller);

    controller.$inject = ["$http"];
    
    function controller($http) {
        var self = this;

        //$http.get("/Experiment/GetAll")
        //    .then(function(response) {
        //        if (response.data.isSuccess) {
        //            self.items = response.data.item;
        //            console.log(response.data);
        //        }
        //    });
    }
})();