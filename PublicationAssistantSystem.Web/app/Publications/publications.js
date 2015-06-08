"use strict";

var publicationsModule = angular.module("publications");

publicationsModule.config(["$routeProvider", function ($routeProvider) {
    
    $routeProvider
        .when("/Publications", {
            templateUrl: "app/Publications/publications.html"
            // controller: "PublicationsControler"
        });
}]);

/*
publicationsModule.controller("PublicationsController", ["$scope", "PublicationFactory", function (PublicationFactory) {
  
}]);
*/

