"use strict";

var publicationsModule = angular.module("publications");

publicationsModule.factory("PublicationFactory", ["$http", function ($http) {

    var getAllPublications = function () {
        return $http.get("/api/Publications/All");
    }

    return {
        getAllPublications: getAllPublications
    };
}]);
