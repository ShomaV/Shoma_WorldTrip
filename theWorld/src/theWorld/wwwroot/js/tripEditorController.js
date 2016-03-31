(function() {
    "use strict";
    angular.module("appTrips")
        .controller("tripEditorController", tripEditorController);

    function tripEditorController($routeParams, $http) {
        var vm = this;
        vm.tripName = $routeParams.tripName;
        vm.stops = [];
        vm.errorMessage = "";
        vm.isBusy = true;
        vm.newStop = {};
        var url = "/api/trips/" + vm.tripName + "/stops";

        $http.get(url)
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

        vm.addStop = function() {
            vm.isBusy = true;
            $http.post(url, vm.newStop)
                .then(function() {
                    vm.stops.push(response.data);
                    _showMap(vm.stops);
                    vm.newStop = {};
                    },
                    function() {
                        vm.errorMessage = "Failed to add new stop";
                    })
                .finally(function() {
                    vm.isBusy = false;
                });
        };
    }

    function _showMap(stops) {//private function starts with _
        if (stops && stops.length > 0) {            
            //show map
            var mapStops = _.map(stops,function(item) {
                return {
                    lat: item.latitude,
                    long: item.longitude,
                    info:item.name
                }
            });
            travelMap.createMap({
                stops: mapStops,
                selector: "#map",
                currentStop: 1,
                initialZoom:3
            });
        }
    }
})();