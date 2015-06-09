"use strict";

var facultiesModule = angular.module("faculties");

facultiesModule.config(["$routeProvider", function ($routeProvider) {
    $routeProvider
        .when("/Faculties", {
            templateUrl: "Content/app/Faculties/faculties.html"
    });
}]);