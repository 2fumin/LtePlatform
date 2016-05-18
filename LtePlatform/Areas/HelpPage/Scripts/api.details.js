app.controller("api.details", function ($scope, $http, $routeParams) {
    $scope.method = $routeParams.method;
    $http({
        method: 'GET',
        url: '/Help/ApiActionDoc',
        params: {
            apiId: $routeParams.apiId
        }
    }).success(function (result) {
        $scope.page.title = result.RelativePath;
        $scope.page.introduction = result.ResponseDoc;
        $scope.parameters = result.ParameterDescriptions;
        $scope.bodyModel = result.FromBodyModel;
        $scope.responseModel = result.ResponseModel;
    });
});