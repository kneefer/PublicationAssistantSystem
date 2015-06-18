'use strict'

var divisionsModule = angular.module("divisions");

divisionsModule.directive("listDivisions", ["DivisionFactory", "$routeParams", function (DivisionFactory, $routeParams) {

    return {
        restrict: "E",
        templateUrl: "Content/app/Faculties/Institutes/Divisions/templates/divisionList.html",
        link: function ($scope, element, attrs) {
            DivisionFactory.getInstituteDivisions($routeParams.instituteId)
                .then(function (response) {
                    $scope.divisions = response.data;
                });
        }
    };
}]);

divisionsModule.directive("addDivision", ["DivisionFactory", "$routeParams", function (DivisionFactory, $routeParams) {

    return {
        restrict: "E",
        templateUrl: "Content/app/Faculties/Institutes/Divisions/templates/addDivision.html",
        link: function ($scope) {
            $scope.addDivision = function () {
                $scope.Division.InstituteId = $routeParams.instituteId;
                DivisionFactory.addDivision($scope.Division)
                    .then(function (response) {
                        if (response.status == 201) {
                            DivisionFactory.getInstituteDivisions($routeParams.instituteId)
                                .then(function (response) {
                                    $scope.divisions = response.data;
                                    $scope.Division = null;
                                })
                            $(".addition-succeded").slideDown("slow").delay(3000).slideUp("slow");
                        } else
                            alert("Error!");
                    });
            }

            $scope.showForm = function () {
                $(".add-item-form").slideToggle();
            }
        }
    }
}]);