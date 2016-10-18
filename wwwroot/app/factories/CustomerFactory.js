"use strict";

app.factory("CustomerFactory", ($q, $http) => {

    let getCustomers = () => {
        return $q((resolve, reject) => {
            $http.get("/customers")
                .success((customerData) => {
                    resolve(customerData);
                })
                .error((error) => {
                    reject(error);
                });
        });
    };

    let getSingleCustomer = (customerId) => {
        return $q((resolve, reject) => {
            $http.get(`/customers/${customerId}`)
                .success((customerData) => {
                    resolve(customerData);
                })
                .error((error) => {
                    reject(error);
                });
        });
    };

    let newCustomer = (customerObj) => {
        return $q((resolve, reject) => {
            $http.post(`/customers`, angular.toJson(customerObj))
                .success((customerData) => {
                    resolve(customerData);
                })
                .error((error) => {
                    reject(error);
                });
        });
    };

    let editCustomer = (customerId, customerObj) => {
        return $q((resolve, reject) => {
            $http.put(`/customers/${customerId}`, angular.toJson(customerObj))
                .success((customerData) => {
                    resolve(customerData);
                })
                .error((error) => {
                    reject(error);
                });
        });
    };

    let deleteCustomer = (customerId) => {
        return $q((resolve, reject) => {
            $http.delete(`/customers/${customerId}`)
                .success((customerData) => {
                    resolve(customerData);
                })
                .error((error) => {
                    reject(error);
                });
        });
    };

    return { getCustomers, getSingleCustomer, newCustomer, deleteCustomer, editCustomer };
});