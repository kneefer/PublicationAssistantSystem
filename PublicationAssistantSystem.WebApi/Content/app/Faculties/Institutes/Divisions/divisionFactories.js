"use strict";

var divisionsModule = angular.module("divisions");

divisionsModule.factory("DivisionFactory", ["$http", function ($http) {

    var getAllDivisions = function () {
        return $http.get("/api/Divisions");
    }
    var getInstituteDivisions = function (instituteId) {
        return $http.get("/api/Institutes/" + instituteId + "/Divisions");
    }

    var addDivision = function (division) {
        return $http.post("/api/Divisions", {
            "Name": division.Name,
            "InstituteId": division.InstituteId
        });
    }
    return {
        getAllDivisions: getAllDivisions,
        getInstituteDivisions: getInstituteDivisions,
        addDivision: addDivision
    };
}]);
