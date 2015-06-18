"use strict";

var institutesModule = angular.module("institutes");

facultiesModule.factory("InstituteFactory", ["$http", function ($http) {

    var getFacultyInstitutes = function (facultyId) {
        return $http.get("/api/Faculties/"+facultyId+"/Institutes");
    }

    var addInstitute = function (institute, facultyId) {
        return $http.post("/api/Institutes", {
            "Name": institute.Name,
            "FacultyId": facultyId
        });
    }

    return {
        getFacultyInstitutes: getFacultyInstitutes,
        addInstitute: addInstitute
    };
}]);
