// Write your JavaScript code.
class ContentPreviewItem {
    constructor(data) {
        this.contentSourceUri = ko.observable(); // クラスメンバは、変更通知プロパティとして定義する。
        data && Object.assign(this, data); // 内部で使う以外のデータは、全てマッピングする。
    }
}