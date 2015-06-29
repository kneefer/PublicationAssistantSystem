"use strict";

var publicationsModule = angular.module("publications");

publicationsModule.directive("listPublications", ["PublicationFactory", function (PublicationFactory) {

    return {
        restrict: "E",
        scope: {
            publicationType: "@type"
        },
        templateUrl: "Content/app/Publications/templates/publicationList.html",
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

publicationsModule.directive("createPublication", ["PublicationFactory", function (PublicationFactory) {

    return {
        restrict: "E",
        scope: {
            publicationType: "@type"
        },
        templateUrl: "Content/app/Publications/templates/conferencePaperFields.html",
        link: function ($scope, element, attrs) {
            PublicationFactory.getPublicationHandler($scope.publicationType).create($scope.publication)
                .then(function (response) {
                    $scope.publication = response.data;
                }, function (response) {
                    alert("error creating publications");
                });
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

publicationsModule.directive("publicationFields", [function () {
    return {
        templateUrl: "Content/app/Publications/templates/publicationFields.html"
    }
}]);

publicationsModule.directive("conferencePaperFields", [function () {
    return {
        templateUrl: "Content/app/Publications/templates/conferencePaperFields.html"
    }
}]);

publicationsModule.directive("technicalReportFields", [function () {
    return {
        templateUrl: "Content/app/Publications/templates/technicalReportFields.html"
    }
}]);

publicationsModule.directive("thesisFields", [function () {
    return {
        templateUrl: "Content/app/Publications/templates/thesisFields.html"
    }
}]);

publicationsModule.directive("bookFields", [function () {
    return {
        templateUrl: "Content/app/Publications/templates/bookFields.html"
    }
}]);

publicationsModule.directive("patentFields", [function () {
    return {
        templateUrl: "Content/app/Publications/templates/patentFields.html"
    }
}]);

publicationsModule.directive("articleFields", [function () {
    return {
        templateUrl: "Content/app/Publications/templates/articleFields.html"
    }
}]);

publicationsModule.directive("datasetFields", [function () {
    return {
        templateUrl: "Content/app/Publications/templates/datasetFields.html"
    }
}]);
