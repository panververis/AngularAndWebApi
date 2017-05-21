angular
    .module('app.vehicles')
    .factory('vehiclesService', vehiclesService);

// Injecting the dependencies
vehiclesService.$inject = ['$http', '$log'];

// Vehicles data service
function vehiclesService($http, $log) {
    return {
        getVehicles: getVehicles
    };

    function getVehicles() {
        return $http.get('/api/Vehicles')
            .then(getVehiclesComplete)
            .catch(getVehiclesFailed);

        // Function handling a successful return
        function getAvengersComplete(response) {

            // Grabbing the response's data in local variables
            var vehiclesData        = response.data.results;
            var vehiclesCount       = vehiclesData.length;

            // Initializing the "vehicles response" object
            var vehiclesResponse    = [];
            vehiclesResponse.data   = vehiclesData;
            vehiclesResponse.count = vehiclesCount;

            // Lastly returning the above prepared "vehicles response object"
            return response.data.results;
        }

        // Function handling an unsuccessful return
        function getVehiclesFailed(error) {
            $log.error('XHR Failed for getVehicles.' + error.data);
        }
    }
}