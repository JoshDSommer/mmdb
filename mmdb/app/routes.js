//create app routing.
angular.module('mmdb')
    .config(['$routeProvider', function ($routeProvider) {
        $routeProvider.
            when('/', {
                templateUrl: "/app/components/home/homeView.html",
                controllerAs: "HomeController"
            }).
            when('/movie/:id', {
                templateUrl: "/app/components/movieInfo/MovieInfoView.html",
                controllerAs: "MovieInfoController"

            }).
               when('/edit/:id', {
                   templateUrl: "/app/components/editMovie/editMovieView.html",
                   controllerAs: "EditMovieController"

               }).
            when('/add/', {
                templateUrl: "/app/components/addMovie/addMovieView.html",
                controllerAs: "AddMovieController"

            }).
             when('/stats/', {
                 templateUrl: "/app/components/stats/statsView.html",
                 controllerAs: "StatsController"

             }).
          otherwise({
              redirectTo: '/'
          });
    }]);