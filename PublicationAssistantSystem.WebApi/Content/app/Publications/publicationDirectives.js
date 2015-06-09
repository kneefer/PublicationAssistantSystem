"use strict";

var publicationsModule = angular.module("publications");

publicationsModule.directive("listPublications", ["PublicationFactory", function (PublicationFactory) {

    return {
        restrict: "E",
        scope: {},
        templateUrl: "Content/app/Publications/templates/publicationList.html",
        link: function ($scope, element, attrs) {
            PublicationFactory.getAllPublications()
                .then(function (response) {
                    $scope.publications = response.data;
                })
        }
    };

}]);
