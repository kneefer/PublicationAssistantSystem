"use strict"

var employeesModule = angular.module("employees");

employeesModule.config(["$routeProvider", function ($routeProvider) {
    $routeProvider
        .when("/Employees", {
            templateUrl: "Content/app/Employees/employees.html"
    });
}]);