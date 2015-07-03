"use strict";

var journalEditionsModule = angular.module("journalEditions");

journalEditionsModule.directive("listJournalEditions", ["$routeParams", "JournalEditionFactory",
    function ($routeParams, JournalEditionFactory) {

    return {
        restrict: "E",
        templateUrl: "Content/app/Journals/JournalEditions/templates/journalEditionList.html",
        link: function ($scope, element, attrs) {
            JournalEditionFactory.getJournalEditions($routeParams.JournalId)
                .then(function (response) {
                    $scope.journalEditions = response.data;
                });
        }
    };

}]);

journalEditionsModule.directive("addJournalEdition", ["$routeParams", "JournalEditionFactory",
    function ($routeParams, JournalEditionFactory) {

    return {
        restrict: 'AE',
        templateUrl: "Content/app/Journals/JournalEditions/templates/addJournalEdition.html",
        link: function ($scope) {

            $scope.addJournalEdition = function () {

                JournalEditionFactory.addJournalEdition($scope.JournalEdition, $routeParams.JournalId)
                    .then(function (response) {
                        if (response.status == 201) {
                            JournalEditionFactory.getJournalEditions($routeParams.JournalId)
                                .then(function (response) {
                                    $scope.journalEditions = response.data;
                                })
                            $(".addition-succeded").slideDown("slow").delay(3000).slideUp("slow");
                        } else
                            alert("Error!");
                    });
            }

            $scope.showForm = function () {
                $(".add-item-form").slideToggle();
            }
        }
    }
}]);

journalEditionsModule.directive("editJournalEdition", ["JournalEditionFactory", "$routeParams",
    function (JournalEditionFactory, $routeParams) {
    return {
        restrict: 'E',
        scope: {},
        templateUrl: 'Content/app/journalEditions/templates/editJournal.html',
        link: function ($scope, element, attrs) {
            JournalEditionFactory.getJournalEdition($routeParams.JournalId)
                .then(function (response) {
                    $scope.Journal = response.data;
                });

            $scope.JournalEditionFactory = function () {
                JournalFactory.updateJournal($scope.Journal)
                .then(function (response) {
                    if (response.status = 201) {
                        $(".update-succeded").slideDown("slow").delay(3000).slideUp("slow");
                    }
                    else
                        alert("Error updating journal edition");
                });
            }

            $scope.showUpdateForm = function () {
                $(".update-item-button").slideToggle();
            }

        }
    }
}]);
