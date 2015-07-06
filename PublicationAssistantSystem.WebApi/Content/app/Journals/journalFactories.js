"use strict";

var journalsModule = angular.module("journals");

journalsModule.factory("JournalFactory", ["$http", function ($http) {

    var getAlljournals = function () {
        return $http.get("/api/journals");
    }

    var getJournal = function (journalId) {
        return $http.get("/api/journals/" + journalId);
    }

    var addJournal = function (journal) {
        return $http.post("/api/journals/", journal);
    }

    var updateJournal = function (journal) {
        return $http.put("/api/journals/", journal);
    }

    return {
        getAlljournals: getAlljournals,
        getJournal: getJournal,
        addJournal: addJournal,
        updateJournal: updateJournal,
    };
}]);
