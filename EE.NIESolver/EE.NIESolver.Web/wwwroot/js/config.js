;(function() {
  angular
  	.module("NIESolverApp")
    .constant("$config", {
        appname: "MathSolver",
        menu: [
            {
                name: "Главная",
                url: "/home/index",
                icon: "home",
            },
            {
                name: "Методы",
                url: "/method/index",
                icon: "external alternate",
            },
            {
                name: "Эксперименты",
                url: "/experiment/index",
                icon: "sience alternate"
            }
        ]
    });
})();
