angular.module('myApp.url', [])
    .factory('appUrlService', function () {
        var parseQueryString = function(queryString) {
            var data = {}, pair, separatorIndex, escapedKey, escapedValue, key, value;

            if (queryString === null || queryString === undefined) {
                return data;
            }

            var pairs = queryString.split("?")[1].split("&");

            for (var i = 0; i < pairs.length; i++) {
                pair = pairs[i];
                separatorIndex = pair.indexOf("=");

                if (separatorIndex === -1) {
                    escapedKey = pair;
                    escapedValue = null;
                } else {
                    escapedKey = pair.substr(0, separatorIndex);
                    escapedValue = pair.substr(separatorIndex + 1);
                }

                key = decodeURIComponent(escapedKey);
                value = decodeURIComponent(escapedValue);

                data[key] = value;
            }

            return data;
        };
        var getFragment = function() {
            if (window.location.hash.indexOf("#") === 0) {
                return parseQueryString(window.location.hash.substr(1));
            } else {
                return {};
            }
        };
        var initializeAuthorization = function () {
            if (!sessionStorage.getItem("accessToken")) {
                var fragment = getFragment();

                if (fragment.access_token) {
                    // returning with access token, restore old hash, or at least hide token
                    window.location.hash = fragment.state || '';
                    sessionStorage.setItem("accessToken", fragment.access_token);
                } else {
                    // no token - so bounce to Authorize endpoint in AccountController to sign in or register
                    window.location = "/Account/Authorize?client_id=web&response_type=token&state=" + encodeURIComponent(window.location.hash);
                }
            }
        };
        var getAccessToken = function() {
            var token = sessionStorage.getItem("accessToken");
            if (!token) initializeAuthorization();
            return token || sessionStorage.getItem("accessToken");
        };
        return {
            getApiUrl: function(topic) {
                return '/api/' + topic;
            },
            userInfoUrl: "/api/Me",
            siteUrl: "/",
            parseQueryString: parseQueryString,
            getAccessToken: getAccessToken
        };
    });