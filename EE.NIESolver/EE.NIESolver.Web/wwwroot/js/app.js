(function() {
    angular
        .module("NIESolverApp", ["ngRoute", "semantic-ui", "angular-md5", "ngStorage", "chart.js"])
        .config(config);
    
  config.$inject = ["$routeProvider", "$locationProvider", "$httpProvider"];
    
  function config($routeProvider, $locationProvider, $httpProvider) {

    $locationProvider.html5Mode(false);

      $routeProvider
          .when("/home/index",
              {
                  templateUrl: "js/pages/home/home-template.html",
                  controller: "HomeController",
                  controllerAs: "homeCtrl"
              })
          .when("/method/index",
              {
                  templateUrl: "js/pages/method/method-index-template.html",
                  controller: "MethodIndexController",
                  controllerAs: "methodIndexCtrl"
              })
          .when("/method/edit/:methodId",
              {
                  templateUrl: "js/pages/method/method-edit-template.html",
                  controller: "MethodEditController",
                  controllerAs: "methodEditCtrl"
              })
          .when("/experiment/index",
              {
                  templateUrl: "js/pages/experiment/experiment-index-template.html",
                  controller: "ExperimentIndexController",
                  controllerAs: "experimentIndexCtrl"
              })
          .when("/experiment/display/:experimentId",
              {
                  templateUrl: "js/pages/experiment/experiment-display-template.html",
                  controller: "ExperimentDisplayController",
                  controllerAs: "experimentDisplayCtrl"
              })
          .when("/experiment/create",
              {
                  templateUrl: "js/pages/experiment/experiment-create-template.html",
                  controller: "ExperimentCreateController",
                  controllerAs: "experimentCreateCtrl"
          })
          .when("/analisys/error-based-report/:experimentId",
              {
                  templateUrl: "js/pages/analisys/error-based-report-template.html",
                  controller: "ErrorBasedReportController",
                  controllerAs: "reportCtrl"
              })
          .otherwise({
              redirectTo: "/home/index"
          });
  }

  angular
      .module("NIESolverApp")
    .run(run);

  run.$inject = ["$rootScope", "$location"];

  function run($rootScope, $location) {
    // put here everything that you need to run on page load
  }
})();