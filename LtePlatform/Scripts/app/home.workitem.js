app.controller("home.workitem", function ($scope, workitemService) {
    workitemService.queryCurrentMonth().then(function (result) {
        console.log(result);
    });
});