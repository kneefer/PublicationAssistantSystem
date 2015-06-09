"use strict";

var divisionsModule = angular.module("divisions");

divisionsModule.factory("DivisionFactory", ["$http", function ($http) {

    var getInstituteDivisions = function (instituteId) {
        return $http.get("/api/Institutes/" + instituteId + "/Divisions");
    }

    return {
        getInstituteDivisions: getInstituteDivisions
    };
}]);
