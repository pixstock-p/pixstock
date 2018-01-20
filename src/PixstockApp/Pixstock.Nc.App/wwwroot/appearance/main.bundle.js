webpackJsonp(["main"],{

/***/ "../../../../../src/$$_lazy_route_resource lazy recursive":
/***/ (function(module, exports) {

function webpackEmptyAsyncContext(req) {
	// Here Promise.resolve().then() is used instead of new Promise() to prevent
	// uncatched exception popping up in devtools
	return Promise.resolve().then(function() {
		throw new Error("Cannot find module '" + req + "'.");
	});
}
webpackEmptyAsyncContext.keys = function() { return []; };
webpackEmptyAsyncContext.resolve = webpackEmptyAsyncContext;
module.exports = webpackEmptyAsyncContext;
webpackEmptyAsyncContext.id = "../../../../../src/$$_lazy_route_resource lazy recursive";

/***/ }),

/***/ "../../../../../src/app/app.component.html":
/***/ (function(module, exports) {

module.exports = "<div class=\"flexbox-parent\">\n  <div class=\"flexbox-item header\">\n    Header_1\n  </div>\n\n  <div class=\"d-flex justify-content-between flexbox-item fill-area content flexbox-item-grow\">\n    <div class=\"flexbox-item fill-area content flexbox-item-grow\" style=\"flex-grow: 0.3;\">\n      <div class=\"fill-area-content flexbox-item-grow\" style=\"overflow: hidden\">\n        <app-category-explorer></app-category-explorer>\n      </div>\n    </div>\n    <div class=\"w-50 flexbox-item fill-area content flexbox-item-grow\" style=\"overflow: hidden\">\n      <div class=\"fill-area-content flexbox-item-grow\" style=\"overflow-y:auto\">\n        <div class=\"alert alert-primary\" role=\"alert\">\n          This is a primary alert—check it out! {{_screen.VisibilityContentList}}\n        </div>\n        <app-content-list *ngIf=\"_screen.VisibilityContentList\"></app-content-list>\n      </div>\n    </div>\n    <div class=\"w-25 flexbox-item fill-area content flexbox-item-grow\" style=\"flex-grow: 0.3\">\n      <div class=\"fill-area-content flexbox-item-grow\" style=\"overflow: hidden\">\n        \n      </div>\n    </div>\n  </div>\n\n  <div class=\"flexbox-item footer\">\n    Footer\n  </div>\n</div>"

/***/ }),

/***/ "../../../../../src/app/app.component.scss":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../src/app/app.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return AppComponent; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/esm2015/core.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_angular2_logger_core__ = __webpack_require__("../../../../angular2-logger/core.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_angular2_logger_core___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_1_angular2_logger_core__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__shared_service_pixstock_net_service__ = __webpack_require__("../../../../../src/app/shared/service/pixstock-net.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__shared_service_screen_service__ = __webpack_require__("../../../../../src/app/shared/service/screen.service.ts");
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};




let AppComponent = class AppComponent {
    constructor(_logger, _pixstock, _screen) {
        this._logger = _logger;
        this._pixstock = _pixstock;
        this._screen = _screen;
        this.title = 'app';
        _pixstock.echo.subscribe(prop => this.onEcho(prop));
        _pixstock.ShowContentPreview.subscribe(prop => this.OnShowContentPreview(prop));
        _pixstock.ShowContentList.subscribe(prop => this.OnShowContentList(prop));
    }
    onEcho(todo) {
        console.info("イベントから取得したメッセージ=" + todo);
    }
    OnShowContentPreview(args) {
        this._logger.info("[Stella][AppComponent][OnShowContentPreview] : イベントから取得したメッセージ=" + args);
    }
    OnShowContentList(args) {
        this._logger.info("[Stella][AppComponent][OnShowContentList] : イベントから取得したメッセージ=" + args);
        this._screen.VisibilityContentList = true;
    }
};
AppComponent = __decorate([
    Object(__WEBPACK_IMPORTED_MODULE_0__angular_core__["m" /* Component */])({
        selector: 'app-root',
        template: __webpack_require__("../../../../../src/app/app.component.html"),
        styles: [__webpack_require__("../../../../../src/app/app.component.scss")]
    }),
    __metadata("design:paramtypes", [__WEBPACK_IMPORTED_MODULE_1_angular2_logger_core__["Logger"],
        __WEBPACK_IMPORTED_MODULE_2__shared_service_pixstock_net_service__["a" /* PixstockNetService */],
        __WEBPACK_IMPORTED_MODULE_3__shared_service_screen_service__["a" /* ScreenService */]])
], AppComponent);



/***/ }),

/***/ "../../../../../src/app/app.module.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return AppModule; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_platform_browser__ = __webpack_require__("../../../platform-browser/esm2015/platform-browser.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_core__ = __webpack_require__("../../../core/esm2015/core.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2_angular2_logger_core__ = __webpack_require__("../../../../angular2-logger/core.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2_angular2_logger_core___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_2_angular2_logger_core__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3_angular_tree_component__ = __webpack_require__("../../../../angular-tree-component/dist/angular-tree-component.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4_ngx_bootstrap__ = __webpack_require__("../../../../ngx-bootstrap/bundles/ngx-bootstrap.es2015.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5_ngx_bootstrap_carousel__ = __webpack_require__("../../../../ngx-bootstrap/carousel/index.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6__shared_shared_module__ = __webpack_require__("../../../../../src/app/shared/shared.module.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_7__shared_service_pixstock_net_service__ = __webpack_require__("../../../../../src/app/shared/service/pixstock-net.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_8__app_component__ = __webpack_require__("../../../../../src/app/app.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_9__category_explorer_category_explorer_component__ = __webpack_require__("../../../../../src/app/category-explorer/category-explorer.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_10__content_list_content_list_component__ = __webpack_require__("../../../../../src/app/content-list/content-list.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_11__content_preview_content_preview_component__ = __webpack_require__("../../../../../src/app/content-preview/content-preview.component.ts");
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};




// ngx-bootstrap









let AppModule = class AppModule {
    constructor(_logger, _ngZone, _pixstock) {
        this._logger = _logger;
        this._ngZone = _ngZone;
        this._pixstock = _pixstock;
        _logger.info("アプリケーションの初期化 v0.0.1#4");
        window['angularComponentRef'] = {
            component: this,
            zone: _ngZone
        };
        var parent = window.parent; // JSのWindowオブジェクト
        _logger.info("ParentLocation = " + parent.location);
        let flag = parent.getFirstLoad();
        if (flag == false) {
            _logger.info("AApp初期読み込み判定");
            parent.setFirstLoad();
            _pixstock.initialize(parent.getIpc(), true, this._logger); // IPCオブジェクト取得
        }
        else {
            _logger.info("AApp初期化済み判定");
            _pixstock.initialize(parent.getIpc(), false, this._logger); // IPCオブジェクト取得
        }
    }
};
AppModule = __decorate([
    Object(__WEBPACK_IMPORTED_MODULE_1__angular_core__["H" /* NgModule */])({
        declarations: [
            __WEBPACK_IMPORTED_MODULE_8__app_component__["a" /* AppComponent */],
            __WEBPACK_IMPORTED_MODULE_9__category_explorer_category_explorer_component__["a" /* CategoryExplorerComponent */],
            __WEBPACK_IMPORTED_MODULE_10__content_list_content_list_component__["a" /* ContentListComponent */],
            __WEBPACK_IMPORTED_MODULE_11__content_preview_content_preview_component__["a" /* ContentPreviewComponent */]
        ],
        imports: [
            __WEBPACK_IMPORTED_MODULE_0__angular_platform_browser__["a" /* BrowserModule */],
            __WEBPACK_IMPORTED_MODULE_3_angular_tree_component__["a" /* TreeModule */],
            __WEBPACK_IMPORTED_MODULE_6__shared_shared_module__["a" /* SharedModule */],
            __WEBPACK_IMPORTED_MODULE_4_ngx_bootstrap__["a" /* AlertModule */].forRoot(),
            __WEBPACK_IMPORTED_MODULE_4_ngx_bootstrap__["b" /* ButtonsModule */].forRoot(),
            __WEBPACK_IMPORTED_MODULE_5_ngx_bootstrap_carousel__["a" /* CarouselModule */].forRoot(),
        ],
        providers: [
            { provide: __WEBPACK_IMPORTED_MODULE_2_angular2_logger_core__["Options"], useValue: { level: __WEBPACK_IMPORTED_MODULE_2_angular2_logger_core__["Level"].DEBUG } },
            __WEBPACK_IMPORTED_MODULE_2_angular2_logger_core__["Logger"],
        ],
        bootstrap: [__WEBPACK_IMPORTED_MODULE_8__app_component__["a" /* AppComponent */]]
    }),
    __metadata("design:paramtypes", [__WEBPACK_IMPORTED_MODULE_2_angular2_logger_core__["Logger"],
        __WEBPACK_IMPORTED_MODULE_1__angular_core__["L" /* NgZone */],
        __WEBPACK_IMPORTED_MODULE_7__shared_service_pixstock_net_service__["a" /* PixstockNetService */]])
], AppModule);



/***/ }),

/***/ "../../../../../src/app/category-explorer/category-explorer.component.html":
/***/ (function(module, exports) {

module.exports = "<p>\n    category-explorer works!\n</p>\n<tree-root #tree [nodes]=\"nodes\" [options]=\"options\"\n    (activate)=\"onEvent_Activate($event)\"\n></tree-root>"

/***/ }),

/***/ "../../../../../src/app/category-explorer/category-explorer.component.scss":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "tree-root tree-viewport {\n  height: auto;\n  overflow: unset; }\n", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../src/app/category-explorer/category-explorer.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return CategoryExplorerComponent; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/esm2015/core.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_angular2_logger_core__ = __webpack_require__("../../../../angular2-logger/core.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_angular2_logger_core___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_1_angular2_logger_core__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__shared_dao_category_dao_service__ = __webpack_require__("../../../../../src/app/shared/dao/category-dao.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__shared_service_pixstock_net_service__ = __webpack_require__("../../../../../src/app/shared/service/pixstock-net.service.ts");
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};




let CategoryExplorerComponent = class CategoryExplorerComponent {
    constructor(_logger, _CategoryDaoService, _pixstock) {
        this._logger = _logger;
        this._CategoryDaoService = _CategoryDaoService;
        this._pixstock = _pixstock;
        this.nodes = [];
        this.options = {
            displayField: 'label',
            getChildren: this.getChildren.bind(this)
        };
        this.onEchoSubscriber = null;
    }
    ngOnInit() {
        this.name = this._CategoryDaoService.getName();
        // ツリーの初期化
        this.getContents(null, 1); // カテゴリID=1はルートノード
    }
    ngOnDestroy() {
        this.onEchoSubscriber.unsubscribe();
    }
    onEcho(todo) {
        this._logger.info("[Pioneer][CategoryExplorerComponent][addTodo] : イベントから取得したメッセージ=" + todo);
        this.onEchoSubscriber.unsubscribe();
    }
    /**
     * ツリーノードの選択イベントハンドラ
     *
     * @param event
     */
    onEvent_Activate(event) {
        var node = event.node;
        // メインアプリケーションの外部公開APIを呼び出す
        this._logger.info("[Pioneer][CategoryExplorerComponent][onEvent_Activate] : カテゴリツリーの選択(id=" + node.data.category.Id + ")");
        this._pixstock.ShowScreenContentList(node.data.category.Id);
    }
    /**
     * TreeViewコンポーネントがPromise型インターフェースを戻り値とするため、Promise型で返す。
     *
     * @param node
     * @returns 子階層
     */
    getChildren(node) {
        return new Promise((resolve, reject) => {
            this.getContents(node.data, node.data.category.Id);
            resolve(node.data.children);
        });
    }
    /**
     * サービスからカテゴリ情報を取得し、ツリー項目を設定する
     *
     * @param parent 親階層のノード。
     * @param categoryId
     */
    getContents(parent, categoryId) {
        let result = [];
        this._CategoryDaoService.getSubCategory(categoryId).subscribe((category) => {
            var l = new Array();
            category.forEach(prop => {
                var item = {
                    label: prop.Name,
                    category: prop,
                    hasChildren: true,
                    children: null
                };
                l.push(item);
            });
            if (parent == null) {
                // TreeViewのルートノードに設定する
                this.nodes = l;
            }
            else {
                // サブカテゴリを設定する
                parent.children = l;
            }
            result = category;
        });
        return result;
    }
};
CategoryExplorerComponent = __decorate([
    Object(__WEBPACK_IMPORTED_MODULE_0__angular_core__["m" /* Component */])({
        selector: 'app-category-explorer',
        template: __webpack_require__("../../../../../src/app/category-explorer/category-explorer.component.html"),
        styles: [__webpack_require__("../../../../../src/app/category-explorer/category-explorer.component.scss")]
    }),
    __metadata("design:paramtypes", [__WEBPACK_IMPORTED_MODULE_1_angular2_logger_core__["Logger"],
        __WEBPACK_IMPORTED_MODULE_2__shared_dao_category_dao_service__["a" /* CategoryDaoService */],
        __WEBPACK_IMPORTED_MODULE_3__shared_service_pixstock_net_service__["a" /* PixstockNetService */]])
], CategoryExplorerComponent);



/***/ }),

/***/ "../../../../../src/app/content-list/content-list.component.html":
/***/ (function(module, exports) {

module.exports = "<div class=\"card-deck\">\n    <div *ngFor=\"let listitem of contents\" class=\"card\" (dblclick)=\"showContentPreview(listitem)\">\n      <img \n        *ngIf=\"listitem.thumbnail != null\"\n        class=\"card-img-top\" src=\"{{parentUrl}}{{listitem.thumbnail.ThumbnailSourceUri}}\"\n        alt=\"Card image cap\">\n      <img \n        *ngIf=\"listitem.thumbnail == null\"\n        class=\"card-img-top\" src=\"data:image/svg+xml;charset=UTF-8,%3Csvg%20width%3D%22286%22%20height%3D%22180%22%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%20viewBox%3D%220%200%20286%20180%22%20preserveAspectRatio%3D%22none%22%3E%3Cdefs%3E%3Cstyle%20type%3D%22text%2Fcss%22%3E%23holder_160b1eea793%20text%20%7B%20fill%3Argba(255%2C255%2C255%2C.75)%3Bfont-weight%3Anormal%3Bfont-family%3AHelvetica%2C%20monospace%3Bfont-size%3A14pt%20%7D%20%3C%2Fstyle%3E%3C%2Fdefs%3E%3Cg%20id%3D%22holder_160b1eea793%22%3E%3Crect%20width%3D%22286%22%20height%3D%22180%22%20fill%3D%22%23777%22%3E%3C%2Frect%3E%3Cg%3E%3Ctext%20x%3D%22107.203125%22%20y%3D%2296.3%22%3E286x180%3C%2Ftext%3E%3C%2Fg%3E%3C%2Fg%3E%3C%2Fsvg%3E\"\n        alt=\"Card image cap\">\n  \n      <div class=\"card-body\">\n        <h5 class=\"card-title\">{{listitem.content.Name}}</h5>\n      </div>\n    </div>\n  </div>"

/***/ }),

/***/ "../../../../../src/app/content-list/content-list.component.scss":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "@media (min-width: 576px) {\n  .card-deck {\n    margin: 3px; }\n    .card-deck .card-body {\n      padding: 0; }\n      .card-deck .card-body h5 {\n        font-size: 0.75rem; }\n    .card-deck .card {\n      min-width: 100px;\n      max-width: 100px;\n      margin-left: 10px;\n      margin-right: 10px;\n      margin-top: 0;\n      margin-bottom: 8px; } }\n", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../src/app/content-list/content-list.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return ContentListComponent; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/esm2015/core.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_angular2_logger_core__ = __webpack_require__("../../../../angular2-logger/core.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_angular2_logger_core___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_1_angular2_logger_core__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__shared_dao_category_dao_service__ = __webpack_require__("../../../../../src/app/shared/dao/category-dao.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__shared_dao_thumbnail_dao_service__ = __webpack_require__("../../../../../src/app/shared/dao/thumbnail-dao.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__shared_service_pixstock_net_service__ = __webpack_require__("../../../../../src/app/shared/service/pixstock-net.service.ts");
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};





let ContentListComponent = class ContentListComponent {
    /**
   * コンストラクタ.
   *
   * @param _logger
   * @param categoryDaoService
   * @param thumbnailDaoService
   * @param router
   * @param route
   */
    constructor(_logger, 
        //private route: ActivatedRoute,
        _pixstock, categoryDaoService, thumbnailDaoService) {
        this._logger = _logger;
        this._pixstock = _pixstock;
        this.categoryDaoService = categoryDaoService;
        this.thumbnailDaoService = thumbnailDaoService;
        this.parentUrl = "";
        // iframeの親ウィンドウが、ASP.NETのHostingのため、
        // そのURLを取得する。
        var parent = window.parent; // JSのWindowオブジェクト
        this.parentUrl = parent.location;
    }
    ngOnInit() {
        // このコンポーネントでは、このイベントを受けないほうが良い。
        this.onShowContentListSubscription = this._pixstock.ShowContentList.subscribe(prop => this.onShowContentList(prop));
        this.contents = new Array();
    }
    ngOnDestroy() {
        this.onShowContentListSubscription.unsubscribe();
    }
    onShowContentList(categoryId) {
        this._logger.debug("[ContentListComponent][onShowContentList] - IN");
        this._logger.info("カテゴリ(" + categoryId + ")を表示します。");
        this.contents = new Array();
        let l_categoryId = categoryId;
        this.getContents(l_categoryId);
    }
    /**
     * リストアイテムのコンテントをコンテントプレビュー画面で表示します
     *
     * @param item 対象項目
     */
    showContentPreview(item) {
        this._logger.debug("コンポーネントクラスでイベント受取 = " + item);
        // コンテントプレビュー画面に表示切り替え
        //this.router.navigate(['/preview'], { queryParams: { id: item.content.Id } });
    }
    getContents(categoryId) {
        this.categoryDaoService.getCategory(categoryId).subscribe(category => {
            var l = new Array();
            category.Contents.forEach(element => {
                var item = new ListItem();
                item.content = element;
                if (item.content.ThumbnailKey != null) {
                    //this.getThumbnail(item, item.content.ThumbnailKey);
                }
                l.push(item);
            });
            this.contents = l;
        });
    }
    getThumbnail(item, thumbnailHash) {
        this.thumbnailDaoService.getThumbnail(thumbnailHash).subscribe(thumbanil => {
            item.thumbnail = thumbanil;
        });
    }
};
ContentListComponent = __decorate([
    Object(__WEBPACK_IMPORTED_MODULE_0__angular_core__["m" /* Component */])({
        selector: 'app-content-list',
        template: __webpack_require__("../../../../../src/app/content-list/content-list.component.html"),
        styles: [__webpack_require__("../../../../../src/app/content-list/content-list.component.scss")]
    }),
    __metadata("design:paramtypes", [__WEBPACK_IMPORTED_MODULE_1_angular2_logger_core__["Logger"],
        __WEBPACK_IMPORTED_MODULE_4__shared_service_pixstock_net_service__["a" /* PixstockNetService */],
        __WEBPACK_IMPORTED_MODULE_2__shared_dao_category_dao_service__["a" /* CategoryDaoService */],
        __WEBPACK_IMPORTED_MODULE_3__shared_dao_thumbnail_dao_service__["a" /* ThumbnailDaoService */]])
], ContentListComponent);

class ListItem {
}
/* unused harmony export ListItem */



/***/ }),

/***/ "../../../../../src/app/content-preview/content-preview.component.html":
/***/ (function(module, exports) {

module.exports = "<!-- Content-Preview(2) -->\n\n<carousel [interval]=\"0\">\n    <slide>\n        <img class=\"assets\" src=\"{{previewUrl}}\" alt=\"First slide\" style=\"display: block; width: 100%;\">\n    </slide>\n<!--\n    <slide>\n        <img class=\"assets\" src=\"assets/sample/PIC2.jpg\" alt=\"Second slide\" style=\"display: block; width: 100%;\">\n    </slide>\n    <slide>\n        <img class=\"assets\" src=\"assets/sample/PIC3.jpg\" alt=\"Third slide\" style=\"display: block; width: 100%;\">\n    </slide>\n-->\n</carousel>"

/***/ }),

/***/ "../../../../../src/app/content-preview/content-preview.component.scss":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../src/app/content-preview/content-preview.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return ContentPreviewComponent; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/esm2015/core.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_angular2_logger_core__ = __webpack_require__("../../../../angular2-logger/core.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_angular2_logger_core___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_1_angular2_logger_core__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__shared_dao_content_dao_service__ = __webpack_require__("../../../../../src/app/shared/dao/content-dao.service.ts");
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};



let ContentPreviewComponent = class ContentPreviewComponent {
    constructor(_logger, _contentDao) {
        this._logger = _logger;
        this._contentDao = _contentDao;
    }
    ngOnInit() {
    }
    updateContentPreviewAtRouting() {
        let contentId = 0;
        this._contentDao.getContentPreview(contentId).subscribe(prop => {
            this.previewUrl = prop;
        });
    }
};
ContentPreviewComponent = __decorate([
    Object(__WEBPACK_IMPORTED_MODULE_0__angular_core__["m" /* Component */])({
        selector: 'app-content-preview',
        template: __webpack_require__("../../../../../src/app/content-preview/content-preview.component.html"),
        styles: [__webpack_require__("../../../../../src/app/content-preview/content-preview.component.scss")]
    }),
    __metadata("design:paramtypes", [__WEBPACK_IMPORTED_MODULE_1_angular2_logger_core__["Logger"],
        __WEBPACK_IMPORTED_MODULE_2__shared_dao_content_dao_service__["a" /* ContentDaoService */]])
], ContentPreviewComponent);



/***/ }),

/***/ "../../../../../src/app/shared/dao/category-dao.service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return CategoryDaoService; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/esm2015/core.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_rxjs_Observable__ = __webpack_require__("../../../../rxjs/_esm2015/Observable.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__service_pixstock_net_service__ = __webpack_require__("../../../../../src/app/shared/service/pixstock-net.service.ts");
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};



let CategoryDaoService = class CategoryDaoService {
    constructor(_pixstock) {
        this._pixstock = _pixstock;
    }
    getName() {
        return "CategoryDaoです";
    }
    /**
     * RequestCategoryAPIを呼び出す
     */
    getCategory(categoryId) {
        console.debug("[CategoryDaoService] getCategory(v2) = " + this._pixstock);
        return __WEBPACK_IMPORTED_MODULE_1_rxjs_Observable__["a" /* Observable */].create(observer => {
            let result = this._pixstock.ipcRenderer.sendSync("EAV_GETCATEGORY", categoryId);
            observer.next(JSON.parse(result));
        });
    }
    /**
     * RequestCategory2APIを呼び出す
     */
    getSubCategory(categoryId) {
        console.debug("[CategoryDaoService] getSubCategory(v2) = " + this._pixstock);
        return __WEBPACK_IMPORTED_MODULE_1_rxjs_Observable__["a" /* Observable */].create(observer => {
            let result = this._pixstock.ipcRenderer.sendSync("EAV_GETSUBCATEGORY", categoryId);
            observer.next(JSON.parse(result));
        });
    }
};
CategoryDaoService = __decorate([
    Object(__WEBPACK_IMPORTED_MODULE_0__angular_core__["z" /* Injectable */])(),
    __metadata("design:paramtypes", [__WEBPACK_IMPORTED_MODULE_2__service_pixstock_net_service__["a" /* PixstockNetService */]])
], CategoryDaoService);



/***/ }),

/***/ "../../../../../src/app/shared/dao/content-dao.service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return ContentDaoService; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/esm2015/core.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_rxjs_Observable__ = __webpack_require__("../../../../rxjs/_esm2015/Observable.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__service_pixstock_net_service__ = __webpack_require__("../../../../../src/app/shared/service/pixstock-net.service.ts");
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};



let ContentDaoService = class ContentDaoService {
    constructor(_pixstock) {
        this._pixstock = _pixstock;
    }
    /**
     * コンテントプレビュー情報を取得する
     *
     * @param contentId
     * @returns
     */
    getContentPreview(contentId) {
        console.info("[ContentDaoService][getContentPreview] IN");
        // 現時点では、情報といってもただのURL文字列を取得する
        return __WEBPACK_IMPORTED_MODULE_1_rxjs_Observable__["a" /* Observable */].create(observer => {
            let result = this._pixstock.ipcRenderer.sendSync("EAV_GET_CONTENTPREVIEW", contentId);
            observer.next(result);
        });
    }
};
ContentDaoService = __decorate([
    Object(__WEBPACK_IMPORTED_MODULE_0__angular_core__["z" /* Injectable */])(),
    __metadata("design:paramtypes", [__WEBPACK_IMPORTED_MODULE_2__service_pixstock_net_service__["a" /* PixstockNetService */]])
], ContentDaoService);



/***/ }),

/***/ "../../../../../src/app/shared/dao/thumbnail-dao.service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return ThumbnailDaoService; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/esm2015/core.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_rxjs_Observable__ = __webpack_require__("../../../../rxjs/_esm2015/Observable.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__service_pixstock_net_service__ = __webpack_require__("../../../../../src/app/shared/service/pixstock-net.service.ts");
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};



let ThumbnailDaoService = class ThumbnailDaoService {
    constructor(_pixstock) {
        this._pixstock = _pixstock;
    }
    getThumbnail(thumbnailHash) {
        return __WEBPACK_IMPORTED_MODULE_1_rxjs_Observable__["a" /* Observable */].create(observer => {
            let result = this._pixstock.ipcRenderer.sendSync("EAV_GETTHUMBNAIL", thumbnailHash);
            observer.next(JSON.parse(result));
        });
    }
};
ThumbnailDaoService = __decorate([
    Object(__WEBPACK_IMPORTED_MODULE_0__angular_core__["z" /* Injectable */])(),
    __metadata("design:paramtypes", [__WEBPACK_IMPORTED_MODULE_2__service_pixstock_net_service__["a" /* PixstockNetService */]])
], ThumbnailDaoService);



/***/ }),

/***/ "../../../../../src/app/shared/service/pixstock-net.service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return PixstockNetService; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/esm2015/core.js");
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};

let PixstockNetService = class PixstockNetService {
    constructor() {
        // Event
        this.echo = new __WEBPACK_IMPORTED_MODULE_0__angular_core__["u" /* EventEmitter */]();
        this.ShowContentPreview = new __WEBPACK_IMPORTED_MODULE_0__angular_core__["u" /* EventEmitter */]();
        this.ShowContentList = new __WEBPACK_IMPORTED_MODULE_0__angular_core__["u" /* EventEmitter */]();
    }
    onMSG_SHOW_CONTENTPREVIEW(event, args) {
        this.ShowContentPreview.emit(args);
    }
    onMSG_SHOW_CONTENLIST(event, args) {
        this._logger.debug("Execute onMSG_SHOW_CONTENLIST");
        this.ShowContentList.emit(args);
    }
    /**
     * サービスの初期化
     *
     * @param _ipcRenderer IPCオブジェクト
     * @param isRpcInitialize IPCオブジェクトのイベントハンドラ登録を行うかどうかのフラグ
     */
    initialize(_ipcRenderer, isRpcInitialize, _logger) {
        this.ipcRenderer = _ipcRenderer;
        this._logger = _logger;
        if (!window['angularComponentRef_PixstockNetService']) {
            window['angularComponentRef_PixstockNetService'] = {
                // NOTE: IPCイベントをすべて登録する
                componentFn_MSG_SHOW_CONTENTPREVIEW: (event, arg) => this.onMSG_SHOW_CONTENTPREVIEW(event, arg),
                componentFn_MSG_SHOW_CONTENLIST: (event, arg) => this.onMSG_SHOW_CONTENLIST(event, arg)
            };
        }
        if (isRpcInitialize) {
            _logger.info("IPCイベントの初期化");
            this.ipcRenderer.removeAllListeners(["MSG_SHOW_CONTENTPREVIEW", "MSG_SHOW_CONTENLIST"]);
            this.ipcRenderer.on('MSG_SHOW_CONTENTPREVIEW', (event, arg) => {
                var ntv_window = window;
                ntv_window.angularComponentRef.zone.run(() => {
                    ntv_window.angularComponentRef_PixstockNetService.componentFn_MSG_SHOW_CONTENTPREVIEW(event, arg);
                });
            });
            this.ipcRenderer.on('MSG_SHOW_CONTENLIST', (event, arg) => {
                var ntv_window = window;
                ntv_window.angularComponentRef.zone.run(() => {
                    ntv_window.angularComponentRef_PixstockNetService.componentFn_MSG_SHOW_CONTENLIST(event, arg);
                });
            });
        }
    }
    /**
     * [インターフェース] ...
     *
     * @param screenId
     */
    ShowScreenContentList(categoryId) {
        this._logger.info("Execute EVT_TRNS_CONTENTLISTイベント送信");
        this.ipcRenderer.send("EVT_TRNS_CONTENTLIST", categoryId);
    }
};
PixstockNetService = __decorate([
    Object(__WEBPACK_IMPORTED_MODULE_0__angular_core__["z" /* Injectable */])()
], PixstockNetService);



/***/ }),

/***/ "../../../../../src/app/shared/service/screen.service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return ScreenService; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/esm2015/core.js");
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};

let ScreenService = class ScreenService {
    constructor() {
        this.VisibilityContentList = false;
        this.ContentListParameter = new ContentListParameter();
    }
};
ScreenService = __decorate([
    Object(__WEBPACK_IMPORTED_MODULE_0__angular_core__["z" /* Injectable */])(),
    __metadata("design:paramtypes", [])
], ScreenService);

class ContentListParameter {
}
/* unused harmony export ContentListParameter */



/***/ }),

/***/ "../../../../../src/app/shared/shared.module.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return SharedModule; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/esm2015/core.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_common__ = __webpack_require__("../../../common/esm2015/common.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__service_pixstock_net_service__ = __webpack_require__("../../../../../src/app/shared/service/pixstock-net.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__service_screen_service__ = __webpack_require__("../../../../../src/app/shared/service/screen.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__dao_category_dao_service__ = __webpack_require__("../../../../../src/app/shared/dao/category-dao.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__dao_thumbnail_dao_service__ = __webpack_require__("../../../../../src/app/shared/dao/thumbnail-dao.service.ts");
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};






let SharedModule = class SharedModule {
};
SharedModule = __decorate([
    Object(__WEBPACK_IMPORTED_MODULE_0__angular_core__["H" /* NgModule */])({
        imports: [
            __WEBPACK_IMPORTED_MODULE_1__angular_common__["a" /* CommonModule */]
        ],
        exports: [
            __WEBPACK_IMPORTED_MODULE_1__angular_common__["a" /* CommonModule */],
        ],
        providers: [
            __WEBPACK_IMPORTED_MODULE_2__service_pixstock_net_service__["a" /* PixstockNetService */],
            __WEBPACK_IMPORTED_MODULE_4__dao_category_dao_service__["a" /* CategoryDaoService */],
            __WEBPACK_IMPORTED_MODULE_5__dao_thumbnail_dao_service__["a" /* ThumbnailDaoService */],
            __WEBPACK_IMPORTED_MODULE_3__service_screen_service__["a" /* ScreenService */]
        ]
    })
], SharedModule);



/***/ }),

/***/ "../../../../../src/environments/environment.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --env=prod` then `environment.prod.ts` will be used instead.
// The list of which env maps to which file can be found in `.angular-cli.json`.
const environment = {
    production: false
};
/* harmony export (immutable) */ __webpack_exports__["a"] = environment;



/***/ }),

/***/ "../../../../../src/main.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
Object.defineProperty(__webpack_exports__, "__esModule", { value: true });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/esm2015/core.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_platform_browser_dynamic__ = __webpack_require__("../../../platform-browser-dynamic/esm2015/platform-browser-dynamic.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__app_app_module__ = __webpack_require__("../../../../../src/app/app.module.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__environments_environment__ = __webpack_require__("../../../../../src/environments/environment.ts");




if (__WEBPACK_IMPORTED_MODULE_3__environments_environment__["a" /* environment */].production) {
    Object(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_13" /* enableProdMode */])();
}
Object(__WEBPACK_IMPORTED_MODULE_1__angular_platform_browser_dynamic__["a" /* platformBrowserDynamic */])().bootstrapModule(__WEBPACK_IMPORTED_MODULE_2__app_app_module__["a" /* AppModule */])
    .catch(err => console.log(err));


/***/ }),

/***/ 0:
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__("../../../../../src/main.ts");


/***/ })

},[0]);
//# sourceMappingURL=main.bundle.js.map