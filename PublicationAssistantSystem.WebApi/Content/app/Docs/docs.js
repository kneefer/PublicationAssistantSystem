"use strict";

var docsModule = angular.module("docs");

docsModule.config(["$routeProvider", function ($routeProvider) {
    $routeProvider
        .when("/Docs", {
            templateUrl: "Content/app/Docs/docs.html"
        });
}]);