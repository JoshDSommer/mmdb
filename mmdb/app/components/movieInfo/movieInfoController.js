angular.module('mmdb')
.controller('MovieInfoController', [ '$routeParams', 'Movies', function ( $routeParams, Movies) {
    this.movie = {};
    //get movied by movie id.
    this.movie = Movies.get({ id: $routeParams.id });
}]);