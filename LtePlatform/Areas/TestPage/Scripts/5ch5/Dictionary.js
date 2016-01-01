var Dictionary, root,
  __indexOf = [].indexOf || function(item) { for (var i = 0, l = this.length; i < l; i++) { if (i in this && this[i] === item) return i; } return -1; };

Dictionary = (function() {
  function Dictionary(originalWordList, grid) {
    this.originalWordList = originalWordList;
    if (grid != null) {
      this.setGrid(grid);
    }
  }

  Dictionary.prototype.setGrid = function(grid) {
    var w, word, x, y, _i, _ref, _results;
    this.grid = grid;
    this.wordList = this.originalWordList.slice(0);
    this.wordList = (function() {
      var _i, _len, _ref, _results;
      _ref = this.wordList;
      _results = [];
      for (_i = 0, _len = _ref.length; _i < _len; _i++) {
        word = _ref[_i];
        if (word.length <= this.grid.size) {
          _results.push(word);
        }
      }
      return _results;
    }).call(this);
    this.minWordLength = this.grid.size;
    this.minWordLength = Math.min.apply(Math, (function() {
      var _i, _len, _ref, _results;
      _ref = this.wordList;
      _results = [];
      for (_i = 0, _len = _ref.length; _i < _len; _i++) {
        w = _ref[_i];
        _results.push(w.length);
      }
      return _results;
    }).call(this));
    this.usedWords = [];
    _results = [];
    for (x = _i = 0, _ref = this.grid.size; 0 <= _ref ? _i < _ref : _i > _ref; x = 0 <= _ref ? ++_i : --_i) {
      _results.push((function() {
        var _j, _ref1, _results1;
        _results1 = [];
        for (y = _j = 0, _ref1 = this.grid.size; 0 <= _ref1 ? _j < _ref1 : _j > _ref1; y = 0 <= _ref1 ? ++_j : --_j) {
          _results1.push((function() {
            var _k, _len, _ref2, _results2;
            _ref2 = this.wordsThroughTile(x, y);
            _results2 = [];
            for (_k = 0, _len = _ref2.length; _k < _len; _k++) {
              word = _ref2[_k];
              _results2.push(this.markUsed(word));
            }
            return _results2;
          }).call(this));
        }
        return _results1;
      }).call(this));
    }
    return _results;
  };

  Dictionary.prototype.markUsed = function(str) {
    if (__indexOf.call(this.usedWords, str) >= 0) {
      return false;
    } else {
      this.usedWords.push(str);
      return true;
    }
  };

  Dictionary.prototype.isWord = function(str) {
    return __indexOf.call(this.wordList, str) >= 0;
  };

  Dictionary.prototype.isNewWord = function(str) {
    return __indexOf.call(this.wordList, str) >= 0 && __indexOf.call(this.usedWords, str) < 0;
  };

  Dictionary.prototype.wordsThroughTile = function(x, y) {
    var addTiles, grid, length, offset, range, str, strings, _i, _j, _k, _len, _ref, _ref1, _results;
    grid = this.grid;
    strings = [];
    for (length = _i = _ref = this.minWordLength, _ref1 = grid.size; _ref <= _ref1 ? _i <= _ref1 : _i >= _ref1; length = _ref <= _ref1 ? ++_i : --_i) {
      range = length - 1;
      addTiles = function(func) {
        var i;
        return strings.push(((function() {
          var _j, _results;
          _results = [];
          for (i = _j = 0; 0 <= range ? _j <= range : _j >= range; i = 0 <= range ? ++_j : --_j) {
            _results.push(func(i));
          }
          return _results;
        })()).join(''));
      };
      for (offset = _j = 0; 0 <= length ? _j < length : _j > length; offset = 0 <= length ? ++_j : --_j) {
        if (grid.inRange(x - offset, y) && grid.inRange(x - offset + range, y)) {
          addTiles(function(i) {
            return grid.tiles[x - offset + i][y];
          });
        }
        if (grid.inRange(x, y - offset) && grid.inRange(x, y - offset + range)) {
          addTiles(function(i) {
            return grid.tiles[x][y - offset + i];
          });
        }
        if (grid.inRange(x - offset, y - offset) && grid.inRange(x - offset + range, y - offset + range)) {
          addTiles(function(i) {
            return grid.tiles[x - offset + i][y - offset + i];
          });
        }
        if (grid.inRange(x - offset, y + offset) && grid.inRange(x - offset + range, y + offset - range)) {
          addTiles(function(i) {
            return grid.tiles[x - offset + i][y + offset - i];
          });
        }
      }
    }
    _results = [];
    for (_k = 0, _len = strings.length; _k < _len; _k++) {
      str = strings[_k];
      if (this.isWord(str)) {
        _results.push(str);
      }
    }
    return _results;
  };

  return Dictionary;

})();

root = typeof exports !== "undefined" && exports !== null ? exports : window;

root.Dictionary = Dictionary;
