"use strict";

var divisionsModule = angular.module("divisions");

divisionsModule.config(["$routeProvider", function ($routeProvider) {
    $routeProvider
        .when("/Faculties/:facultyId/Institutes/:instituteId/Divisions", {
            templateUrl: "Content/app/Faculties/Institutes/Divisions/divisions.html",
            controller: "DivisionsController"
        });
}]);

divisionsModule.controller("DivisionsController", ["$scope","$routeParams",  function ($scope, $routeParams) {


    $scope.getInstituteFacultyId = function () {
        return $routeParams.facultyId;
    }
}]);
