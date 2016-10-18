"use strict";

app.controller("CustomerCtrl", function($scope, CustomerFactory) {

    $scope.customers = [];
    $scope.customer = {};

    CustomerFactory.getCustomers()
        .then((customerData) => {
            console.log("customer Data", customerData);
            $scope.customers = customerData;
        });

    $scope.addCustomer = () => {
        console.log("new customer clicked");
        CustomerFactory.newCustomer($scope.customer)
        .then((customer)=>{
            $scope.customers.push(customer);
        })
    }

});