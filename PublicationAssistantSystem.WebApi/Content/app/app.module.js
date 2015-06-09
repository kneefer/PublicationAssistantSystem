(function () {
    "use strict";
    
    angular.module("app", ["ngRoute", "publications", "faculties", "institutes", "divisions"]);
    angular.module("publications", ["ngRoute"]);
    angular.module("faculties", ["ngRoute"]);
    angular.module("institutes", ["ngRoute"]);
    angular.module("divisions", ["ngRoute"]);

})();