"use strict";

var journalsModule = angular.module("journals");

journalsModule.directive("listJournals", ["JournalFactory", function (JournalFactory) {

    return {
        restrict: "E",
        templateUrl: "Content/app/journals/templates/journalList.html",
        link: function ($scope, element, attrs) {
            JournalFactory.getAlljournals()
                .then(function (response) {
                    $scope.journals = response.data;
                })
        }
    };

}]);

journalsModule.directive("addJournal", ["JournalFactory", function (JournalFactory) {

    return {
        restrict: 'AE',
        templateUrl: "Content/app/journals/templates/addJournal.html",
        link: function ($scope) {

            $scope.addJournal = function () {

                JournalFactory.addJournal($scope.Journal)
                    .then(function (response) {
                        if (response.status == 201) {
                            JournalFactory.getAlljournals()
                                .then(function (response) {
                                    $scope.journals = response.data;
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

journalsModule.directive("editJournal", ["JournalFactory", "$routeParams", function (JournalFactory, $routeParams) {
    return {
        restrict: 'E',
        scope: {},
        templateUrl: 'Content/app/journals/templates/editJournal.html',
        link: function ($scope, element, attrs) {
            JournalFactory.getJournal($routeParams.JournalId)
                .then(function (response) {
                    $scope.Journal = response.data;
                });

            $scope.updateJournal = function () {
                JournalFactory.updateJournal($scope.Journal)
                .then(function (response) {
                    if (response.status = 201) {           
                        $(".update-succeded").slideDown("slow").delay(3000).slideUp("slow");
                    }
                    else
                        alert("Error!");
                });
            }

            $scope.showUpdateForm = function () {
                $(".update-item-form").slideToggle();
            }

        }
    }
}]);

journalsModule.directive("journalFields", ["JournalFactory", "$routeParams", function (JournalFactory, $routeParams) {
    return {
        restrict: 'E',
        templateUrl: 'Content/app/journals/templates/journalFields.html',
    }
}]);