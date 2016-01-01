var Grid, alphabet, count, letter, randomLetter, root, tileCounts, totalTiles;

tileCounts = {
  A: 9,
  B: 2,
  C: 2,
  D: 4,
  E: 12,
  F: 2,
  G: 3,
  H: 2,
  I: 9,
  J: 1,
  K: 1,
  L: 4,
  M: 2,
  N: 6,
  O: 8,
  P: 2,
  Q: 1,
  R: 6,
  S: 4,
  T: 6,
  U: 4,
  V: 2,
  W: 2,
  X: 1,
  Y: 2,
  Z: 1
};

totalTiles = 0;

for (letter in tileCounts) {
  count = tileCounts[letter];
  totalTiles += count;
}

alphabet = ((function() {
  var _results;
  _results = [];
  for (letter in tileCounts) {
    _results.push(letter);
  }
  return _results;
})()).sort();

randomLetter = function() {
  var randomNumber, x, _i, _len;
  randomNumber = Math.ceil(Math.random() * totalTiles);
  x = 1;
  for (_i = 0, _len = alphabet.length; _i < _len; _i++) {
    letter = alphabet[_i];
    x += tileCounts[letter];
    if (x > randomNumber) {
      return letter;
    }
  }
};

Grid = (function() {
  function Grid() {
    var size, x, y;
    this.size = size = 5;
    this.tiles = (function() {
      var _i, _results;
      _results = [];
      for (x = _i = 0; 0 <= size ? _i < size : _i > size; x = 0 <= size ? ++_i : --_i) {
        _results.push((function() {
          var _j, _results1;
          _results1 = [];
          for (y = _j = 0; 0 <= size ? _j < size : _j > size; y = 0 <= size ? ++_j : --_j) {
            _results1.push(randomLetter());
          }
          return _results1;
        })());
      }
      return _results;
    })();
  }

  Grid.prototype.inRange = function(x, y) {
    return (0 <= x && x < this.size) && (0 <= y && y < this.size);
  };

  Grid.prototype.swap = function(_arg) {
    var x1, x2, y1, y2, _ref;
    x1 = _arg.x1, y1 = _arg.y1, x2 = _arg.x2, y2 = _arg.y2;
    return _ref = [this.tiles[x2][y2], this.tiles[x1][y1]], this.tiles[x1][y1] = _ref[0], this.tiles[x2][y2] = _ref[1], _ref;
  };

  Grid.prototype.rows = function() {
    var x, y, _i, _ref, _results;
    _results = [];
    for (x = _i = 0, _ref = this.size; 0 <= _ref ? _i < _ref : _i > _ref; x = 0 <= _ref ? ++_i : --_i) {
      _results.push((function() {
        var _j, _ref1, _results1;
        _results1 = [];
        for (y = _j = 0, _ref1 = this.size; 0 <= _ref1 ? _j < _ref1 : _j > _ref1; y = 0 <= _ref1 ? ++_j : --_j) {
          _results1.push(this.tiles[y][x]);
        }
        return _results1;
      }).call(this));
    }
    return _results;
  };

  return Grid;

})();

root = typeof exports !== "undefined" && exports !== null ? exports : window;

root.Grid = Grid;
