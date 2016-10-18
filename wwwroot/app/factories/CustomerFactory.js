"use strict";

app.factory("CustomerFactory", ($q, $http) => {

    let customers = () => {
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

    return { getCustomers, getSingleCustomer };
});