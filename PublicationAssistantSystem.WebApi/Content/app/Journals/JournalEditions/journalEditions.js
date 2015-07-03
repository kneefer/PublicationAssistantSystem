"use strict";

var journalEditionsModule = angular.module("journalEditions");

journalEditionsModule.config(["$routeProvider", function ($routeProvider) {
    $routeProvider
        .when("/Journals/:JournalId/JournalEditions", {
            templateUrl: "Content/app/Journals/JournalEditions/journalEditions.html"
        });
}]);
