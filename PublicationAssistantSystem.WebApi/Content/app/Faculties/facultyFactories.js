"use strict";

var facultiesModule = angular.module("faculties");

facultiesModule.factory("FacultyFactory", ["$http", function ($http) {

    var getAllFaculties = function () {
        return $http.get("/api/Faculties");
    }

    var addFaculty = function(faculty) {
        return $http.post("/api/Faculties", {
            "Abbreviation": faculty.Abbreviation,
            "Name" : faculty.Name
        });
    }

    return {
        getAllFaculties: getAllFaculties,
        addFaculty : addFaculty
    };
}]);
