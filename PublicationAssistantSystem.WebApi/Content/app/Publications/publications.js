"use strict";

var publicationsModule = angular.module("publications");

publicationsModule.config(["$routeProvider", function ($routeProvider) {
    
    $routeProvider
    .when("/Publications", {
        templateUrl: "Content/app/Publications/publications.html"
    })
    .when("/Publications/All", {
        templateUrl: "Content/app/Publications/allPublications.html"
    })
    .when("/Publications/ConferencePapers", {
        templateUrl: "Content/app/Publications/conferencePapers.html"
    });

}]);

