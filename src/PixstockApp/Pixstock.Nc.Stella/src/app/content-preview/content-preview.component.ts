import { Component, OnInit, OnDestroy } from '@angular/core';
import { Logger } from "angular2-logger/core";
import { ActivatedRoute, Router, Params, NavigationStart, ActivationEnd } from '@angular/router';
import { Subscription } from 'rxjs/Subscription';

@Component({
    selector: 'app-content-preview',
    templateUrl: './content-preview.component.html',
    styleUrls: ['./content-preview.component.scss']
})
export class ContentPreviewComponent implements OnInit, OnDestroy {
    /**
     * ルーティングイベントの購読
     */
    routeEvent: Subscription;

    constructor(
        private _logger: Logger,
        private router: Router,
        private route: ActivatedRoute, ) { }

    ngOnInit() {
        this._logger.debug("[Stella][ContentPreviewComponent][ngOnInit] コンポーネント初期化");

        // ルーティングイベントを購読し、同一ルーティング時のパラメータ変化を取得する
        this.routeEvent = this.router.events.subscribe(event => {
            if (event instanceof ActivationEnd) {
                let activationEnd = event as ActivationEnd;
                if (activationEnd.snapshot.component['name'] === this.constructor.name) {
                    this.updateContentPreviewAtRouting();
                }
            }
        });

        this.updateContentPreviewAtRouting();
    }

    ngOnDestroy() {
        this._logger.debug("[Stella][ContentPreviewComponent][ngOnDestroy] コンポーネント削除");
        this.routeEvent.unsubscribe(); // 購読中のルーティングイベントを破棄する
    }

    private updateContentPreviewAtRouting() {
        let contentId = 0;

        // ルーティング時のパラメータから、表示するコンテント情報のIDを取得する
        this.route.queryParams.forEach((params: Params) => {
            if (params['id'] != null) {
                contentId = params['id'];
            }
        });

        if (contentId == 0)
            throw new Error("idパラメータが未設定です");

        // TODO
        
    }
}