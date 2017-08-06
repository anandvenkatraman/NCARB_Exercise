'use strict';

angular.module('myApp.home', ['ngRoute'])

    .config(['$routeProvider', function ($routeProvider) {
        $routeProvider.when('/home', {
            templateUrl: 'home/home.html',
            controller: 'HomeCtrl'
        });
    }])
    .filter('startFrom', function() {
        return function(input, start) {
            if(input) {
                start = +start; //parse to int
                return input.slice(start);
            }
            return [];
        }
    })
    .controller('HomeCtrl', function ($scope, $modal, Data, $timeout) {
        $scope.person = {};
        $scope.currentPage = 1;
        $scope.entryLimit = 5; 
       
        $scope.setPage = function(pageNo) {
            $scope.currentPage = pageNo;
        };
        $scope.filter = function() {
            $timeout(function() { 
                $scope.filteredItems = $scope.filtered.length;
            }, 10);
        };
        $scope.sort_by = function(predicate) {
            $scope.predicate = predicate;
            $scope.reverse = !$scope.reverse;
        };
        $scope.updateList = function(){
            Data.get('Persons').then(function (data) {
                $scope.persons = data;
                $scope.filteredItems = $scope.persons.length; 
                $scope.totalItems = $scope.persons.length;
            });
        }

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
            modalInstance.result.then(function () {
                $scope.updateList();
            });
        };

        $scope.deletePerson = function(p){
        if(confirm("Are you OK to delete (" + p.FirstName + " " + p.LastName + " - " + p.JobTitle + ")?")){
            Data.delete("persons/"+p.Id).then(function(result){
                    $scope.updateList();
                });
            }
        };
        $scope.deleteAll = function(){
        if(confirm("Are you OK to delete all persons data?")){
            Data.put("persons/deleteAll").then(function(result){
                    $scope.updateList();
                });
            }
        };
        $scope.generatePersons = function(){
        if(confirm("Are you OK to generate a list of persons?")){
            Data.put("persons/generateData").then(function(result){
                    $scope.updateList();
                });
            }
        };
        $scope.updateList();
    });


app.controller('PersonEditCtrl', function ($scope, $modalInstance, item, Data, $timeout) {
    $scope.isOK = false;
    $scope.isNotOK = false;
    $scope.person = angular.copy(item);

    $scope.cancel = function () {
        $modalInstance.dismiss('Close');
    };
    $scope.title = (item.Id > 0) ? 'Update Person' : 'Add Person';
    $scope.buttonText = (item.Id > 0) ? 'Update' : 'Add';
    
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
            } 
            else {
                $scope.isNotOK = true;
            }
            $timeout(function () {
                    $modalInstance.close();
                }, 1500);
        });
    };
});
