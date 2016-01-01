localStorage.clear();
sessionStorage.clear();

asyncTest("session CSV retrieve", 2, postCreate(function (csvname, csvdata) {
    CSV.begin(csvname).go(
	function (e, D) {
	    ok(!e, "errors: " + e);
	    deepEqual(D.rows, csvdata, "retrieved data matches");
	    start();
	}
    );
}, null, null));



asyncTest("local CSV to HTML table display and retrieve", 3, postCreate(function (csvname, csvdata) {
    CSV.begin(csvname).table("tab1", { header: 1, caption: 'CSV/HTML display-retrieve test data' }).go(
	function (e, D) {
	    ok(!e, "errors: " + e);
	    CSV.begin('#tab1').go(
		function (ee, DD) {
		    ok(!ee, "errors: " + ee);
		    equal(JSON.stringify(D.rows), JSON.stringify(DD.rows), "data matches");
		}
	    );
	}
    );
    setTimeout(
	function () {
	    start();
	    $('#tab1').remove();
	},
	3000);
}, null, null));


asyncTest("HTML table data retrieve -- check that new lines and white space are trimmed", 3, function () {
    var newHTML = "<div id='tab1'><table>\n<tr>\n<th>      A    \n</th><th>  B    \n            \n</th><th>\n\nC</th></tr><tr><td>1</td><td>2\n\n\n\n\n      \n</td><td>3\n\n</td></tr><tr><td>     9            \n</td><td>       \n   8     \n</td><td>\n               7           \n</td>\n\n\n</tr>        \n       </table>       \n</div>";
    $(document.body).append(newHTML);
    setTimeout(function () {
        ok($('#tab1').html().split("\n").length > 10, "jQuery retrieved html contains newlines");
    }, 200);
    setTimeout(function () {
        CSV.begin('#tab1').go(function (e, D) {
            ok(!e, "error: " + e);
            deepEqual(D.rows, [['A', 'B', 'C'], [1, 2, 3], [9, 8, 7]], "retrieved correct data");
        }
                     );
    }, 500);
    setTimeout(function () {
        $('#tab1').remove();
        start();
    }, 1000);
});


asyncTest("appendCol -- even/odd", 2, postCreate(function (csvname, csvdata) {
    CSV.begin(csvname).
	appendCol("parity", [1, 0]).
	go(function (e, D) {
	    var checkdata = JSON.parse(JSON.stringify(csvdata)); // deep copy
	    var i, l;
	    for (i = 1, l = checkdata.length; i < l; ++i) checkdata[i].push(i % 2);
	    checkdata[0].push('parity');
	    ok(!e, "errors: " + e);
	    deepEqual(D.rows, checkdata, "data matches alternate calculation");
	    start();
	});

}));

asyncTest("appendCol -- sum function, !rowprops", 3, postCreate(function (csvname, csvdata) {
    CSV.begin(csvname).
	appendCol("sum", function (index, row) {
	    for (var i = 0, l = row.length, s = 0; i < l; ++i)
	        s += row[i];
	    return s;
	}, false).
	go(function (e, D) {
	    var i, j, l, ll, s = 0;
	    console.log(e);
	    console.log(D);
	    console.log(csvdata);
	    ok(!e, "errors: " + e);
	    ok(D.rows[0][csvdata[0].length] === 'sum', "sets new colname 'sum'");
	    for (i = 1, l = D.rows.length; i < l; ++i) {
	        for (j = 0, ll = D.rows[i].length - 1; j < ll; ++j)
	            s += D.rows[i][j];
	        s -= D.rows[i][ll]
	    }
	    ok(s === 0, "bad test sum, should be zero, got: " + s);
	    start();
	});
}));

asyncTest("appendCol -- detect length mismatch", 2, postCreate(function (csvname, csvdata) {
    CSV.begin(csvname).
	appendCol("parity", [1, 0], "strict").
	go(function (e, D) {
	    ok(e, "should report error: " + e);
	    equal(D, null, "null data expected");
	    start();
	});

}));

asyncTest("editor -- do nothing", 6, postCreate(function (csvname, csvdata) {
    CSV.begin(csvname).
	editor("ed1", true).
	go(function (e, D) {
	    ok(!e, "errors: " + e);
	    ok(!!D, "D not null");
	    ok(!!D.rows, "D.rows not null");
	    ok(D.rows.length, "D.rows.length not zero");
	    equal(D.rows.length, csvdata.length, "D.rows.length == csvdata.length");
	    equal(JSON.stringify(csvdata),
		  JSON.stringify(D.rows),
		  'data unchanged');
	    start();
	});
    setTimeout(
	function () {
	    $('.editorDoneButton').click();
	},
	1000);
}));

asyncTest("editor -- change 2nd b to 8.5", 3, postCreate(function (csvname, csvdata) {
    var old = csvdata[2][1];
    CSV.begin(csvname).
	editor("ed1", true).
	go(function (e, D) {
	    ok(!e, "errors: " + e);
	    equal(D.rows[2][1], 8.5, "new value set correctly");
	    D.rows[2][1] = old;
	    equal(JSON.stringify(csvdata),
		  JSON.stringify(D.rows),
		  'data otherwise unchanged');
	    start();
	});
    setTimeout(
	function () {
	    // when forcing input into the editor, be sure to call change()
	    // which triggers the onchange() handler bound to that editor cell
	    $('#ed1 input[data-row="2"][data-col="1"]').val('8.5').change();
	}, 1000);
    setTimeout(
	function () {
	    $('.editorDoneButton').click();
	}, 1200);

}));

asyncTest("start editor from button using finalize -- then change 2nd b to 8.5", 4, postCreate(function (csvname, csvdata) {
    var old = csvdata[2][1];
    $(document.body).append('<div id="testFinalizeDiv"></div>');
    $('#testFinalizeDiv').html("<button id='startEditorButton'>start</button>");
    var workflow =
	CSV.begin(csvname).
	    editor("ed1", true).
	    finalize(function (e, D) {
	        ok(!e, "errors: " + e);
	        equal(D.rows[2][1], 8.5, "new value set correctly");
	        D.rows[2][1] = old;
	        equal(JSON.stringify(csvdata),
                  JSON.stringify(D.rows),
                  'data otherwise unchanged');
	        start();
	        $('#testFinalizeDiv').remove();
	    });
    $('#startEditorButton').click(workflow);
    setTimeout(
	function () {
	    $('#startEditorButton').click();
	},
	1000);
    // click it twice to be annoying and make sure finalize only fires it once
    // check console log for the warning message
    setTimeout(
	function () {
	    equal(workflow(), null, "handler returns null when work in progress");
	},
	1050);
    setTimeout(
	function () {
	    // when forcing input into the editor, be sure to call change()
	    // which triggers the onchange() handler bound to that editor cell
	    $('#ed1 input[data-row="2"][data-col="1"]').val('8.5').change();
	}, 3000);
    setTimeout(
	function () {
	    $('.editorDoneButton').click();
	}, 3500);

}));

asyncTest("editor -- change 52nd b to 8.5 with b=50,e=60", 3, postCreate(function (csvname, csvdata) {
    var old = csvdata[52][1];
    CSV.begin(csvname).
	editor("ed1", true, 50, 60).
	go(function (e, D) {
	    ok(!e, "errors: " + e);
	    equal(D.rows[52][1], 8.5, "new value set correctly");
	    D.rows[52][1] = old;
	    equal(JSON.stringify(csvdata),
		  JSON.stringify(D.rows),
		  'data otherwise unchanged');
	    start();
	});
    setTimeout(
	function () {
	    // when forcing input into the editor, be sure to call change()
	    // which triggers the onchange() handler bound to that editor cell
	    $('#ed1 input[data-row="52"][data-col="1"]').val('8.5').change();
	}, 1000);
    setTimeout(
	function () {
	    $('.editorDoneButton').click();
	}, 1200);

}));


asyncTest("editor -- change last a to 23, scrollable", 4, postCreate(function (csvname, csvdata) {
    var lastRow = csvdata.length - 1;
    var old = csvdata[lastRow][0];
    CSV.begin(csvname).
	editor("edscrollable", true).
	go(function (e, D) {
	    ok(!e, "errors: " + e);
	    equal(D.rows.length, lastRow + 1, "lengths equal");
	    equal(D.rows[lastRow][0], 23, "new value set correctly");
	    D.rows[lastRow][0] = old;
	    equal(JSON.stringify(csvdata),
		  JSON.stringify(D.rows),
		  'data otherwise unchanged');
	    start();
	});
    setTimeout(
	function () {
	    // when forcing input into the editor, be sure to call change()
	    // which triggers the onchange() handler bound to that editor cell
	    $('#edscrollable input[data-row="' + lastRow + '"][data-col="0"]').val('23').change();
	}, 1000);
    setTimeout(
	function () {
	    $('.editorDoneButton').click();
	}, 1200);

}));

asyncTest("editor -- change last a to 23 -- header off", 3, postCreate(function (csvname, csvdata) {
    var lastRow = csvdata.length - 1;
    var old = csvdata[lastRow][0];
    CSV.begin(csvname).
	editor("ed1", false).
	go(function (e, D) {
	    ok(!e, "errors: " + e);
	    equal(D.rows[lastRow][0], 23, "new value set correctly");
	    D.rows[lastRow][0] = old;
	    equal(JSON.stringify(csvdata),
		  JSON.stringify(D.rows),
		  'data otherwise unchanged');
	    start();
	});
    setTimeout(
	function () {
	    // when forcing input into the editor, be sure to call change()
	    // which triggers the onchange() handler bound to that editor cell
	    $('#ed1 input[data-row="' + lastRow + '"][data-col="0"]').val('23').change();
	}, 1000);
    setTimeout(
	function () {
	    $('.editorDoneButton').click();
	}, 1200);

}));
