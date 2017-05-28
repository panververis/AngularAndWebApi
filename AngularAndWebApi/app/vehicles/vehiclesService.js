(function () {
    'use strict';

    angular
        .module('app.vehicles')
        .factory('vehiclesService', vehiclesService);

    // Injecting the dependencies
    vehiclesService.$inject = ['$http', '$log'];

    // Vehicles data service
    function vehiclesService($http, $log) {

        // Defining the service's exposed methods
        var service = {
            getVehicles:    getVehicles,
            deleteVehicle:  deleteVehicle
        };

        // Returning the above constructed method
        return service;

        // Function making an XHR call, fetching the Vehicles
        function getVehicles() {
            return $http.get('/api/Vehicles')
                .then(getVehiclesComplete)
                .catch(getVehiclesFailed);

            // Function handling a successful "getVehicles" return
            function getVehiclesComplete(response) {
                return response.data;
            }

            // Function handling an unsuccessful "getVehicles" return
            function getVehiclesFailed(error) {
                $log.error('XHR "getVehicles" failed.' + error.data);
            }

        }

        // Function making an XHR call, deleting the provided as input Vehicle
        function deleteVehicle(vehicle) {

            // Preparing the delete vehicle URL
            var delUrl = 'api/Vehicles/' + vehicle.id;

            // Making the "Delete" API call, getting and handling the promise
            return $http.delete(delUrl)
                .then(deleteVehicleComplete(vehicle))
                .catch(deleteVehicleFailed(vehicle));

            // Function handling a successful "deleteVehicles" return
            function deleteVehicleComplete(vehicle) {
                $log.info('Vehicle with ID: ' + vehicle.id + ' successfully deleted.');
            }

            // Function handling an unsuccessful "deleteVehicles" return
            function deleteVehicleFailed(vehicle) {
                $log.error('XHR "deleteVehicle" failed, for vehicle with ID: ' + vehicle.id + '.');
            }

        }

    }

})();
