angular.module('mmdb')
.controller('StatsController', ['Movies', 'Stats', function (Movies, Stats) {
    var stats = this;
    stats.top3Genres = [];
    stats.top3Actors = [];
    stats.Actors = [];
    stats.Genres = [];
    stats.selectedGenre = '';

    //get to actors on load.
    Stats.TopActors(function (data) {
        stats.top3Actors = data;
    });

    // get top 3 genres on load
    Stats.TopGenres(function (data) {
        stats.top3Genres = data;
    });
    //get genres on load
    Stats.Genres(function (data) {
        stats.Genres = data;
        //set drop down to first item in genres.
        stats.selectedGenre = data[0].Genre;
        //get all the actors in the first genre
        stats.ActorsByGenre(stats.selectedGenre);
    });

    // when the genres drop down is changed.
    stats.ActorsByGenre = function (genre) {
        //get all the actors in a genre
        Stats.ActorsByGenre({ genre: genre }, function (data) {
            stats.Actors = data
        });
    };
}]);