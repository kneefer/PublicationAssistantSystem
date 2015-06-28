"use strict"

var employeesModule = angular.module("employees");

employeesModule.directive("listEmployees", ["EmployeeFactory", function (EmployeeFactory) {

    return {
        restrict: "E",
        templateUrl: "Content/app/Employees/templates/employeeList.html",
        link: function ($scope, element, attrs) {
            EmployeeFactory.getAllEmployees()
                .then(function (response) {
                    $scope.employees = response.data;
                });
        }
    };

}]);

employeesModule.directive("addEmployee", ["EmployeeFactory", "DivisionFactory", function (EmployeeFactory, DivisionFactory) {

    return {
        restrict: 'AE',
        templateUrl: "Content/app/Employees/templates/addEmployee.html",
        link: function ($scope) {

            getDivisions();
            $scope.addEmployee = function () {

                EmployeeFactory.addEmployee($scope.Employee)
                    .then(function (response) {
                        if (response.status == 201) {
                            $scope.Employee = null;
                            EmployeeFactory.getAllEmployees()
                               .then(function (response) {
                                   $scope.employees = response.data;
                               })
                            $(".addition-succeded").slideDown("slow").delay(3000).slideUp("slow");

                        } else
                            alert("Error!");
                    });
            }

            $scope.showForm = function () {
                $(".add-item-form").slideToggle();
            }

            function getDivisions() {
                DivisionFactory.getAllDivisions()
                    .then(function (response) {
                        $scope.divisions = response.data;
                    });
            }
        }
    }
}]);