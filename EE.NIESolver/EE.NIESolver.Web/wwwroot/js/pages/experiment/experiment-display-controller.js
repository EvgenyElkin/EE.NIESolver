;(function() {
    "use strict";

    angular
        .module("NIESolverApp")
        .controller("ExperimentDisplayController", controller);

    controller.$inject = ["$scope", "$http", "$location", "$routeParams"];
    
    function controller($scope, $http, $location, $routeParams) {
        var self = this;
        self.model = {};
        self.loading = true;

        $http.get("/Experiment/Display?id=" + $routeParams.experimentId)
            .then(function (response) {
                console.log(response.data.item);
                self.model = response.data.item;
                self.run = {
                    runs: []
                };
                self.addRunParameter();
            });

        self.addRunParameter = function () {
            var newRun = {
                parameters: []
            };
            for (var parameter in self.model.runParameters.parameters) {
                if (self.model.runParameters.parameters.hasOwnProperty(parameter)) {
                    newRun.parameters.push({ name: parameter, value: undefined });
                }
            }
            self.run.runs.push(newRun);
        };

        self.removeRunParameter = function (parameter) {
            self.run.parameters = _.without(self.run.parameters, parameter);
        };

        self.runExperiment = function () {
            console.log(1);
            var runParameters = {
                ExperimentId: self.model.id,
                RunnerIds: self.run.runners,
                Runs: _.map(self.run.runs,
                    function (x) {
                        var parameters = {};
                        _.each(x.parameters, function(p) { parameters[p.name] = p.value; });
                        return { Parameters: parameters };
                    })
            };
            $http.post("/Experiment/Run", runParameters);
            console.log(runParameters);
        }

    }
})();