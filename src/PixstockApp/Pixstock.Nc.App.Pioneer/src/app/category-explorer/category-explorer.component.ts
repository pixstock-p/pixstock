import { Component, OnInit, OnDestroy } from '@angular/core';
import { Logger } from "angular2-logger/core";
import { CategoryTreeItem } from './categoryTreeItem';
import { ITreeOptions, TreeNode } from 'angular-tree-component';
import { CategoryDaoService } from 'pixstock.nc.app.core/dest/src/dao/categorydao.service';
import { PixstockNetService } from 'pixstock.nc.app.core/dest/src/dao/pixstocknet.service';
import { Category } from 'pixstock.nc.app.core/dest/src/model/category';
import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'app-category-explorer',
  templateUrl: './category-explorer.component.html',
  styleUrls: ['./category-explorer.component.scss']
})
export class CategoryExplorerComponent implements OnInit, OnDestroy {

  nodes: any[] = [];

  options: ITreeOptions = {
    displayField: 'label',
    getChildren: this.getChildren.bind(this)
  };

  private onSubmitSubscriber = null;

  /**
   * コンストラクタ
   * 
   * @param _logger 
   * @param categoryDaoService 
   */
  constructor(
    private _logger: Logger,
    private categoryDaoService: CategoryDaoService,
    private _pixstock: PixstockNetService
  ) { }

  ngOnInit() {
    this._logger.debug("カテゴリエクスプローラの初期化222");

    this.onSubmitSubscriber = this._pixstock.submit.subscribe(prop => this.addTodo2(prop));

    // ツリーの初期化
    this.getContents(null, 1); // カテゴリID=1はルートノード
  }

  ngOnDestroy() {
    this.onSubmitSubscriber.unsubscribe();
  }

  onEvent_Activate(event: any) {
    var node: TreeNode = event.node;

    // メインアプリケーションの外部公開APIを呼び出す
    this._logger.info("[Pioneer][CategoryExplorerComponent][onEvent_Activate] : カテゴリツリーの選択(id=" + node.data.category.Id);
    var pa: any = window.parent;
    pa.showContentListPanelByCategory(node.data.category.Id);
  }

  addTodo2(todo: string) {
    this._logger.info("[Pioneer][CategoryExplorerComponent][addTodo] : イベントから取得したメッセージ=" + todo);

    this.onSubmitSubscriber.unsubscribe();
  }

  /**
   * TreeViewコンポーネントがPromise型インターフェースを戻り値とするため、Promise型で返す。
   * 
   * @param node 
   * @returns 子階層
   */
  getChildren(node: TreeNode) {
    return new Promise((resolve, reject) => {
      this.getContents(node.data, node.data.category.Id)
      resolve(node.data.children);
    });
  }

  /**
   * サービスからカテゴリ情報を取得し、ツリー項目を設定する
   * 
   * @param parent 親階層のノード。
   * @param categoryId 
   */
  private getContents(parent: CategoryTreeItem, categoryId: Number): Category[] {
    let result: Category[] = [];
    this.categoryDaoService.getSubCategory(categoryId).subscribe((category) => {
      var l = new Array<CategoryTreeItem>();
      category.forEach(prop => {
        var item: CategoryTreeItem = {
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
      } else {
        // サブカテゴリを設定する
        parent.children = l;
      }

      result = category;
    });
    return result;
  }
}
