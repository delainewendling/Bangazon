"use strict";

var app = angular.module("Bangazon", ["ngRoute"])

app.config(function($routeProvider) {
    $routeProvider
        .when('/', {
            templateUrl: 'partials/homeView.html',
            controller: 'HomeCtrl'
        })
        .when('/bangazon/customers', {
            templateUrl: 'partials/customerView.html',
            controller: 'CustomerCtrl'
        })
        .when('/bangazon/products', {
            templateUrl: 'partials/productView.html',
            controller: 'ProductCtrl'
        })
});