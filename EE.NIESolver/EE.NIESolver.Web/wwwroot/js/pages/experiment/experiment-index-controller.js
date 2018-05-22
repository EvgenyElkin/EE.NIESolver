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
            $http.delete("/Experiment/DeleteExperiment?id=" + id)
                .then(function(response) {
                    if (response.data.isSuccess) {
                        self.data = _.filter(self.data, function(x) { return x.id !== id });
                    }
                });
        };

        self.run = function(experiment) {
            $http.get("/Experiment/RunExperiment?experimentId=" + experiment.id)
                .then(function (response) {
                    if (response.data.isSuccess) {
                        experiment.resultCount++;
                        console.log("bingo");
                    }
                });
        }
    }
})();