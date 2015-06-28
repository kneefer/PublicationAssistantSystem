"use strict";

var publicationsModule = angular.module("publications");

publicationsModule.factory("PublicationFactory", ["$http", function ($http) {


    // all publications code
    var getAllPublications = function () {
        return $http.get("/api/Publications/All");
    }

    var getPublication = function (publicationId) {
        return $http.get("/api/Publications/All/" + publicationId);
    }

    var getPublicationOfEmployee = function (employeeId) {
        return $http.get("/api/Employees/" + employeeId + "/Publications");
    }

    // conference papers code
    var getAllConferencePapers = function () {
        return $http.get("/api/Publications/ConferencePapers");
    }

    var addConferencePaper = function (publication) {
        return $http.post("/api/Publications/ConferencePapers", publication);
    }

    // technical reports code
    var getAllTechnicalReports = function () {
        return $http.get("/api/Publications/TechnicalReports");
    }

    // theses code
    var getAllTheses = function () {
        return $http.get("/api/Publications/Theses");
    }

    // datasets code
    var getAllDatasets = function () {
        return $http.get("/api/Publications/Datasets");
    }

    // books code
    var getAllBooks = function () {
        return $http.get("/api/Publications/Books");
    }

    // patents code
    var getAllPatents = function () {
        return $http.get("/api/Publications/Patents");
    }

    // articles code
    var getAllArticles = function () {
        return $http.get("/api/Publications/Articles");
    }

    return {
        getAllPublications: getAllPublications,
        getAllConferencePapers: getAllConferencePapers,
        addConferencePaper: addConferencePaper
    };
}]);
