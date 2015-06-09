"use strict";

var institutesModule = angular.module("institutes");

facultiesModule.factory("InstituteFactory", ["$http", function ($http) {

    var getFacultyInstitutes = function (facultyId) {
        return $http.get("/api/Faculties/"+facultyId+"/Institutes");
    }

    return {
        getFacultyInstitutes: getFacultyInstitutes
    };
}]);
