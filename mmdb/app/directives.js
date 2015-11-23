angular.module("mmdb")
    //Generic form for editing or adding movies.
    .directive("movieForm", function () {
        return {
            restrict: 'E',
            templateUrl: "/app/shared/movieForm/movieForm.html",
            require: 'ngModel',
            scope: {
                Movie: "=ngModel"
            },
            link: function ($scope, $elements, $attr) {

            }
        }
    })
    //Movie Car Directive
 .directive("movieCard", function () {
     return {
         restrict: 'E',
         templateUrl: "/app/shared/movieCard/movieCard.html",
         require: 'ngModel',
         scope: {
             Movie: "=ngModel"
         },
         link: function ($scope, $elements, $attr) {

         }
     }
 });