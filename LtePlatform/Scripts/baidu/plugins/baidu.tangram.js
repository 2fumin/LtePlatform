var tangram = function (baidu, T) {
    window[baidu.guid] = window[baidu.guid] || {};

    baidu.lang = baidu.lang || {};
    baidu.lang.isString = function (source) {
        return '[object String]' == Object.prototype.toString.call(source);
    };
    baidu.lang.isFunction = function (source) {
        return '[object Function]' == Object.prototype.toString.call(source);
    };
    baidu.lang.Event = function (type, target) {
        this.type = type;
        this.returnValue = true;
        this.target = target || null;
        this.currentTarget = null;
    };


    baidu.object = baidu.object || {};
    baidu.extend =
    baidu.object.extend = function (target, source) {
        for (var p in source) {
            if (source.hasOwnProperty(p)) {
                target[p] = source[p];
            }
        }

        return target;
    };
    baidu.event = baidu.event || {};
    baidu.event._listeners = baidu.event._listeners || [];
    baidu.dom = baidu.dom || {};

    baidu.dom._g = function (id) {
        if (baidu.lang.isString(id)) {
            return document.getElementById(id);
        }
        return id;
    };
    baidu._g = baidu.dom._g;
    baidu.event.on = function (element, type, listener) {
        type = type.replace(/^on/i, '');
        element = baidu.dom._g(element);
        var realListener = function (ev) {
            // 1. 这里不支持EventArgument,  原因是跨frame的事件挂载
            // 2. element是为了修正this
            listener.call(element, ev);
        },
            lis = baidu.event._listeners,
            filter = baidu.event._eventFilter,
            afterFilter,
            realType = type;
        type = type.toLowerCase();
        // filter过滤
        if (filter && filter[type]) {
            afterFilter = filter[type](element, type, realListener);
            realType = afterFilter.type;
            realListener = afterFilter.listener;
        }

        // 事件监听器挂载
        if (element.addEventListener) {
            element.addEventListener(realType, realListener, false);
        } else if (element.attachEvent) {
            element.attachEvent('on' + realType, realListener);
        }
        // 将监听器存储到数组中
        lis[lis.length] = [element, type, listener, realListener, realType];
        return element;
    };

    baidu.on = baidu.event.on;
    baidu.event.un = function (element, type, listener) {
        element = baidu.dom._g(element);
        type = type.replace(/^on/i, '').toLowerCase();

        var lis = baidu.event._listeners,
            len = lis.length,
            isRemoveAll = !listener,
            item,
            realType, realListener;
        while (len--) {
            item = lis[len];

            if (item[1] === type
                && item[0] === element
                && (isRemoveAll || item[2] === listener)) {
                realType = item[4];
                realListener = item[3];
                if (element.removeEventListener) {
                    element.removeEventListener(realType, realListener, false);
                } else if (element.detachEvent) {
                    element.detachEvent('on' + realType, realListener);
                }
                lis.splice(len, 1);
            }
        }

        return element;
    };
    baidu.un = baidu.event.un;
    baidu.dom.g = function (id) {
        if ('string' == typeof id || id instanceof String) {
            return document.getElementById(id);
        } else if (id && id.nodeName && (id.nodeType == 1 || id.nodeType == 9)) {
            return id;
        }
        return null;
    };
    baidu.g = baidu.G = baidu.dom.g;
    baidu.dom._styleFixer = baidu.dom._styleFixer || {};
    baidu.dom._styleFilter = baidu.dom._styleFilter || [];
    baidu.dom._styleFilter.filter = function (key, value, method) {
        for (var i = 0, filters = baidu.dom._styleFilter, filter; filter = filters[i]; i++) {
            if (filter = filter[method]) {
                value = filter(key, value);
            }
        }
        return value;
    };
    baidu.string = baidu.string || {};

    baidu.string.toCamelCase = function (source) {
        //提前判断，提高getStyle等的效率 thanks xianwei
        if (source.indexOf('-') < 0 && source.indexOf('_') < 0) {
            return source;
        }
        return source.replace(/[-_][^-_]/g, function (match) {
            return match.charAt(1).toUpperCase();
        });
    };

    baidu.dom.setStyle = function (element, key, value) {
        var dom = baidu.dom, fixer;

        // 放弃了对firefox 0.9的opacity的支持
        element = dom.g(element);
        key = baidu.string.toCamelCase(key);

        if (fixer = dom._styleFilter) {
            value = fixer.filter(key, value, 'set');
        }

        fixer = dom._styleFixer[key];
        (fixer && fixer.set) ? fixer.set(element, value) : (element.style[fixer || key] = value);

        return element;
    };

    baidu.setStyle = baidu.dom.setStyle;

    baidu.dom.setStyles = function (element, styles) {
        element = baidu.dom.g(element);
        for (var key in styles) {
            baidu.dom.setStyle(element, key, styles[key]);
        }
        return element;
    };
    baidu.setStyles = baidu.dom.setStyles;
    baidu.browser = baidu.browser || {};
    baidu.browser.ie = baidu.ie = /msie (\d+\.\d+)/i.test(navigator.userAgent) ? (document.documentMode || +RegExp['\x241']) : undefined;
    baidu.dom._NAME_ATTRS = (function () {
        var result = {
            'cellpadding': 'cellPadding',
            'cellspacing': 'cellSpacing',
            'colspan': 'colSpan',
            'rowspan': 'rowSpan',
            'valign': 'vAlign',
            'usemap': 'useMap',
            'frameborder': 'frameBorder'
        };

        if (baidu.browser.ie < 8) {
            result['for'] = 'htmlFor';
            result['class'] = 'className';
        } else {
            result['htmlFor'] = 'for';
            result['className'] = 'class';
        }

        return result;
    })();
    baidu.dom.setAttr = function (element, key, value) {
        element = baidu.dom.g(element);
        if ('style' == key) {
            element.style.cssText = value;
        } else {
            key = baidu.dom._NAME_ATTRS[key] || key;
            element.setAttribute(key, value);
        }
        return element;
    };
    baidu.setAttr = baidu.dom.setAttr;
    baidu.dom.setAttrs = function (element, attributes) {
        element = baidu.dom.g(element);
        for (var key in attributes) {
            baidu.dom.setAttr(element, key, attributes[key]);
        }
        return element;
    };
    baidu.setAttrs = baidu.dom.setAttrs;
    baidu.dom.create = function (tagName, opt_attributes) {
        var el = document.createElement(tagName),
            attributes = opt_attributes || {};
        return baidu.dom.setAttrs(el, attributes);
    };
    T.undope = true;
};