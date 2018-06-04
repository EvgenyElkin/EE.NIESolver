;(function() {
    "use strict";

    angular
        .module("NIESolverApp")
        .controller("ErrorBasedReportController", controller);

    controller.$inject = ["$scope", "$http", "$routeParams", "$timeout"];

    function buildChart(chartData) {
        var labels = _.map(chartData, function (x) { return x.label; });
        var data = [_.map(chartData, function (x) { return x.error; })];
        console.log(data);
        console.log(labels);
        return {
            series: ['Размер ошибки'],
            colors: ["#f2711c"],
            datasetOverride: [{ lineTension: 0, fill: false }],
            data: data,
            labels: labels,
            options: {
                scales: {
                    yAxes: [
                        {
                            type: "logarithmic"
                        }
                    ]
                }
            }
        }
    }

    function controller($scope, $http, $routeParams, $timeout) {
        var self = this;
        self.model = {};
        
        self.state = "initialize";
        $http.get("/Analysis/SetupErrorBasedReport?experimentId=" + $routeParams.experimentId)
            .then(function(response) {
                self.model = {
                    runs: response.data.item,
                    selectedRuns: []
                }
            });
        
        self.build = function () {
            self.state = "loading";
            $timeout(function () {
                var chartModel = [
                    { label: "m:40 n:40", error: 0.021 },
                    { label: "m:80 n:80", error: 0.00112 },
                    { label: "m:160 n:80", error: 0.000614 },
                    { label: "m:80 n:160", error: 0.000654 },
                    { label: "m:320 n:320", error: 0.0000123 }
                ];
                self.chart = buildChart(chartModel);
                self.state = "ready";
            }, 500);
        }
    }
})();