"use strict";

var journalEditionsModule = angular.module("journalEditions");

journalEditionsModule.factory("JournalEditionFactory", ["$http", function ($http) {

    var getJournalEditions = function (journalId) {
        return $http.get("/api/Journals/" + journalId + "/JournalEditions");
    }

    var getJournalEdition = function (journalEditionid) {
        return $http.get("/api/JournalEditions/" + journalEditionid);
    }

    var addJournalEdition = function (journalEdition, journalId) {
        journalEdition.JournalId = journalId;
        return $http.post("/api/JournalEditions/", journalEdition);
    }

    var updateJournalEdition = function (journalEdition) {
        return $http.put("/api/JournalEditions/", journalEdition);
    }

    return {
        getJournalEditions: getJournalEditions,
        getJournalEdition: getJournalEdition,
        addJournalEdition: addJournalEdition,
        updateJournalEdition: updateJournalEdition
    };
}]);
