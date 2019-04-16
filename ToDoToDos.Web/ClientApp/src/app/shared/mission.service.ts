import { Injectable } from '@angular/core';
import { Mission } from './mission.model';
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class MissionService {
  formData: Mission
  readonly rootUrl = 'http://localhost:50732/api';
  list : Mission[];

  constructor(private http: HttpClient) { }

  create() {
    return this.http.post(this.rootUrl + '/Mission', this.formData);
  }

  update(){
    return this.http.put(this.rootUrl + '/Mission/'+ this.formData.MissionID, this.formData);
  }

  updateList(){
    return this.http.put(this.rootUrl + '/Mission', this.list);
  }

  delete(id) {
    return this.http.delete(this.rootUrl + '/Mission/'+ id);
  }

  refreshList(){
    this.http.get(this.rootUrl + '/Mission')
              .toPromise()
              .then(res => this.list = res as Mission[]);
    }

  sortList(btn: number) {
    if (btn == 0) {
      this.list.sort((a,b) => {
        var date1 = new Date(a.DateToDo);
        var date2 = new Date(b.DateToDo);

        return date1.getTime() - date2.getTime();
      });
    }
    else if (btn == 1) {
      this.list.sort((a,b) => {
        var date1 = new Date(a.Created);
        var date2 = new Date(b.Created);

        return date1.getTime() - date2.getTime();
      });
    } 
    else if (btn == 2) {
      this.list.sort((a,b) => b.Importance - a.Importance);
    } 
  }
}
