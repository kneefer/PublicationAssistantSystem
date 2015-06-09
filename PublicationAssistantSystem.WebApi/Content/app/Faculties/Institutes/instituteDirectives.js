'use strict'

var institutesModule = angular.module("institutes");

institutesModule.directive("listInstitutes", ["InstituteFactory", "$routeParams", function (InstituteFactory, $routeParams) {

    return {
        restrict: "E",
        scope: {},
        templateUrl: "Content/app/Faculties/Institutes/templates/instituteList.html",
        link: function ($scope, element, attrs) {
            InstituteFactory.getFacultyInstitutes($routeParams.facultyId)
                .then(function (response) {
                    $scope.institutes = response.data;
                });
        }
    };
}]);