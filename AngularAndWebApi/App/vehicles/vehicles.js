(function () {
    'use strict';

    angular
        .module('app.vehicles')
        .controller('Vehicles', Vehicles);

    // Injecting the dependencies
    Vehicles.$inject = ['vehiclesService'];

    // Vehicles Controller
    function Vehicles(vehiclesService) {

        var vm              = this;
        vm.vehiclesData     = [];
        vm.vehiclesCount    = 0;

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
    }
})();