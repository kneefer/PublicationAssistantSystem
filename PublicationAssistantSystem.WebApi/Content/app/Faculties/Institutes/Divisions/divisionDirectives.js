'use strict'

var divisionsModule = angular.module("divisions");

divisionsModule.directive("listDivisions", ["DivisionFactory", "$routeParams", function (DivisionFactory, $routeParams) {

    return {
        restrict: "E",
        scope: {},
        templateUrl: "Content/app/Faculties/Institutes/Divisions/templates/divisionList.html",
        link: function ($scope, element, attrs) {
            DivisionFactory.getInstituteDivisions($routeParams.instituteId)
                .then(function (response) {
                    $scope.divisions = response.data;
                });
        }
    };
}]);