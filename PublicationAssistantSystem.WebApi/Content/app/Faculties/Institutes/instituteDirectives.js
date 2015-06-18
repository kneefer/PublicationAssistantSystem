'use strict'

var institutesModule = angular.module("institutes");

institutesModule.directive("listInstitutes", ["InstituteFactory", "$routeParams", function (InstituteFactory, $routeParams) {

    return {
        restrict: "E",
        templateUrl: "Content/app/Faculties/Institutes/templates/instituteList.html",
        link: function ($scope, element, attrs) {
            InstituteFactory.getFacultyInstitutes($routeParams.facultyId)
                .then(function (response) {
                    $scope.institutes = response.data;
                });
        }
    };
}]);

institutesModule.directive("addInstitute", ["InstituteFactory", "$routeParams", function (InstituteFactory, $routeParams) {

    return {
        restrict: 'AE',
        templateUrl: "Content/app/Faculties/Institutes/templates/addInstitute.html",
        link: function ($scope) {

            $scope.addInstitute = function () {

                InstituteFactory.addInstitute($scope.Institute, $routeParams.facultyId)
                    .then(function (response) {
                        if (response.status == 201) {
                            InstituteFactory.getFacultyInstitutes($routeParams.facultyId)
                                .then(function (response) {
                                    $scope.institutes = response.data;
                                    $scope.Institute = null;
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