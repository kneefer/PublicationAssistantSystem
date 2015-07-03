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

    var getJournalEditions = function (journalId) {
        return $http.get("/api/Journals/" + journalId + "/JournalEditions");
    }

    var getJournalEdition = function (journalEditionid) {
        return $http.get("/api/JournalEditions/" + journalEditionid);
    }

    var addJournalEdition = function (journal) {
        return $http.post("/api/JournalEditions/", journal);
    }

    var updateJournalEdition = function (journal) {
        return $http.put("/api/JournalEditions/", journal);
    }

    return {
        getAlljournals: getAlljournals,
        getJournal: getJournal,
        addJournal: addJournal,
        updateJournal: updateJournal,
        getJournalEditions: getJournalEditions,
        getJournalEdition: getJournalEdition,
        addJournalEdition: addJournalEdition,
        updateJournalEdition: updateJournalEdition
    };
}]);
