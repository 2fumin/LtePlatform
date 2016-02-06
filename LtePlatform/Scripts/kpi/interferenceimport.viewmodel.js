app.controller("interference.import", function($scope, $http) {
    $scope.totalDumpItems = 0;
    $scope.totalSuccessItems = 0;
    $scope.totalFailItems = 0;
    $scope.dataModel = new AppDataModel();

    $scope.clearItems = function () {
        $http.delete($scope.dataModel.dumpInterferenceUrl).success(function() {
            $scope.totalDumpItems = 0;
            $scope.totalSuccessItems = 0;
            $scope.totalFailItems = 0;
        });
    };

    $scope.dumpItems = function() {
        sendRequest(actionUrl, "PUT", null, function(result) {
            if (result === true) {
                viewModel.totalSuccessItems(viewModel.totalSuccessItems() + 1);
            } else {
                viewModel.totalFailItems(viewModel.totalFailItems() + 1);
            }
            if (viewModel.totalSuccessItems() + viewModel.totalFailItems() < viewModel.totalDumpItems()) {
                viewModel.dumpItems();
            } else {
                viewModel.updateHistoryItems();
            }
        }, function() {
            viewModel.totalFailItems(viewModel.totalFailItems() + 1);
            if (viewModel.totalSuccessItems() + viewModel.totalFailItems() < viewModel.totalDumpItems()) {
                viewModel.dumpItems();
            } else {
                viewModel.updateHistoryItems();
            }
        });
    };

    $http.get($scope.dataModel.dumpInterferenceUrl).success(function(result) {
        $scope.totalDumpItems = result;
    });
});

function InterferenceImportViewModel(app, dataModel) {
    var self = this;

    self.updateHistoryItems = function () {
        self.clearItems();
    };

    return self;
}