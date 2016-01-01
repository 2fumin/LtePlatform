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

var postLM = function (coeff, func) {
    var M = randomData(postLMlength, 4);
    var i, l;
    M[0][4] = 'z';
    for (i = 1, l = M.length; i < l; ++i) {
        M[i][4] = coeff[0] * M[i][0] + coeff[1] * M[i][1] + coeff[2] * M[i][2] + coeff[3] * M[i][3]
            + ((coeff[4]) ? coeff[4] : 0) + ((coeff[5]) ? coeff[5] * (Math.random() - 0.5) : 0.0);
    }
    return postCreate(func, null, M);
};