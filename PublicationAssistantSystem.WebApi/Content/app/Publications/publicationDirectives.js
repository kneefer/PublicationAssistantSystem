"use strict";

var publicationsModule = angular.module("publications");

publicationsModule.directive("listPublications", ["PublicationFactory", function (PublicationFactory) {

    return {
        restrict: "E",
        scope: {
            publicationType: "@type"
        },
        templateUrl: "Content/app/Publications/templates/publicationsTable.html",
        link: function ($scope, element, attrs) {

            PublicationFactory.getPublicationHandler($scope.publicationType).list()
            .then(function (response) {
                $scope.publications = response.data;
            }, function (response) {
                alert("error getting publications");
            });
            
        }
    };

}]);

publicationsModule.directive("showPublication", ["PublicationFactory", function (PublicationFactory) {

    return {
        restrict: "E",
        scope: {
            publicationType: "@type",
            publicationId: "@id"
        },
        templateUrl: "Content/app/Publications/templates/conferencePaperFields.html",
        link: function ($scope, element, attrs) {
            PublicationFactory.getPublicationHandler($scope.publicationType).get($scope.publicationId)
                .then(function (response) {
                    $scope.publication = response.data;
                }, function (response) {
                    alert("error creating publications");
                });
        }
    };

}]);

publicationsModule.directive("createPublication", ["PublicationFactory", "EmployeeFactory",
    function (PublicationFactory, EmployeeFactory) {

        function remapEmployeesByIds(employeesIds, allEmployees) {
            var ret = [];

            for (var i = 0; i < allEmployees.length; i++) {
                for (var j = 0; j < employeesIds.length; j++) {
                    if (allEmployees[i].Id == employeesIds[j]) {
                        ret.push(allEmployees[i]);
                    }
                }
            }

            return ret;
        }

    return {
        restrict: "E",
        scope: {
            publicationType: "@type"
        },
        templateUrl: "Content/app/Publications/templates/createPublication.html",
        link: function ($scope, element, attrs) {

            $scope.employees = [];

            EmployeeFactory.getAllEmployees()
            .then(function (response) {
                $scope.employees = response.data;
            }, function (response) {
                alert("error getting employees");
            });

            $scope.savePublication = function (publication) {

                publication.employees = remapEmployeesByIds(publication.Employees, $scope.employees);

                PublicationFactory.getPublicationHandler($scope.publicationType).create(publication)
                    .then(function (response) {
                        $scope.publication = response.data;
                        $(".addition-succeded").slideDown("slow").delay(3000).slideUp("slow");
                    }, function (response) {
                        alert("error creating publications");
                    });
            }

            $scope.showForm = function () {
                $(element).find("input").prop("disabled", false);
                $(".add-item-form").slideToggle();
            }
        }
    };

}]);

publicationsModule.directive("updatePublication", ["PublicationFactory", function (PublicationFactory) {

    return {
        restrict: "E",
        scope: {
            publicationType: "@type"
        },
        templateUrl: "Content/app/Publications/templates/conferencePaperFields.html",
        link: function ($scope, element, attrs) {
            PublicationFactory.getPublicationHandler($scope.publicationType).update($scope.publication)
                .then(function (response) {
                    $scope.publication = response.data;
                }, function (response) {
                    alert("error creating publications");
                });
        }
    };

}]);

publicationsModule.directive("publicationFields", ['$routeParams', function ($routeParams) {

    return {
        templateUrl: "Content/app/Publications/templates/publicationFields.html",
        link: function ($scope, element, attrs) {
            
        }
    }
}]);
