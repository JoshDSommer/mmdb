angular.module('mmdb')
    .controller('EditMovieController', ['Movies', 'Actors', 'MovieActors', '$routeParams', function (Movies, Actors, MovieActors, $routeParams) {
        var edit = this;
        edit.Movie = {};
        //get movied on load
        Movies.get({ id: $routeParams.id }, function (data) {
            edit.Movie = data;
        });

        //delete editing movie
        edit.deleteMovie = function (Movie) {
            if (window.confirm('Delete ' + Movie.Title + '?')) {
                Movies.delete({ id: Movie.MovieId });
                window.location = "/#/";
            }
        };

        // save updates to moveie
        edit.saveMovie = function (Movie) {
            Movies.update({ id: Movie.MovieId }, Movie);
            window.location = "/#/";
        };

        //remove actor.
        edit.removeActor = function (ActorId) {
            MovieActors.delete({ MovieId: edit.Movie.MovieId, ActorId: ActorId });
            //remove actor for Actors array
            edit.Movie.Actors = edit.Movie.Actors.filter(function (a) {
                return a.ActorId != ActorId;
            });
        };
        //add actor
        edit.addActor = function (actor) {
            //try to get actor by name entered.
            Actors.get({ id: actor }, function (data) {
                //if the actor existis already.
                if (data.ActorId != undefined) {
                    //add the actor to the movie.
                    addMovieActor(data);
                } else {
                    //create new actor
                    Actors.save({ Name: actor }, function (data) {
                        //save to movie.
                        addMovieActor(data);
                    });
                }
            });
            // clear actor text box and keep focus.
            angular.element("#add-actor").val('').focus();
        }

        //add actor to database and actor array.
        function addMovieActor(actor) {
            MovieActors.save({ MovieId: edit.Movie.MovieId, ActorId: actor.ActorId });
            edit.Movie.Actors.push(actor);
        }
    }]);