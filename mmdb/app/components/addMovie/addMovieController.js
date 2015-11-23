angular.module('mmdb')
.controller('AddMovieController', ['Movies', function (Movies) {
    //save new movie to database.
    this.addMovie = function (newMovie) {
        Movies.save(newMovie, function (data) {
        
            window.alert(newMovie.Title + " added to database. You can now add Actors.");
            // redirect to edit screen for new movie so actors can be added.
            window.location = "/#/edit/" + data.MovieId;
        });
    };
}]);