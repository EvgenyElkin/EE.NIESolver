;(function() {
    "use strict";

    angular
        .module("NIESolverApp")
        .controller("ErrorBasedReportController", controller);

    controller.$inject = ["$scope", "$http", "$routeParams", "$timeout"];

    function buildChart(chartData) {
        //var labels = _.map(chartData, function (x) { return x.label; });
        //var data = [_.map(chartData, function (x) { return x.error; })];
        //console.log(data);
        //console.log(labels);
        var labels = ["N: 80, M:80", "N:160, M:160", "N:320, M:320", "N:640, M:640", "N:1280, M:1280", "N:2560, M:2560"];
        var data = [
            [5,19,82,314,1314,5498],
            [4,9,44,152,558,2271],
            [4,12,42,156,662,2450]
        ];
        return {
            series: ['Последовательный', 'Паралельный', 'Пулл потоков'],
            datasetOverride: [{ lineTension: 0, fill: false }, { lineTension: 0, fill: false }, { lineTension: 0, fill: false }],
            data: data,
            labels: labels,
            options: {
                legend: {
                    display: true,
                    position: "top"
                },
                scales: {
                    yAxes: [
                        {
                            display:true,
                            //type: "logarithmic"
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