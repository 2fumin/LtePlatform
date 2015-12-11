/**
 * @fileoverview 百度地图的自定义信息窗口，对外开放。
 * 用户自定义信息窗口的各种样式。例如：border，margin，padding，color，background等
 * 主入口类是<a href="symbols/BMapLib.InfoBox.html">InfoBox</a>，
 * 基于Baidu Map API 1.2。
 *
 * @author Baidu Map Api Group
 * @version 1.2
 * @Modified by Ouyang Hui, 2015-12-11
 */
/**
 * @namespace BMap的所有library类均放在BMapLib命名空间下
 */

var BMapLib = window.BMapLib = BMapLib || {};

//常量，infoBox可以出现的位置，此版本只可实现上下两个方向。
var INFOBOX_AT_TOP = 1, INFOBOX_AT_RIGHT = 2, INFOBOX_AT_BOTTOM = 3, INFOBOX_AT_LEFT = 4;

(function() {
    //声明baidu包
    var T,
        baidu = T = baidu || { version: '1.5.0' };
    baidu.guid = '$BAIDU$';

    //以下方法为百度Tangram框架中的方法，请到http://tangram.baidu.com 查看文档
    tangram(baidu, T);

    /**
     * @exports InfoBox as BMapLib.InfoBox
     */

    var InfoBox =
    /**
     * InfoBox类的构造函数
     * @class InfoBox <b>入口</b>。
     * 可以自定义border,margin,padding,关闭按钮等等。
     * @constructor
         * @param {Map} map Baidu map的实例对象.
         * @param {String} content infoBox中的内容.
         * @param {Json Object} opts 可选的输入参数，非必填项。可输入选项包括：<br />
         * {<br />"<b>offset</b>" : {Size} infoBox的偏移量
         * <br />"<b>boxClass</b>" : {String} 定义infoBox的class,
         * <br />"<b>boxStyle</b>" : {Json} 定义infoBox的style,此项会覆盖boxClass<br />
         * <br />"<b>closeIconMargin</b>" : {String} 关闭按钮的margin    <br />
         * <br />"<b>closeIconUrl</b>" : {String} 关闭按钮的url地址    <br />
         * <br />"<b>enableAutoPan</b>" : {Boolean} 是否启动自动平移功能    <br />
         * <br />"<b>align</b>" : {Number} 基于哪个位置进行定位，取值为[INFOBOX_AT_TOP,INFOBOX_AT_BOTTOM]<br />
         * }<br />.
         * @example <b>参考示例：</b><br />
         * var infoBox = new BMapLib.InfoBox(map,"百度地图api",{boxStyle:{background:"url('tipbox.gif') no-repeat
          center top",width: "200px"},closeIconMargin: "10px 2px 0 0",enableAutoPan: true
          ,alignBottom: false});
     */
        BMapLib.InfoBox = function (map, content, opts) {

            this._content = content || "";
            this._isOpen = false;
            this._map = map;

            this._opts = opts = opts || {};
            this._opts.offset = opts.offset || new BMap.Size(0, 0);
            this._opts.boxClass = opts.boxClass || "infoBox";
            this._opts.boxStyle = opts.boxStyle || {};
            this._opts.closeIconMargin = opts.closeIconMargin || "2px";
            this._opts.closeIconUrl = opts.closeIconUrl || "close.png";
            this._opts.enableAutoPan = opts.enableAutoPan ? true : false;
            this._opts.align = opts.align || INFOBOX_AT_TOP;
        };

    InfoBox.prototype = new BMap.Overlay();

    InfoBox.prototype.initialize = function (map) {
        var me = this;
        var div = this._div = baidu.dom.create('div', { "class": this._opts.boxClass });
        baidu.dom.setStyles(div, this._opts.boxStyle);

        //设置position为absolute，用于定位
        div.style.position = "absolute";
        this._setContent(this._content);

        var floatPane = map.getPanes().floatPane;
        floatPane.style.width = "auto";
        floatPane.appendChild(div);

        //设置完内容后，获取div的宽度,高度
        this._getInfoBoxSize();

        //this._boxWidth = parseInt(this._div.offsetWidth,10);
        //this._boxHeight = parseInt(this._div.offsetHeight,10);
        //阻止各种冒泡事件
        baidu.event.on(div, "onmousedown", function (e) {
            me._stopBubble(e);
        });

        baidu.event.on(div, "onmouseover", function (e) {
            me._stopBubble(e);
        });

        baidu.event.on(div, "click", function (e) {
            me._stopBubble(e);
        });

        baidu.event.on(div, "dblclick", function (e) {
            me._stopBubble(e);
        });
        return div;
    };

    InfoBox.prototype.draw = function() {
        this._isOpen && this._adjustPosition(this._point);
    }
    /**
     * 打开infoBox
     * @param {Marker|Point} anchor 要在哪个marker或者point上打开infobox
     * @return none
     *
     * @example <b>参考示例：</b><br />
     * infoBox.open();
     */
    InfoBox.prototype.open = function(anchor){
        var me = this,
            poi;

        if (!this._isOpen) {
            this._map.addOverlay(this);
            this._isOpen = true;

            //延迟10ms派发open事件，使后绑定的事件可以触发。
            setTimeout(function () {
                me._dispatchEvent(me, "open", { "point": me._point });
            }, 10);
        }

        //这里仅支持BMap.Point和BMap.Marker
        if (anchor instanceof BMap.Point) {
            poi = anchor;

            //清除之前存在的marker事件绑定，如果存在的话
            this._removeMarkerEvt();

        } else if (anchor instanceof BMap.Marker) {
            //如果当前marker不为空，说明是第二个marker，或者第二次点open按钮,先移除掉之前绑定的事件
            if (this._marker) {
                this._removeMarkerEvt();
            }
            poi = anchor.getPosition();
            this._marker = anchor;
            !this._markerDragend && this._marker.addEventListener("dragend", this._markerDragend = function (e) {
                me._point = e.point;
                me._adjustPosition(me._point);
                me._panBox();
                me.show();
            });

            //给marker绑定dragging事件，拖动marker的时候，infoBox也跟随移动
            !this._markerDragging && this._marker.addEventListener("dragging", this._markerDragging = function () {
                me.hide();
                me._point = me._marker.getPosition();
                me._adjustPosition(me._point);
            });
        }

        //打开的时候，将infowindow显示
        this.show();
        this._point = poi;
        this._panBox();
        this._adjustPosition(this._point);
    }
    /**
     * 关闭infoBox
     * @return none
     *
     * @example <b>参考示例：</b><br />
     * infoBox.close();
     */
    InfoBox.prototype.close = function(){
        if(this._isOpen){
            this._map.removeOverlay(this);
            this._remove();
            this._isOpen = false;
            this._dispatchEvent(this,"close",{"point" : this._point});
        }
    }

	/**
   	 * 打开infoBox时，派发事件的接口
     * @name InfoBox#Open
     * @event
     * @param {Event Object} e 回调函数会返回event参数，包括以下返回值：
     * <br />{"<b>target</b> : {BMap.Overlay} 触发事件的元素,
     * <br />"<b>type</b>：{String} 事件类型,
     * <br />"<b>point</b>：{Point} infoBox的打开位置}
     *
     * @example <b>参考示例：</b>
     * infoBox.addEventListener("open", function(e) {
     *     alert(e.type);
     * });
     */
   /**
   	 * 关闭infoBox时，派发事件的接口
     * @name InfoBox#Close
     * @event
     * @param {Event Object} e 回调函数会返回event参数，包括以下返回值：
     * <br />{"<b>target</b> : {BMap.Overlay} 触发事件的元素,
     * <br />"<b>type</b>：{String} 事件类型,
     * <br />"<b>point</b>：{Point} infoBox的关闭位置}
     *
     * @example <b>参考示例：</b>
     * infoBox.addEventListener("close", function(e) {
     *     alert(e.type);
     * });
     */
  /**
     * 启用自动平移
     * @return none
     *
     * @example <b>参考示例：</b><br />
     * infoBox.enableAutoPan();
     */
    InfoBox.prototype.enableAutoPan = function(){
        this._opts.enableAutoPan = true;
    }
    /**
     * 禁用自动平移
     * @return none
     *
     * @example <b>参考示例：</b><br />
     * infoBox.disableAutoPan();
     */
    InfoBox.prototype.disableAutoPan = function(){
        this._opts.enableAutoPan = false;
    }
    /**
     * 设置infoBox的内容
     * @param {String|HTMLElement} content 弹出气泡中的内容
     * @return none
     *
     * @example <b>参考示例：</b><br />
     * infoBox.setContent("百度地图API");
     */
    InfoBox.prototype.setContent = function(content){
      	this._setContent(content);
      	this._getInfoBoxSize();
        this._adjustPosition(this._point);
    }
    /**
     * 设置信息窗的地理位置
     * @param {Point} point 设置position
     * @return none
     *
     * @example <b>参考示例：</b><br />
     * infoBox.setPosition(new BMap.Point(116.35,39.911));
     */
    InfoBox.prototype.setPosition = function(poi){
        this._point = poi;
        this._adjustPosition(poi);
        this._removeMarkerEvt();
    }
    /**
     * 获得信息窗的地理位置
     * @param none
     * @return {Point} 信息窗的地理坐标
     *
     * @example <b>参考示例：</b><br />
     * infoBox.getPosition();
     */
    InfoBox.prototype.getPosition = function(){
        return this._point;
    }
    /**
     * 返回信息窗口的箭头距离信息窗口在地图
     * 上所锚定的地理坐标点的像素偏移量。
     * @return {Size} Size
     *
     * @example <b>参考示例：</b><br />
     * infoBox.getOffset();
     */
    InfoBox.prototype.getOffset = function(){
        return this._opts.offset;
    },
    /**
   	 *@ignore
     * 删除overlay，调用Map.removeOverlay时将调用此方法，
     * 将移除覆盖物的容器元素
     */
    InfoBox.prototype._remove = function(){
        var me = this;
        if(this.domElement && this.domElement.parentNode){
          //防止内存泄露
          baidu.event.un(this._div.firstChild, "click", me._closeHandler());
          this.domElement.parentNode.removeChild(this.domElement);
        }
        this.domElement = null;
        this._isOpen = false;
        this.dispatchEvent("onremove");
    },
    baidu.object.extend(InfoBox.prototype,{
        /**
         * 获取关闭按钮的html
         * @return IMG 关闭按钮的HTML代码
         */
        _getCloseIcon: function(){
            var img = "<img src='"+ this._opts.closeIconUrl +"' align='right' style='position:absolute;right:0px;cursor:pointer;margin:"+ this._opts.closeIconMargin +"'/>";
            return img;
        },
        /**
	     * 设置infoBox的内容
	     * @param {String|HTMLElement} content 弹出气泡中的内容
	     * @return none
	     *
	     * @example <b>参考示例：</b><br />
	     * infoBox.setContent("百度地图API");
	     */
        _setContent: function(content){
	        if(!this._div){
	            return;
	        }
	        var closeHtml = this._getCloseIcon();
	        //string类型的content
	        if(typeof content.nodeType === "undefined"){
	            this._div.innerHTML = closeHtml + content;
	        }else{
	            this._div.innerHTML = closeHtml;
	            this._div.appendChild(content);
	        }
	        this._content = content;
	        //添加click关闭infobox事件
	        this._addEventToClose();

   	    },
        /**
         * 调整infobox的position
         * @return none
         */
        _adjustPosition: function(poi){
            var pixel = this._getPointPosition(poi);
            var icon = this._marker && this._marker.getIcon();
            switch(this._opts.align){
                case INFOBOX_AT_TOP:
                    if(this._marker){
                        this._div.style.bottom = -(pixel.y - this._opts.offset.height - icon.anchor.height + icon.infoWindowAnchor.height) - this._marker.getOffset().height + 2 + "px";
                    }else{
                        this._div.style.bottom = -(pixel.y - this._opts.offset.height) + "px";
                    }
                    break;
                case INFOBOX_AT_BOTTOM:
                    if(this._marker){
          		        this._div.style.top = pixel.y + this._opts.offset.height - icon.anchor.height + icon.infoWindowAnchor.height + this._marker.getOffset().height + "px";
                    }else{
                        this._div.style.top = pixel.y + this._opts.offset.height + "px";
                    }
                    break;
            }

            if(this._marker){
                this._div.style.left = pixel.x - icon.anchor.width + this._marker.getOffset().width + icon.infoWindowAnchor.width - this._boxWidth / 2 + "px";
            }else{
                this._div.style.left = pixel.x - this._boxWidth / 2 + "px";
            }
        },
        /**
         * 得到infobox的position
         * @return Point  infobox当前的position
         */
        _getPointPosition: function(poi){
            this._pointPosition = this._map.pointToOverlayPixel(poi);
            return this._pointPosition;
        },
        /**
         * 得到infobox的高度跟宽度
         * @return none
         */
        _getInfoBoxSize: function(){
        	this._boxWidth = parseInt(this._div.offsetWidth,10);
        	this._boxHeight = parseInt(this._div.offsetHeight,10);
        },
        /**
         * 添加关闭事件
         * @return none
         */
        _addEventToClose: function(){
            var me = this;
            baidu.event.on(this._div.firstChild, "click", me._closeHandler());
            this._hasBindEventClose = true;
        },
        /**
         * 处理关闭事件
         * @return none
         */
        _closeHandler: function(){
            var me = this;
            return function(e){
                me.close();
            }
        },
        /**
         * 阻止事件冒泡
         * @return none
         */
        _stopBubble: function(e){
            if(e && e.stopPropagation){
                e.stopPropagation();
            }else{
                window.event.cancelBubble = true;
            }
        },
        /**
         * 自动平移infobox，使其在视野中全部显示
         * @return none
         */
        _panBox: function(){
            if(!this._opts.enableAutoPan){
                return;
            }
            var mapH = parseInt(this._map.getContainer().offsetHeight,10),
                mapW = parseInt(this._map.getContainer().offsetWidth,10),
                boxH = this._boxHeight,
                boxW = this._boxWidth;
            //infobox窗口本身的宽度或者高度超过map container
            if(boxH >= mapH || boxW >= mapW){
                return;
            }
            //如果point不在可视区域内
            if(!this._map.getBounds().containsPoint(this._point)){
                this._map.setCenter(this._point);
            }
            var anchorPos = this._map.pointToPixel(this._point),
                panTop,panBottom,panY,
                //左侧超出
                panLeft = boxW / 2 - anchorPos.x,
                //右侧超出
                panRight = boxW / 2 + anchorPos.x - mapW;
            if(this._marker){
                var icon = this._marker.getIcon();
            }
            //基于bottom定位，也就是infoBox在上方的情况
            switch(this._opts.align){
                case INFOBOX_AT_TOP:
                    //上侧超出
                    var h = this._marker ? icon.anchor.height + this._marker.getOffset().height - icon.infoWindowAnchor.height : 0;
                    panTop = boxH - anchorPos.y + this._opts.offset.height + h + 2 ;
                    break;
                case INFOBOX_AT_BOTTOM:
                    //下侧超出
                    var h = this._marker ? -icon.anchor.height + icon.infoWindowAnchor.height + this._marker.getOffset().height + this._opts.offset.height : 0;
                    panBottom = boxH + anchorPos.y - mapH + h + 4;
                    break;
            }

            panX = panLeft > 0 ? panLeft : (panRight > 0 ? -panRight : 0);
            panY = panTop > 0 ? panTop : (panBottom > 0 ? -panBottom : 0);
            this._map.panBy(panX,panY);
        },
        _removeMarkerEvt: function(){
			this._markerDragend && this._marker.removeEventListener("dragend", this._markerDragend);
            this._markerDragging && this._marker.removeEventListener("dragging", this._markerDragging);
            this._markerDragend = this._markerDragging = null;
        },
      	/**
	     * 集中派发事件函数
	     *
	     * @private
	     * @param {Object} instance 派发事件的实例
	     * @param {String} type 派发的事件名
	     * @param {Json} opts 派发事件里添加的参数，可选
	     */
	     _dispatchEvent: function(instance, type, opts) {
	        type.indexOf("on") != 0 && (type = "on" + type);
	        var event = new baidu.lang.Event(type);
	        if (!!opts) {
	            for (var p in opts) {
	                event[p] = opts[p];
	            }
	        }
	        instance.dispatchEvent(event);
	    }
    });
})();

