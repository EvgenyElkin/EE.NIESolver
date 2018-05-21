;(function() {
    "use strict";

    angular
        .module("NIESolverApp")
        .controller("MethodEditController", controller);

    controller.$inject = ["$routeParams", "$http", "$location"];
    
    function controller($routeParams, $http, $location) {
        var self = this;
        
        self.isNew = $routeParams.methodId === "new";
        $http.get("/method/edit?methodId=" + $routeParams.methodId)
            .then(function(response) {
                self.model = response.data.item;
                console.log(self.model);
            });
        
        self.addParameter = function() {
            self.model.parameteres.push({typeId: undefined});
        }

        self.removeParameter = function(index) {
            self.model.parameteres.splice(index, 1);
        }

        self.submit = function () {
            var data = {
                Id: self.model.id,
                Name: self.model.name,
                Description: self.model.description,
                MethodTypeId: self.model.methodTypeId,
                Parameteres: _.map(self.model.parameteres,
                    function(x) {
                        return {
                            Id: x.id,
                            Name: x.name,
                            Description: x.description,
                            Code: x.code,
                            TypeId: x.typeId
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