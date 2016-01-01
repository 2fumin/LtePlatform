localStorage.clear();
sessionStorage.clear();

QUnit.module("Basie");

test("CSV exists", 1, function () {
    ok(!!CSV, "CSV exists");
});

test("CSV has begin, extend methods",2,function(){
    ok( (typeof CSV.begin ==="function"), "CSV.begin");
    ok( (typeof CSV.extend ==="function"), "CSV.extend");
});

test("CSV.begin() requires valid parameter(s)", 1, function () {
    throws(function () { CSV.begin() }, "no parameters");
});

test("CSV.extend() requires valid parameter(s)", 5, function () {
    throws(function () { CSV.extend() }, "no parameters");
    throws(function () { CSV.extend(function () { }, null) }, "CSV.extend(func,null)");
    throws(function () { CSV.extend(null, function () { }) }, "CSV.extend(null,func)");
    throws(function () { CSV.extend("hello") }, "CSV.extend(string)");
    throws(function () { CSV.extend(34) }, "CSV.extend(number)");
});

QUnit.module("Random data");

asyncTest("session CSV create", 7, function () {
    var csvName = "session/qtest1";
    var csvdata = totallyRandomData();
    window.sessionStorage.clear();
    CSV.begin(csvdata).save(csvName).go(
	    function (e, D) {
	        ok(!e, "errors: " + e);
	        ok(!!D, "data non-null");
	        ok(!!csvdata, "csvdata input non-null");
	        ok(!!D.rows, "data.rows non-null");
	        equal(D.rows.length, csvdata.length, "same # of rows");
	        deepEqual(D.rows, csvdata, "go -- data.rows matches input");
	    }
    );
    setTimeout(
	function () {
	    ok(!!window.sessionStorage[csvName], "session storage entry exists");
	    start();
	},
	2000);
});

asyncTest("%U creates uniformRandomMatrix", 13, function () {
    CSV.begin('%U', {}).go(function (e, D) {
        ok(e, "should give error: " + e);
        equal(D, null, "D should be null");
    });
    CSV.begin('%U', { dim: [1, 100000] }).go(function (e, D) {
        var sum = 0.0, sumsq = 0.0;
        ok(!e, "error: " + e);
        ok(!!D, "D exists");
        ok(!!D.rows, "D.rows exists");
        equal(D.rows.length, 1, "1 row");
        equal(D.rows[0].length, 100000, "100,000 cols");
        ok(Math.min.apply(Math, D.rows[0]) > 0.0, "smallest of 100,000 > 0.0");
        ok(Math.min.apply(Math, D.rows[0]) < 0.001, "smallest of 100,000 < 0.01");
        ok(Math.max.apply(Math, D.rows[0]) < 1.0, "largest of 100,000 < 1.0");
        ok(Math.max.apply(Math, D.rows[0]) > 0.999, "largest of 100,000 > 0.99");
        for (var i = 0; i < 100000; ++i) {
            sum += D.rows[0][i];
            sumsq += Math.pow(D.rows[0][i], 2);
        }
        ok(Math.abs(sum - 50000) < 1000, "sum of 100,000 is 50,000 +/- 1,000");
        ok(Math.abs(sumsq - 33333) < 1000, "sumsq of 100,000 is 33,333 +/- 1,000");
        start();
    });
});

asyncTest("%N creates normalRandomMatrix", 13, function () {
    CSV.begin('%N', {}).go(function (e, D) {
        ok(e, "should give error: " + e);
        equal(D, null, "D should be null");
    });
    CSV.begin('%N', { dim: [1, 100000] }).go(function (e, D) {
        var sum = 0.0, sumsq = 0.0;
        ok(!e, "error: " + e);
        ok(!!D, "D exists");
        ok(!!D.rows, "D.rows exists");
        equal(D.rows.length, 1, "1 row");
        equal(D.rows[0].length, 100000, "100,000 cols");
        ok(Math.min.apply(Math, D.rows[0]) > -6.0, "smallest of 100,000 > -6.0");
        ok(Math.min.apply(Math, D.rows[0]) < -3.5, "smallest of 100,000 < -3.5");
        ok(Math.max.apply(Math, D.rows[0]) > 3, 5, "largest of 100,000 > 3.5");
        ok(Math.max.apply(Math, D.rows[0]) < 6.0, "largest of 100,000 < 6.0");
        for (var i = 0; i < 100000; ++i) {
            sum += D.rows[0][i];
            sumsq += Math.pow(D.rows[0][i], 2);
        }
        ok(Math.abs(sum) < 1000, "sum of 100,000 is 0 +/- 1000");
        ok(Math.abs(sumsq - 100000) < 5000, "sumsq of 100,000 is 100,000 +/- 5,000");
        start();
    });
});

asyncTest("%I creates identityMatrix", 6, function () {
    CSV.begin('%I', {}).go(function (e, D) {
        ok(e, "should give error: " + e);
        equal(D, null, "D should be null");
    });
    CSV.begin('%I', { dim: [3, 3] }).go(
	function (e, D) {
	    ok(!e, "error: " + e);
	    ok(!!D, "D exists");
	    ok(!!D.rows, "D.rows exists");
	    deepEqual(D.rows, [[1, 0, 0], [0, 1, 0], [0, 0, 1]], "correct matrix");
	    start();
	}
    );
});

asyncTest("%D creates diagonalMatrix", 6, function () {
    CSV.begin('%D', {}).go(function (e, D) {
        ok(e, "should give error: " + e);
        equal(D, null, "D should be null");
    });
    CSV.begin('%D', { diag: [-7, 3, 0.5] }).go(
	function (e, D) {
	    ok(!e, "error: " + e);
	    ok(!!D, "D exists");
	    ok(!!D.rows, "D.rows exists");
	    deepEqual(D.rows, [[-7, 0, 0], [0, 3, 0], [0, 0, 0.5]], "correct matrix");
	    start();
	}
    );
});

asyncTest("%F creates matrix from func(i,j)", 4, function () {
    CSV.begin('%F', {}).go(function (e, D) {
        ok(e, "should give error: " + e);
        equal(D, null, "D should be null");
    });
    CSV.begin('%F', { dim: [3, 4], func: function (i, j) { return i * j } }).
	go(
	    function (e, D) {
	        ok(!e, "error: " + e);
	        deepEqual(D.rows,
                  [
                      [0, 0, 0, 0],
                      [0, 1, 2, 3],
                      [0, 2, 4, 6]
                  ],
                  "correct matrix"
                 );
	        start();
	    }
	);
});

asyncTest("local CSV create", 3, function () {
    var csvname = "local/qtest1";
    var csvdata = totallyRandomData();
    window.localStorage.clear();
    CSV.begin(csvdata).save(csvname).go(
	function (e, D) {
	    ok(!e, "errors: " + e);
	    deepEqual(D.rows, csvdata, "go -- this.data.rows matches input");
	}
    );
    setTimeout(
	function () {
	    ok(!!window.localStorage[csvname], "local storage entry exists");
	    window.localStorage.clear();
	    start();
	},
	2000);
});
