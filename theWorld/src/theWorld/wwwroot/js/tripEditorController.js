﻿(function() {
    "use strict";
    angular.module("appTrips")
        .controller("tripEditorController", tripEditorController);

    function tripEditorController($routeParams, $http) {
        var vm = this;
        vm.tripName = $routeParams.tripName;
        vm.stops = [];
        vm.errorMessage = "";
        vm.isBusy = true;

        $http.get("/api/trips/" + vm.tripName + "/stops")
            .then(function(response) {
                angular.copy(response.data, vm.stops);
                    _showMap(vm.stops);
                },
            function(error) {
                vm.errorMessage = "Failed to load stops";
            })
        .finally(function() {
                vm.isBusy = false;
            });
    }

    function _showMap(stops) {//private function starts with _
        if (stops && stops.length > 0) {            
            //show map
            travelMap.createMap({
                stops: stops,
                selector: "#map",
                currentStop: 1,
                initialZoom:3
            });
        }
    }
})();