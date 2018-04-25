;(function() {
  angular
  	.module("NIESolverApp")
    .constant("$config", {
        appname: "MathSolver",
        menu: [
            {
                name: "Главная",
                url: "/home/index",
                icon: "home"
            }
        ]
    });
})();
