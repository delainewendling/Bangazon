"use strict";

app.controller("CustomerCtrl", function($scope, CustomerFactory, $route) {

    $scope.customers = [];
    $scope.customer = {};
    $scope.editMode = false;
    $scope.customerId = null;
    $scope.editedCustomer = {};

    CustomerFactory.getCustomers()
        .then((customerData) => {
            console.log("customer Data", customerData);
            $scope.customers = customerData;
        });

    $scope.addCustomer = () => {
        CustomerFactory.newCustomer($scope.customer)
            .then((customer) => {
                $scope.customers.push(customer);
            })
    }

    $scope.deleteCustomer = (customerId, index) => {
        CustomerFactory.deleteCustomer(customerId)
            .then((customer) => {
                $scope.customers.splice(index, 1);
            })
    }

    $scope.editCustomer = (customerId, index) => {

        CustomerFactory.getSingleCustomer(customerId)
            .then((customer) => {
                $scope.editMode = true;
                $scope.customerId = customerId;
                $scope.editedCustomer.firstName = customer.firstName;
                $scope.editedCustomer.lastName = customer.lastName;
            })
    }

    $scope.submitCustomerChanges = (customerId) => {
        $scope.editedCustomer.customerId = customerId;
        CustomerFactory.editCustomer(customerId, $scope.editedCustomer)
            .then((editedCustomer) => {
                $scope.editMode = false;
                $scope.customerId = null;
                $route.reload();
                console.log("edited customer", editedCustomer);
            })
    }

});