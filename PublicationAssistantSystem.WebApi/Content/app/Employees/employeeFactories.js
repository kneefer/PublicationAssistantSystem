"use strict"

var employeesModule = angular.module("employees");

employeesModule.factory("EmployeeFactory", ["$http", function($http) {

    var getAllEmployees = function () {
        return $http.get("api/Employees");
    }
    
    var addEmployee = function (employee) {
        return $http.post("api/Employees", {
            "FirstName": employee.FirstName,
            "LastName": employee.LastName,
            "AcademicTitle": employee.AcademicTitle,
            "DivisionId": employee.DivisionId.Id
        });
    }

    return {
        getAllEmployees: getAllEmployees,
        addEmployee: addEmployee
    };
}]);