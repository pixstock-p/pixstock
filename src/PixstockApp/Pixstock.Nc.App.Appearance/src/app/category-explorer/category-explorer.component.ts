import { Component, OnInit, OnDestroy } from '@angular/core';
import { Logger } from "angular2-logger/core";

import { ITreeOptions, TreeNode } from 'angular-tree-component';

import { CategoryDaoService } from './../shared/dao/category-dao.service';
import { Category } from '../shared/model/category';
import { PixstockNetService } from '../shared/service/pixstock-net.service';

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

  private onEchoSubscriber = null;

  constructor(
    private _logger: Logger,
    private _CategoryDaoService: CategoryDaoService,
    private _pixstock: PixstockNetService
  ) { }

  name: string;

  ngOnInit() {
    this.name = this._CategoryDaoService.getName();

    // ツリーの初期化
    this.getContents(null, 1); // カテゴリID=1はルートノード
  }


  ngOnDestroy() {
    this.onEchoSubscriber.unsubscribe();
  }

  onEcho(todo: string) {
    this._logger.info("[Pioneer][CategoryExplorerComponent][addTodo] : イベントから取得したメッセージ=" + todo);

    this.onEchoSubscriber.unsubscribe();
  }

  /**
   * ツリーノードの選択イベントハンドラ
   * 
   * @param event 
   */
  onEvent_Activate(event: any) {
    var node: TreeNode = event.node;

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
    this._CategoryDaoService.getSubCategory(categoryId).subscribe((category) => {
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

export interface CategoryTreeItem {
  label: string;
  category?: Category;
  hasChildren: boolean;
  children: CategoryTreeItem[];
}