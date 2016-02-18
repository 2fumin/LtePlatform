
app.controller("rutrace.index", function ($scope) {
    $scope.page.title = "指标总体情况";
    var yesterday = new Date();
    yesterday.setDate(yesterday.getDate() - 1);
    $scope.statDate = {
        value: yesterday,
        opened: false
    };
    $scope.city = {
        sekected: "佛山",
        options: ["佛山", "广州"]
    };
});
