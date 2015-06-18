"use strict";

var facultiesModule = angular.module("faculties");

facultiesModule.factory("FacultyFactory", ["$http", function ($http) {

    var getAllFaculties = function () {
        return $http.get("/api/Faculties");
    }

    var getFaculty = function (facultyId) {
        return $http.get("/api/Faculties/" + facultyId);
    }

    var addFaculty = function(faculty) {
        return $http.post("/api/Faculties", {
            "Abbreviation": faculty.Abbreviation,
            "Name" : faculty.Name
        });
    }

    var updateFaculty = function (faculty) {
        return $http.put("/api/Faculties/", {
            "Id": faculty.Id,
            "Abbreviation": faculty.Abbreviation,
            "Name": faculty.Name
        });
    }

    return {
        getAllFaculties: getAllFaculties,
        getFaculty: getFaculty,
        addFaculty: addFaculty,
        updateFaculty: updateFaculty
    };
}]);
