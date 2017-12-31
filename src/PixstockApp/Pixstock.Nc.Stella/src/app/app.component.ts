import { Component } from '@angular/core';
import { Http } from '@angular/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'app';

  constructor(private http: Http) { }

  create() {
    alert("create!21");
    //this.getHeroes();
  }


  /**
   * Electronに対してリクエスト送信テスト
   */
  getHeroes(): Promise<string> {
    return this.http.get("/cli/Sample01/RequestCategory2",
              {
                params: {
                  CategoryId: '1'
                }
              })
               .toPromise()
               .then(response => response.json())
  }
}
