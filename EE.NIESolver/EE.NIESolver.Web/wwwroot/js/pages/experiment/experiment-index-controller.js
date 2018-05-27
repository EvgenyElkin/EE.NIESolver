;(function() {
    "use strict";

    angular
        .module("NIESolverApp")
        .controller("ExperimentIndexController", controller);

    controller.$inject = ["$http"];
    
    function controller($http) {
        var self = this;

        $http.get("/Experiment/GetAll")
            .then(function(response) {
                if (response.data.isSuccess) {
                    self.items = response.data.item;
                    console.log(self.items);
                }
            });

        self.remove = function(id) {
            $http.delete("/Experiment/Delete?id=" + id)
                .then(function(response) {
                    if (response.data.isSuccess) {
                        self.data = _.filter(self.data, function(x) { return x.id !== id });
                    }
                });
        };

        self.run = function(experiment) {
            $http.get("/Experiment/Run?id=" + experiment.id)
                .then(function (response) {
                    if (response.data.isSuccess) {
                        experiment.resultCount++;
                        console.log("bingo");
                    }
                });
        }
    }
})();