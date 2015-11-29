$(function () {
    // 激活 Knockout
    ko.validation.init({ grouping: { observable: false } });
    ko.applyBindings(app);
});
