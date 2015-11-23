angular.module('mmdb')
    //factories to interact with mmdb web api.
    .factory('Movies', ['$resource', function ($resource) {
        return $resource('api/movies/:id', {}, {
            'update': { method: 'PUT', params: { id: '@id' } }
        });
    }])
    .factory('Actors', ['$resource', function ($resource) {
        return $resource('api/actors/:id', {}, {});
    }])
    .factory('MovieActors', ['$resource', function ($resource) {
        return $resource('api/movieactors/:MovieId/:ActorId', {}, {});
    }])
    .factory('Stats', ['$resource', function ($resource) {
        return $resource('api/stats/:method/:genre', {}, {
            'TopActors': { method: 'GET', params: { method: 'TopActors' }, isArray: true },
            'TopGenres': { method: 'GET', params: { method: 'TopGenres' }, isArray: true },
            'Genres': { method: 'GET', params: { method: 'Genres' }, isArray: true },
            'ActorsByGenre': { method: 'POST', params: { method: 'ActorsByGenre', genre: '@genre' }, isArray: true },
        });
    }]);
;