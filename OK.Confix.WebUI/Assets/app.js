var app = angular.module('app', ['ngRoute']);

app.config(function ($routeProvider) {
    $routeProvider
        .when('/', {
            templateUrl: 'home',
            controller: 'homeCtrl'
        })
        .when('/applications/create', {
            templateUrl: 'applications/create',
            controller: 'createApplicationCtrl'
        })
        .when('/applications/edit/:id', {
            templateUrl: 'applications/edit',
            controller: 'editApplicationCtrl'
        })
        .when('/applications/delete/:id', {
            templateUrl: 'applications/delete',
            controller: 'deleteApplicationCtrl'
        })
        .when('/environments/create', {
            templateUrl: 'environments/create',
            controller: 'createEnvironmentCtrl'
        })
        .when('/environments/edit/:id', {
            templateUrl: 'environments/edit',
            controller: 'editEnvironmentCtrl'
        })
        .when('/environments/delete/:id', {
            templateUrl: 'environments/delete',
            controller: 'deleteEnvironmentCtrl'
        })
        .when('/configurations/create', {
            templateUrl: 'configurations/create',
            controller: 'createConfigurationCtrl'
        })
        .when('/configurations/edit/:id', {
            templateUrl: 'configurations/edit',
            controller: 'editConfigurationCtrl'
        })
        .when('/configurations/delete/:id', {
            templateUrl: 'configurations/delete',
            controller: 'deleteConfigurationCtrl'
        });
});

app.controller('homeCtrl', function ($scope, $http) {

    $scope.getApplications = function () {
        $http.get('api/applications/list')
            .then(function (response) {
                $scope.applications = response.data.applicationList;

                $scope.getEnvironments();
            });
    };

    $scope.getEnvironments = function () {
        $http.get('api/environments/list')
            .then(function (response) {
                $scope.environments = response.data.environmentList;

                $scope.getConfigurations();
            });
    };

    $scope.getConfigurations = function () {
        $http.get('api/configurations/list')
            .then(function (response) {
                $scope.configurations = response.data.configurationList;
            });
    };

    $scope.getApplications();

});

app.controller('createApplicationCtrl', function ($scope, $routeParams, $location, $http) {

    $scope.save = function () {
        $http.post('api/applications/create', { name: $scope.application.name })
            .then(function (response) {
                $location.path('');
            });
    };

    $scope.cancel = function () {
        $location.path('');
    };

});

app.controller('editApplicationCtrl', function ($scope, $routeParams, $location, $http) {

    $http.post('api/applications/details', { id: $routeParams.id })
        .then(function (response) {
            $scope.application = response.data.application;
        });

    $scope.save = function () {
        $http.post('api/applications/edit', { id: $routeParams.id, name: $scope.application.name })
            .then(function (response) {
                $location.path('');
            });
    };

    $scope.cancel = function () {
        $location.path('');
    };

});

app.controller('deleteApplicationCtrl', function ($scope, $routeParams, $location, $http) {

    $http.post('api/applications/details', { id: $routeParams.id })
        .then(function (response) {
            $scope.application = response.data.application;
        });

    $scope.save = function () {
        $http.post('api/applications/delete', { id: $routeParams.id })
            .then(function (response) {
                $location.path('');
            });
    };

    $scope.cancel = function () {
        $location.path('');
    };

});

app.controller('createEnvironmentCtrl', function ($scope, $routeParams, $location, $http) {

    $scope.getApplications = function () {
        $http.get('api/applications/list')
            .then(function (response) {
                $scope.applications = response.data.applicationList;
            });
    };

    $scope.save = function () {
        $http.post('api/environments/create', { applicationId: $scope.environment.applicationId, name: $scope.environment.name })
            .then(function (response) {
                $location.path('');
            });
    };

    $scope.cancel = function () {
        $location.path('');
    };

    $scope.getApplications();

});

app.controller('editEnvironmentCtrl', function ($scope, $routeParams, $location, $http) {

    $scope.getApplications = function () {
        $http.get('api/applications/list')
            .then(function (response) {
                $scope.applications = response.data.applicationList;

                $scope.getEnvironmentDetails();
            });
    };

    $scope.getEnvironmentDetails = function () {
        $http.post('api/environments/details', { id: $routeParams.id })
            .then(function (response) {
                $scope.environment = response.data.environment;
            });
    };

    $scope.save = function () {
        $http.post('api/environments/edit', { id: $routeParams.id, applicationId: $scope.environment.applicationId, name: $scope.environment.name })
            .then(function (response) {
                $location.path('');
            });
    };

    $scope.cancel = function () {
        $location.path('');
    };

    $scope.getApplications();

});

app.controller('deleteEnvironmentCtrl', function ($scope, $routeParams, $location, $http) {

    $http.post('api/environments/details', { id: $routeParams.id })
        .then(function (response) {
            $scope.environment = response.data.environment;
        });

    $scope.save = function () {
        $http.post('api/environments/delete', { id: $routeParams.id })
            .then(function (response) {
                $location.path('');
            });
    };

    $scope.cancel = function () {
        $location.path('');
    };

});

app.controller('createConfigurationCtrl', function ($scope, $routeParams, $location, $http) {

    var environments;

    $scope.getApplications = function () {
        $http.get('api/applications/list')
            .then(function (response) {
                $scope.applications = response.data.applicationList;

                $scope.getEnvironments();
            });
    };

    $scope.getEnvironments = function () {
        $http.get('api/environments/list')
            .then(function (response) {
                environments = response.data.environmentList;
            });
    };

    $scope.updateEnvironments = function () {
        $scope.environments = [];

        if ($scope.configuration.applicationId) {
            $scope.environments = environments.filter(function (item) { return item.applicationId == $scope.configuration.applicationId; });
        }
    };

    $scope.save = function () {
        $http.post('api/configurations/create', { applicationId: $scope.configuration.applicationId, environmentId: $scope.configuration.environmentId == 0 ? null : $scope.configuration.environmentId, name: $scope.configuration.name, value: $scope.configuration.value })
            .then(function (response) {
                $location.path('');
            });
    };

    $scope.cancel = function () {
        $location.path('');
    };

    $scope.getApplications();

});

app.controller('editConfigurationCtrl', function ($scope, $routeParams, $location, $http) {

    var environments;

    $scope.getApplications = function () {
        $http.get('api/applications/list')
            .then(function (response) {
                $scope.applications = response.data.applicationList;

                $scope.getEnvironments();
            });
    };

    $scope.getEnvironments = function () {
        $http.get('api/environments/list')
            .then(function (response) {
                environments = response.data.environmentList;

                $scope.getConfigurationDetails();
            });
    };

    $scope.getConfigurationDetails = function () {
        $http.post('api/configurations/details', { id: $routeParams.id })
            .then(function (response) {
                $scope.configuration = response.data.configuration;

                $scope.updateEnvironments();
            });
    };

    $scope.updateEnvironments = function () {
        $scope.environments = [];

        if ($scope.configuration.applicationId) {
            $scope.environments = environments.filter(function (item) { return item.applicationId == $scope.configuration.applicationId; });
        }
    };
    
    $scope.save = function () {
        var environment = $scope.environments.filter(function (item) { return item.id == $scope.configuration.environmentId; })[0];

        $http.post('api/configurations/edit', { id: $routeParams.id, applicationId: environment.applicationId, environmentId: $scope.configuration.environmentId == 0 ? null : $scope.configuration.environmentId, name: $scope.configuration.name, value: $scope.configuration.value })
            .then(function (response) {
                $location.path('');
            });
    };

    $scope.cancel = function () {
        $location.path('');
    };

    $scope.getApplications();

});

app.controller('deleteConfigurationCtrl', function ($scope, $routeParams, $location, $http) {

    $http.post('api/configurations/details', { id: $routeParams.id })
        .then(function (response) {
            $scope.configuration = response.data.configuration;
        });

    $scope.save = function () {
        $http.post('api/configurations/delete', { id: $routeParams.id })
            .then(function (response) {
                $location.path('');
            });
    };

    $scope.cancel = function () {
        $location.path('');
    };

});