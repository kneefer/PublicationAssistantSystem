"use strict";

var institutesModule = angular.module("institutes");

institutesModule.config(["$routeProvider", function ($routeProvider) {
    $routeProvider
        .when("/Faculties/:facultyId/Institutes", {
            templateUrl: "Content/app/Faculties/Institutes/institutes.html"
        });
}]);