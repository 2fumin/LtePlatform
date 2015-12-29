function WorkItemViewModel(app, dataModel) {
    var self = this;

    self.states = ko.observableArray(['未完成', '全部']);
    self.currentState = ko.observable('未完成');
    self.types = ko.observableArray(['全部', '2/3G', '4G']);
    self.currentType = ko.observable('全部');
    self.totalPages = ko.observable(0);
    self.currentPage = ko.observable(1);
    self.itemsPerPage = ko.observable(20);
    self.pageSizeSelection = ko.observableArray([10, 15, 20, 30, 50]);
    self.workItemViews = ko.observableArray([]);
    self.canGotoCurrentPage = ko.observable(false);

    self.currentPage.subscribe(function(page) {
        if (page >= 1 && page <= self.totalPages()) {
            self.canGotoCurrentPage(true);
        } else {
            self.canGotoCurrentPage(false);
            self.currentPage(1);
        }
    });

    Sammy(function () {
        this.get('#workItem', function () {
            sendRequest(app.dataModel.workItemUrl, "GET", {
                statCondition: self.currentState(),
                typeCondition: self.currentType(),
                itemsPerPage: self.itemsPerPage()
            }, function(result) {
                self.totalPages(result);
                self.query();
            });
        });
        this.get('/Kpi/TopDrop2GDaily', function () { this.app.runRoute('get', '#workItem'); });
    });

    self.query = function() {
        sendRequest(app.dataModel.workItemUrl, "GET", {
            statCondition: self.currentState(),
            typeCondition: self.currentType(),
            itemsPerPage: self.itemsPerPage(),
            page: self.currentPage()
        }, function(result) {
            self.workItemViews(result);
        });
    };

    self.queryFirstPage = function() {
        self.currentPage(1);
        self.query();
    };

    self.queryPrevPage = function() {
        self.currentPage(self.currentPage() - 1);
        self.query();
    };

    self.queryNextPage = function() {
        self.currentPage(self.currentPage() + 1);
        self.query();
    };

    self.queryLastPage = function() {
        self.currentPage(self.totalPages());
        self.query();
    };

    return self;
}

app.addViewModel({
    name: "WorkItem",
    bindingMemberName: "workItemList",
    factory: WorkItemViewModel
});