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
            $scope.publicationType = "default";

            PublicationFactory.getAllPublications()
                .then(function (response) {
                    $scope.publications = response.data;
                })
        }
    };

}]);

publicationsModule.directive("listConferencePapers", ["PublicationFactory", function (PublicationFactory) {

    return {
        restrict: "E",
        scope: {},
        templateUrl: "Content/app/Publications/templates/publicationList.html",
        link: function ($scope, element, attrs) {
            $scope.publicationType = "ConferencePapers";

            PublicationFactory.getAllConferencePapers()
                .then(function (response) {
                    $scope.publications = response.data;
                })
        }
    };

}]);

publicationsModule.directive("addConferencePaper", ["PublicationFactory", function (PublicationFactory) {

    return {
        restrict: "E",
        scope: {},
        templateUrl: "Content/app/Publications/templates/conferencePaperFields.html",
        link: function ($scope, element, attrs) {
            PublicationFactory.getAllConferencePapers()
                .then(function (response) {
                    $scope.publications = response.data;
                })
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
