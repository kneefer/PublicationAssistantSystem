"use strict";

var facultiesModule = angular.module("faculties");

facultiesModule.directive("listFaculties", ["FacultyFactory", function (FacultyFactory) {

    return {
        restrict: "E",
        scope: {},
        templateUrl: "Content/app/Faculties/templates/facultyList.html",
        link: function ($scope, element, attrs) {
            FacultyFactory.getAllFaculties()
                .then(function (response) {
                    $scope.faculties = response.data;
                })
        }
    };

}]);