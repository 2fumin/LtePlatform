app.controller("angular.menu", function ($scope) {
    var rootPath = '/TestPage/AngularTest/';
    $scope.menuItems = [
    {
        name: "Simple",
        displayName: "Simple Type Test",
        url: rootPath + 'Simple'
    }, {
        name: "SubmitForm",
        displayName: "Submit Form",
        url: rootPath + "SubmitForm"
    }, {
        name: "RootProperty",
        displayName: "Root Property",
        url: rootPath + "RootProperty"
    }, {
        name: "Chapter9Ari",
        displayName: "第 9 章　内置指令",
        url: rootPath + "Chapter9Ari"
    }, {
        name: "Chapter10Ari",
        displayName: "第 10 章　指令详解",
        url: rootPath + "Chapter10Ari"
    }];
});