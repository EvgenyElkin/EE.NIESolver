;(function() {
    "use strict";

    angular
        .module("NIESolverApp")
        .controller("ExperimentCreateController", controller);

    controller.$inject = ["$scope", "$http", "$location"];
    
    function controller($scope, $http, $location) {
        var self = this;
        self.model = {};

        $http.get("/Experiment/Create")
            .then(function(response) {
                self.model = response.data.item;
                console.log(self.model);
            });

        $scope.$watch(function() { return self.model.methodId },
            function (value) {
                self.model.parameters = [];
                if (value) {
                    $http.get("/Experiment/GetParameters?methodId=" + value)
                        .then(function(response) {
                            if (response.data.isSuccess) {
                                self.model.parameters = _.map(response.data.item,
                                    function(x) {
                                        return {
                                            name: x.value,
                                            id: x.id,
                                            description: x.description
                                        }
                                    });
                                console.log(self.model.parameters);
                            }
                        });
                }
            });

        self.submit = function () {
            var data = {
                Name: self.model.name,
                Description: self.model.description,
                MethodId: self.model.methodId,
                Values: _.map(self.model.parameters,
                    function(x) {
                        return {
                            ParameterId: x.id,
                            Value: x.value
                        }
                    })
            };
            $http.post("/Experiment/Create", data).then(function(response) {
                if (response.data.isSuccess == true) {
                    $location.path("/experiment/index");
                }
            });
        };
    }
})();