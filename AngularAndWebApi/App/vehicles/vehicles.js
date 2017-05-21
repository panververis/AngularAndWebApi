(function () {
    'use strict';

    angular
        .module('app.vehicles')
        .controller('Vehicles', Vehicles);

    // Injecting the dependencies
    Vehicles.$inject = ['$http', 'vehiclesService'];

    // Vehicles Controller
    function Vehicles($http, vehiclesService) {

        var vm              = this;
        var vehiclesData    = [];
        var vehiclesCount   = 0;

        // Invoking the Controller's activation
        activate();

        // Function activating the Controller
        function activate() {
            var response        = vehiclesService.getVehicles();
            if (response){
                vehiclesData    = response.data;
                vehiclesCount   = response.count;
            }
        }

    }
})();