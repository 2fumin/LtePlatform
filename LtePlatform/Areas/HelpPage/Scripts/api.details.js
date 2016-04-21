app.controller("api.details", function ($scope, $http, $routeParams) {
    $scope.page.title = $routeParams.apiId;
    $scope.method = $routeParams.method;
    $scope.page.introduction = "Provide the description of this API action here.";
    $http({
        method: 'GET',
        url: '/Help/ApiDetails',
        params: {
            controllerName: $routeParams.apiId
        }
    }).success(function (result) {
        console.log(result);
    });
});