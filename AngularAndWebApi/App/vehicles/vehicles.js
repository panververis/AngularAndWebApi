(function () {
    'use strict';

    angular
        .module('app.vehicles')
        .controller('Vehicles', Vehicles);

    // injecting the dependencies
    Vehicles.$inject = ['$http'];

    // Vehicles Controller
    function Vehicles($http) {

        // getting a reference to the controller
        var vm = this;

        // the vehicles data that are to be displayed in the vehicles ("List") View
        var vehiclesData = [];

        // invoking the Controller's activation
        activate();

        function activate() {
            var response = $http.get('/api/Vehicles')
                .then(function (response) {
                    vehiclesData = response.data;
                    vm.title = 'Vehicles';
                    vm.vehiclesCount = vehiclesData.length;
                });
        }

    }
})();