(function () {
    'use strict';

    angular
        .module('app.vehicles')
        .controller('Vehicles', Vehicles);

    Attendees.$inject = [];

    function Vehicles() {

        var vm = this;
        vm.title = 'vehicles title';


        activate();

        function activate() {
            var a = 15;
        }

    }
})();