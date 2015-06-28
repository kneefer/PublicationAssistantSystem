(function () {
    "use strict";
    
    angular.module("app", ["ngRoute", "publications", "faculties", "institutes", "divisions", "employees"]);
    angular.module("publications", ["ngRoute"]);
    angular.module("faculties", ["ngRoute"]);
    angular.module("institutes", ["ngRoute"]);
    angular.module("divisions", ["ngRoute"]);
    angular.module("employees", ["ngRoute"]);

})();