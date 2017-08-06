app.factory("Data", ['$http', '$location',
    function ($http, $q, $location) {

        var serviceBase = 'http://localhost:3928/api/';

        var obj = {};
       
        obj.get = function (q) {
            return $http.get(serviceBase + q).then(function (results) {
                return results.data;
            });
        };
        obj.post = function (q, object) {
            return $http.post(serviceBase + q, object).then(function (result) {
                return result;
            });
        };
        obj.put = function (q, object) {
            return $http.put(serviceBase + q, object).then(function (result) {
                return result;
            });
        };
        obj.delete = function (q) {
            return $http.delete(serviceBase + q).then(function (result) {
                return result;
            });
        };
        return obj;
    }]);
