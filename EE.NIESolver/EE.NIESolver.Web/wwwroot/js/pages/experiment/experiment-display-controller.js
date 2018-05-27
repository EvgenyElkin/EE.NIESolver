;(function() {
    "use strict";

    angular
        .module("NIESolverApp")
        .controller("ExperimentDisplayController", controller);

    controller.$inject = ["$scope", "$http", "$location", "$routeParams", "$timeout"];
    
    function controller($scope, $http, $location, $routeParams, $timeout) {
        var self = this;
        self.model = {};
        self.loading = true;

        $http.get("/Experiment/Display?id=" + $routeParams.experimentId)
            .then(function(response) {
                self.model = response.data.item;
                self.run = {
                    parameters: [{}],
                    runners: [],
                    runnerTypes: [
                        { id: 1, value: "Последовательный \"снизу-слева\"" },
                        { id: 2, value: "Параллельнеый по диагонали \"снизу-слева\"" }
                    ]
                }
                $timeout(function() { self.loading = false; }, 2000);
                console.log(self.model);
            });

        self.addRunParameter = function() {
            self.run.parameters.push({});
        };

        self.removeRunParameter = function (parameter) {
            self.run.parameters = _.without(self.run.parameters, parameter);
        };

        self.runExperiment = function () {
            console.log(1);
            var runParameters = {
                Runners: self.run.runners,
                Parameters: _.map(self.run.parameters,
                    function(x) {
                        return { N: x.n, M: x.m }
                    })
            };
            console.log(runParameters);
        }

    }
})();