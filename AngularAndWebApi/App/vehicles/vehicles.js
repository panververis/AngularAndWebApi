(function () {
    'use strict';

    angular
        .module('app.vehicles')
        .controller('Vehicles', Vehicles);

    // Injecting the dependencies
    Vehicles.$inject = ['vehiclesService'];

    // Vehicles Controller
    function Vehicles(vehiclesService) {

        // Getting an instance of the Angular Controller (ViewModel)
        var vm              = this;

        // Controller properties
        vm.vehiclesData     = [];
        vm.vehiclesCount    = 0;
        vm.title            = 'Vehicles List View';
        vm.filterText       = "";

        // Controller functions
        vm.clearFilterText  = clearFilterText;
        vm.deleteVehicle    = deleteVehicle;

        // Invoking the Controller's activation
        activate();

        // Function activating the Controller
        function activate() {
            return getVehicles();
        }

        // Function handling the "vehicles service"'s "get vehicles" call
        function getVehicles() {
            return vehiclesService.getVehicles()
                .then(function (data) {
                    vm.vehiclesData = data;
                    vm.vehiclesCount = vm.vehiclesData.length;
                    return vm.vehiclesData;
                });
        }

        // Function clearing the filter text value
        function clearFilterText() {
            vm.filterText = "";
        }

        function deleteVehicle(vehicle) {
            if (vehicle == null) {
                return;
            }

            return vehiclesService.deleteVehicle(vehicle)
                .then(function () {
                    alert("Successful vehicle deletion!");
                })
                .catch(function () {
                    alert("Vehicle deletion failed!");
                });
        }
    }
})();