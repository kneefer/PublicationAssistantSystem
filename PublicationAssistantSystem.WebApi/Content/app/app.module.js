(function () {
    "use strict";
    
    angular.module("app", ["ngRoute", "publications", "faculties", "institutes", "divisions", "employees", "journals", "journalEditions"]);
    angular.module("publications", ["ngRoute"]);
    angular.module("faculties", ["ngRoute"]);
    angular.module("institutes", ["ngRoute"]);
    angular.module("divisions", ["ngRoute"]);
    angular.module("employees", ["ngRoute"]);
    angular.module("journals", ["ngRoute"])
    angular.module("journalEditions", ["ngRoute"])

})();