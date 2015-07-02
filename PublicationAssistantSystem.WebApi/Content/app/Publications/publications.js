"use strict";

var publicationsModule = angular.module("publications");

publicationsModule.config(["$routeProvider", function ($routeProvider) {
    
    $routeProvider
    .when("/Publications", {
        templateUrl: "Content/app/Publications/publications.html",
        controller: 'PublicationsController'
    })
    .when("/Publications/:type", {
        templateUrl: "Content/app/Publications/listPublications.html",
        controller: 'PublicationsController'
    })
    .when("/Publications/:type/create", {
        templateUrl: "Content/app/Publications/createPublication.html",
        controller: 'PublicationsController'
    });

}]);

publicationsModule.controller("PublicationsController", ['$scope', '$location', 'PublicationFactory',
    function ($scope, $location, PublicationFactory) {

        $scope.paths = PublicationFactory.getPathFromUrl();
        $scope.publicationTypes = PublicationFactory.translations;
        $scope.publicationType = PublicationFactory.getTypeFromUrl();

}]);

