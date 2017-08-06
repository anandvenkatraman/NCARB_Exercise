'use strict';

// Declare app level module which depends on views, and components
var app = angular.module('myApp', [
    'ngRoute', 'ui.bootstrap',
    'myApp.view1',
    'myApp.view2',
    'myApp.home',
    'myApp.version'
]);

app.config(['$locationProvider', '$routeProvider', function ($locationProvider, $routeProvider) {
    $locationProvider.hashPrefix('!');
    $routeProvider.otherwise({ redirectTo: '/home' });
}]);