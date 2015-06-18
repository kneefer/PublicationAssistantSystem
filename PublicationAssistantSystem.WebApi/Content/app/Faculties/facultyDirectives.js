"use strict";

var facultiesModule = angular.module("faculties");

facultiesModule.directive("listFaculties", ["FacultyFactory", function (FacultyFactory) {

    return {
        restrict: "E",
        templateUrl: "Content/app/Faculties/templates/facultyList.html",
        link: function ($scope, element, attrs) {
            FacultyFactory.getAllFaculties()
                .then(function (response) {
                    $scope.faculties = response.data;
                })
        }
    };

}]);

facultiesModule.directive("addFaculty", ["FacultyFactory", function (FacultyFactory) {

    return {
        restrict: 'AE',
        templateUrl: "Content/app/Faculties/templates/addFaculty.html",
        link: function ($scope) {
            
            $scope.addFaculty = function () {
                
                FacultyFactory.addFaculty($scope.Faculty)
                    .then(function(response) {
                        if (response.status == 201) {
                            FacultyFactory.getAllFaculties()
                                .then(function (response) {
                                    $scope.faculties = response.data;
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

facultiesModule.directive("editFaculty", ["FacultyFactory", "$routeParams", function (FacultyFactory, $routeParams) {
    return {
        restrict: 'E',
        templateUrl: 'Content/app/Faculties/templates/editFaculty.html',
        link: function ($scope) {
            FacultyFactory.getFaculty($routeParams.facultyId)
                .then(function (response) {
                    $scope.Faculty = response.data;
                });
            $scope.updateFaculty = function () {
                FacultyFactory.updateFaculty($scope.Faculty)
                .then(function (response) {
                    if (response.status = 201)
                        $(".update-succeded").slideDown("slow").delay(3000).slideUp("slow");
                    else
                        alert("Error!");
                });
            }

            $scope.showUpdateForm = function () {
                $(".update-item-form").slideToggle();
            }
            
        }
    }
}]);