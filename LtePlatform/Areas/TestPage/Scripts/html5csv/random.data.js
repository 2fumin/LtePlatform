var randomData = function (n, m) {
    var h = 'abcdefghijklmnopqrstuvwxyz'.split('');
    var data = [];
    var i, j, row;
    data.push(h.slice(0, m));
    for (i = 0; i < n; ++i) {
        row = [];
        for (j = 0; j < m; ++j) {
            row.push(Math.random());
        }
        data.push(row);
    }
    return data;
};

var totallyRandomData = function () {
    var n = 100 + Math.floor(Math.random() * 400);
    var m = 5 + Math.floor(Math.random() * 10);
    return randomData(n, m);
};

function postCreate(func, csvname, csvdata) {
    return function () {
        var csvn = csvname || "session/qtest";
        var csvd = csvdata || totallyRandomData();
        window.sessionStorage.clear();
        CSV.begin(csvd).save(csvn).go(
            function (e, unused) {
                if (e) throw "onCreate:" + e;
                func.apply({}, [csvn, csvd])
            }
        );
    };
}

var postLMlength = 4000;
