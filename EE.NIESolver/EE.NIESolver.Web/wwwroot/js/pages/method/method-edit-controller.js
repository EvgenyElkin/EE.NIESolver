;(function() {
    "use strict";

    angular
        .module("NIESolverApp")
        .controller("MethodEditController", controller);

    controller.$inject = ["$scope", "$routeParams", "$http", "$location"];
    
    function controller($scope, $routeParams, $http, $location) {
        var self = this;
        
        self.isNew = $routeParams.methodId === "new";
        $http.get("/method/edit?methodId=" + $routeParams.methodId)
            .then(function(response) {
                self.model = response.data.item;
                $scope.$watch(function () { return self.model.methodTypeId },
                    function (newType, oldType) {
                        if (newType && newType !== oldType) {
                            self.model.parameteres = angular.copy(self.model.systemParameters[newType]);
                            _.each(self.model.parameteres, function(x) { x.id = undefined });
                        }
                    });

                console.log(self.model);
            });

        

        self.addParameter = function() {
            self.model.parameteres.push({typeId: undefined});
        }

        self.removeParameter = function(index) {
            self.model.parameteres.splice(index, 1);
        }

        self.getParameterName = function(id) {
            return _.filter(self.model.methodParameterTypes, function(x) { return x.id === id }).shift().description;
        }

        self.submit = function () {
            var data = {
                Id: self.model.id,
                Name: self.model.name,
                Description: self.model.description,
                MethodTypeId: self.model.methodTypeId,
                MethodExpression: self.model.methodExpression,
                Parameteres: _.map(self.model.parameteres,
                    function(x) {
                        return {
                            Id: x.id,
                            Name: x.name,
                            Description: x.description,
                            Code: x.code,
                            IsSystem: x.isSystem,
                            ParameterTypeId: x.parameterTypeId
                        }
                    })
            };

            $http.post("/Method/Edit", data).then(function(response) {
                if (response.data.isSuccess == true) {
                    $location.path("/method/index");
                }
            });
        };
    }
})();