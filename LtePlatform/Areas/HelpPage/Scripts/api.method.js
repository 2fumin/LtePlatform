app.controller("api.method", function ($scope, $http, $routeParams) {
    $scope.page.title = $routeParams.name;
    $scope.page.introduction = "Provide the description of this API controller here.";
    $http({
        method: 'GET',
        url: '/Help/ApiMethod',
        params: {
            controllerName: $routeParams.name
        }
    }).success(function(result) {
        $scope.methods = result;
    });
});