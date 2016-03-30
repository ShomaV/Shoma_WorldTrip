(function() {
    "use strict";
    //Getting the exising module
    angular.module("appTrips")
        .controller("tripsController", tripsController);

    function tripsController($http) {
        var vm = this;// this refers the object returned from the trip controller
        vm.trips = [];

        vm.newTrip = {};
        vm.errorMessage = "";
        vm.isBusy = true;
        $http.get("/api/trips")
            .then(function(response) {
                //success
                angular.copy(response.data, vm.trips);
            }, function(error) {
                //failure
                vm.errorMessage = "Failed to load data: " + error; 
            }).finally(function() {
                vm.isBusy = false;
            });

        vm.addTrip = function () {
            vm.isBusy = true;
            $http.post("/api/trips", vm.newTrip)
                .then(function (response) {
                    vm.trips.push(response.data);
                    vm.newTrip = {};
                }, function(error) {
                    vm.errorMessage = "Failed to saved trip: " + error;
                }).finally(function() {
                    vm.isBusy = false;
                });
            //vm.trips.push({ name: vm.newTrip.name ,created:new Date()});
            //vm.newTrip = {};
        };
    }
})();

