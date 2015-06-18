"use strict";

var institutesModule = angular.module("institutes");

facultiesModule.factory("InstituteFactory", ["$http", function ($http) {

    var getFacultyInstitutes = function (facultyId) {
        return $http.get("/api/Faculties/"+facultyId+"/Institutes");
    }

    var getInstitute = function (instituteId) {
        return $http.get("/api/Institutes/" + instituteId);
    }

    var addInstitute = function (institute, facultyId) {
        return $http.post("/api/Institutes", {
            "Name": institute.Name,
            "FacultyId": facultyId
        });
    }

    var updateInstitute = function (institute) {
        return $http.put("/api/Institutes", {
            "Id": institute.Id,
            "Name": institute.Name,
            "FacultyId": institute.FacultyId
        });
    }

    return {
        getFacultyInstitutes: getFacultyInstitutes,
        getInstitute: getInstitute,
        addInstitute: addInstitute,
        updateInstitute: updateInstitute
    };
}]);
