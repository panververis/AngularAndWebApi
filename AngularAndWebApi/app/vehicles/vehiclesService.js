(function () {
    'use strict';

    angular
        .module('app.vehicles')
        .factory('vehiclesService', vehiclesService);

    // Injecting the dependencies
    vehiclesService.$inject = ['$http', '$log'];

    // Vehicles data service
    function vehiclesService($http, $log) {

        var service = {
            getVehicles: getVehicles
        };

        return service;

        function getVehicles() {
            return $http.get('/api/Vehicles')
                .then(getVehiclesComplete)
                .catch(getVehiclesFailed);

            // Function handling a successful return
            function getVehiclesComplete(response) {
                return response.data;
            }

            // Function handling an unsuccessful return
            function getVehiclesFailed(error) {
                $log.error('XHR Failed for getVehicles.' + error.data);
            }
        }
    }
})();
