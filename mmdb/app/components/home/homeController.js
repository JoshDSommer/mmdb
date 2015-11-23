angular.module('mmdb')
    .controller('HomeController', ['Movies', function (Movies) {
        var home = this;
        home.movies = [];

        //get all movies in database
        Movies.query(function (data) {
            home.movies = data;
        });
    }]);