"use strict";

var mainModule = angular.module("app");

mainModule.config(["$routeProvider", function ($routeProvider) {
    $routeProvider
        .otherwise("/");
}]);
