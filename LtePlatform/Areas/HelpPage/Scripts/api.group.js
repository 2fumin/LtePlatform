app.controller("api.group", function($scope, $http) {
    $http({
        method: 'GET',
        url: '/Help/ApiDescriptions'
    }).success(function(result) {
        $scope.apiDescription = result;
    });
});