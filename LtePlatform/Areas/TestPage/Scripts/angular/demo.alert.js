function AlertDemoCtrl($scope) {
  $scope.alerts = [
    { type: 'warning', msg: 'Oh snap! Change a few things up and try submitting again.' }, 
    { type: 'success', msg: 'Well done! You successfully read this important alert message.' }
  ];

  $scope.addAlert = function() {
    $scope.alerts.push({msg: "Another alert!", type: 'info'});
  };

  $scope.closeAlert = function(index) {
    $scope.alerts.splice(index, 1);
  };

}

app.controller("AlertDemoCtrl", AlertDemoCtrl);