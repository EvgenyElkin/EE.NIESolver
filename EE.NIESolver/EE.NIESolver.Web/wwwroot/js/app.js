(function() {
  angular
    .module("NIESolverApp", ["ngRoute", "semantic-ui", "angular-md5", "ngStorage"])
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