"use strict";

var facultiesModule = angular.module("faculties");

facultiesModule.factory("FacultyFactory", ["$http", function ($http) {

    var getAllFaculties = function () {
        return $http.get("/api/Faculties");
    }

    return {
        getAllFaculties: getAllFaculties
    };
}]);
