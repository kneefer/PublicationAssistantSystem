"use strict";

var publicationsModule = angular.module("publications");

publicationsModule.factory("PublicationFactory", ["$http", "$routeParams", "$location", "$q", "JournalEditionFactory",
    function ($http, $routeParams, $location, $q, JournalEditionFactory) {
    
    /* handlers format:
    ** list() - get whole repository
    ** get(id) - get publication with specified id
    ** create(publication) - create new publication
    ** update(publication) - update publication
    ** delete(publication) - delete publication
    */

    var translations = [
        ['All', 'Wszystkie publikacje'],
        ['ConferencePapers', 'Dokumenty konferencyjne'],
        ['TechnicalReports', 'Raporty techniczne'],
        ['Theses', 'Dowody'],
        ['Books', 'Książki'],
        ['Patents', 'Patenty'],
        ['Articles', 'Artykuły'],
        ['Datasets', 'Zestawy danych']
    ];

    var getTypeFromUrl = function () {
        var ret = null;

        translations.forEach(function (element) {
            if (element[0] == $routeParams.type) {
                ret = element;
            }
        });

        return ret;
    }

    var getPathFromUrl = function () {
        var path = $location.path().split('/').slice(1);
        var ret = [];

        for (var i = 0; i < path.length; i++) {
            for (var j = 0; j < translations.length; j++) {
                if (translations[j][0] == path[i]) {
                    ret.push(translations[j]);
                }
            }
        }
        return ret;
    }

    var handlers = function (publicationType) {
        switch (publicationType) {
            case 'ConferencePapers':
                return conferencePapersHandlers;
                break;
            case 'TechnicalReports':
                return technicalReportsHandlers;
                break;
            case 'Theses':
                return thesesHandlers;
                break;
            case 'Books':
                return booksHandlers;
                break;
            case 'Patents':
                return patentsHandlers;
                break;
            case 'Articles':
                return articlesHandlers;
                break;
            case 'Datasets':
                return datasetsHandlers;
                break;
            default:
                return commonHandlers
                break;
        }
    }

    // all publications code
    var getAllPublications = function () {
        return $http.get("/api/Publications/All");
    }

    var getPublication = function (publicationId) {
        return $http.get("/api/Publications/All/" + publicationId);
    }

    var getPublicationOfEmployee = function (employeeId) {
        return $http.get("/api/Employees/" + employeeId + "/Publications");
    }

    var commonHandlers = {
        list: getAllPublications,
        get: getPublication,
        create: null,
        update: null
    };

    // conference papers code
    var getAllConferencePapers = function () {
        return $http.get("/api/Publications/ConferencePapers");
    }

    var getConferencePaper = function (id) {
        return $http.get("/api/Publications/ConferencePapers/" + id);
    }

    var addConferencePaper = function (publication) {
        return $http.post("/api/Publications/ConferencePapers", publication);
    }

    var updateConferencePaper = function (publication) {
        return $http.put("/api/Publications/ConferencePapers", publication)
    }

    var conferencePapersHandlers = {
        list: getAllConferencePapers,
        get: getConferencePaper,
        create: addConferencePaper,
        update: updateConferencePaper
    };

    // technical reports code
    var getAllTechnicalReports = function () {
        return $http.get("/api/Publications/TechnicalReports");
    }

    var getTechnicalReport = function (id) {
        return $http.get("/api/Publications/TechnicalReports/" + id);
    }

    var addTechnicalReport = function (publication) {
        return $http.post("/api/Publications/TechnicalReports", publication);
    }

    var updateTechnicalReport = function (publication) {
        return $http.put("/api/Publications/TechnicalReports", publication)
    }

    var technicalReportsHandlers = {
        list: getAllTechnicalReports,
        get: getTechnicalReport,
        create: addTechnicalReport,
        update: updateTechnicalReport
    };

    // theses code
    var getAllTheses = function () {
        return $http.get("/api/Publications/Theses");
    }

    var getThesis = function (id) {
        return $http.get("/api/Publications/Theses/" + id);
    }

    var addThesis = function (publication) {
        return $http.post("/api/Publications/Theses", publication);
    }

    var updateThesis = function (publication) {
        return $http.put("/api/Publications/Theses", publication)
    }

    var thesesHandlers = {
        list: getAllTheses,
        get: getThesis,
        create: addThesis,
        update: updateThesis
    };

    // datasets code
    var getAllDatasets = function () {
        return $http.get("/api/Publications/Datasets");
    }

    var getDataset = function (id) {
        return $http.get("/api/Publications/Datasets/" + id);
    }

    var addDataset = function (publication) {
        return $http.post("/api/Publications/Datasets", publication);
    }

    var updateDataset = function (publication) {
        return $http.put("/api/Publications/Datasets", publication)
    }

    var datasetsHandlers = {
        list: getAllDatasets,
        get: getDataset,
        create: addDataset,
        update: updateDataset
    };

    // books code
    var getAllBooks = function () {
        return $http.get("/api/Publications/Books");
    }

    var getBook = function (id) {
        return $http.get("/api/Publications/Books/" + id);
    }

    var addBook = function (publication) {
        return $http.post("/api/Publications/Books", publication);
    }

    var updateBook = function (publication) {
        return $http.put("/api/Publications/Books", publication)
    }

    var booksHandlers = {
        list: getAllBooks,
        get: getBook,
        create: addBook,
        update: updateBook
    };

    // patents code
    var getAllPatents = function () {
        return $http.get("/api/Publications/Patents");
    }

    var getPatent = function (id) {
        return $http.get("/api/Publications/Patents/" + id);
    }

    var addPatent = function (publication) {
        return $http.post("/api/Publications/Patents", publication);
    }

    var updatePatent = function (publication) {
        return $http.put("/api/Publications/Patents", publication)
    }

    var patentsHandlers = {
        list: getAllPatents,
        get: getPatent,
        create: addPatent,
        update: updatePatent
    };

    // articles code
    var getAllArticles = function () {
        return $http.get("/api/Publications/Articles");
    }

    function getArticlesJournalEditions(articles) {
        if (articles == null) {
            return $q(function (resolve, reject) {
                resolve({ data: null });
            });
        }

        var responses = 0;

        return $q(function (resolve, reject) {
            for (var i = 0; i < articles.length; i++) {
                JournalEditionFactory.getJournalEditionWithJournal(articles[i].JournalEditionId)
                .then(function (response) {
                    articles[i];
                })
            }

            while (responses - articles.length == 0) { }
            resolve({ data: articles });
        });
    }

    var getAllArticlesWithJournalEditionsAndJournals = function () {
        var articles = [];

        return $q(function (resolve, reject) {
            getAllArticles()
            .then(function (response) {
                articles = response.data;
                getArticlesJournalEditions(articles)
                .then(function (response) {
                    articles = response.data;
                    resolve(response);
                }, function (response) { reject(); });
            }, function (response) { reject(); });
        });
    }

    var getArticle = function (id) {
        return $http.get("/api/Publications/Articles/" + id);
    }

    var addArticle = function (publication) {
        return $http.post("/api/Publications/Articles", publication);
    }

    var updateArticle = function (publication) {
        return $http.put("/api/Publications/Articles", publication)
    }

    var articlesHandlers = {
        list: getAllArticlesWithJournalEditionsAndJournals,
        get: getArticle,
        create: addArticle,
        update: updateArticle
    };

    return {
        getPublicationHandler: handlers,
        translations: translations,
        getTypeFromUrl: getTypeFromUrl,
        getPathFromUrl: getPathFromUrl
    };
}]);
