'use strict';

angular.module('myApp.home', ['ngRoute'])

    .config(['$routeProvider', function ($routeProvider) {
        $routeProvider.when('/home', {
            templateUrl: 'home/home.html',
            controller: 'HomeCtrl'
        });
    }])

    .controller('HomeCtrl', function ($scope, $modal, Data) {
        $scope.person = {};
        $scope.columns = [
            { text: "First Name", predicate: "FirstName" },
            { text: "Last Name", predicate: "LastName" },
            { text: "Job Title", predicate: "JobTitle" },
            { text: "Action", predicate: "" }
        ];

        Data.get('Persons').then(function (data) {
            $scope.persons = data;
        });

        $scope.open = function (p) {
            var modalInstance = $modal.open({
                templateUrl: 'home/personEdit.html',
                controller: 'PersonEditCtrl',
                resolve: {
                    item: function () {
                        return p;
                    }
                }
            });
            modalInstance.result.then(function (o) {
                p.FirstName = o.FirstName;
                p.LastName = o.LastName;
                p.JobTitle = o.JobTitle;
            });
        };

    });


app.controller('PersonEditCtrl', function ($scope, $modalInstance, item, Data, $timeout) {
    $scope.isOK = false;
    $scope.isNotOK = false;
    $scope.person = angular.copy(item);

    $scope.cancel = function () {
        $modalInstance.dismiss('Close');
    };

    var original = item;
    $scope.isClean = function () {
        return angular.equals(original, $scope.person);
    }
    $scope.savePerson = function (person) {
        $scope.AlertMessage = false;
        Data.post('Persons', person).then(function (result) {
            console.log(result);
            if (result.status == '200') {
                $scope.isOK = true;
                var x = angular.copy(person);
                $timeout(function () {
                    $modalInstance.close(x);
                }, 1500);
                
            } else {
                $scope.isNotOK = true;
                $timeout(function () {
                    $modalInstance.close();
                }, 1500);
            }
        });
    };
});
