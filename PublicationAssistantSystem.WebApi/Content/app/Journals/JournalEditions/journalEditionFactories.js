"use strict";

var journalEditionsModule = angular.module("journalEditions");

journalEditionsModule.factory("JournalEditionFactory", ["$http", "$q", "JournalFactory", 
    function ($http, $q, JournalFactory) {


    var getAllJournalEditions = function () {
        return $http.get("/api/JournalEditions/");
    }

    var getJournalEditions = function (journalId) {
        return $http.get("/api/Journals/" + journalId + "/JournalEditions");
    }

    var getJournalEdition = function (journalEditionid) {
        return $http.get("/api/JournalEditions/" + journalEditionid);
    }

    var getJournalEditionWithJournal = function (journalEditionId) {
        var journalEdition = {};

        return $q(function (resolve, reject) {
            getJournalEdition(journalEditionId)
            .then(function (response) {
                journalEdition = response.data;

                JournalFactory.getJournal(journalEdition.JournalId)
                .then(function (response) {
                    journalEdition.Journal = response.data;
                    response.data = journalEdition;
                    resolve(response);
                }, function (response) {
                    reject();
                });
            }, function (response) {
                reject();
            });
        });
    }

    function mapJournalsToJournalEditions(journals, journalEditions) {
        for (var i = 0; i < journals.length; i++) {
            for (var j = 0; j < journalEditions.length; j++) {
                if (journalEditions[j].JournalId == journals[i].Id) {
                    journalEditions[j].Journal = journals[i];
                }
            }
        }
        return journalEditions;
    }

    var getAllWithJournals = function () {
        var journals = [];
        var journalEditions = [];

        return $q(function (resolve, reject) {
            JournalFactory.getAlljournals()
            .then(function (response) {
                journals = response.data;

                getAllJournalEditions()
                .then(function (response) {
                    journalEditions = response.data;

                    journalEditions = mapJournalsToJournalEditions(journals, journalEditions);
                    response.data = journalEditions;
                    resolve(response);
                }, function (response) {
                    reject();
                });

            }, function (response) {
                reject();
            });
        });
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
        updateJournalEdition: updateJournalEdition,
        getAllWithJournals: getAllWithJournals,
        getAllJournalEditions: getAllJournalEditions,
        getJournalEditionWithJournal: getJournalEditionWithJournal
    };
}]);
