// ����һ���ؼ���,��function
function TimeControl() {
    // Ĭ��ͣ��λ�ú�ƫ����
    this.defaultAnchor = BMAP_ANCHOR_TOP_RIGHT;
    this.defaultOffset = new BMap.Size(10, 10);
}

// ͨ��JavaScript��prototype���Լ̳���BMap.Control
TimeControl.prototype = new BMap.Control();

// �Զ���ؼ�����ʵ���Լ���initialize����,���ҽ��ؼ���DOMԪ�ط���
TimeControl.prototype.initialize = function (map) {
    // ����һ��DOMԪ��
    var div = $("#time-control");
    // ���DOMԪ�ص���ͼ��
    map.getContainer().appendChild(div);
    // ��DOMԪ�ط���
    return div;
}