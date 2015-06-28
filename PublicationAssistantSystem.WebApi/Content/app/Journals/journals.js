"use strict";

var journalsModule = angular.module("journals");

journalsModule.config(["$routeProvider", function ($routeProvider) {
    $routeProvider
        .when("/Journals", {
            templateUrl: "Content/app/Journals/journals.html"
        })
        .when("/Journals/:JournalId/", {
            templateUrl: "Content/app/Journals/journalDetails.html"
        });
}]);
