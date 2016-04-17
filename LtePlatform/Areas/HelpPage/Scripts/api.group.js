app.controller("api.group", function ($scope, $http) {
    $scope.page.title = "Introduction";
    $scope.page.introduction = "Provide a general description of your APIs here.";
    $http({
        method: 'GET',
        url: '/Help/ApiDescriptions'
    }).success(function(result) {
        $scope.apiDescription = result;
    });
});