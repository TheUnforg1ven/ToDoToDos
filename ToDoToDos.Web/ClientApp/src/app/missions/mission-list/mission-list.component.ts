import { Component, OnInit } from '@angular/core';
import { MissionService } from 'src/app/shared/mission.service';
import { ToastrService } from 'ngx-toastr';
import { Mission } from 'src/app/shared/mission.model';

import { moveItemInArray, CdkDragDrop } from '@angular/cdk/drag-drop';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-mission-list',
  templateUrl: './mission-list.component.html',
  styles: []
})
export class MissionListComponent implements OnInit {

  constructor(private service: MissionService,
    private toastr: ToastrService) { }

    ngOnInit() {
      if (this.service.list == null) {
        this.service.refreshList();
      }
    }

    onDrop(event: CdkDragDrop<string[]>) {
      moveItemInArray(this.service.list, event.previousIndex, event.currentIndex);
    }

    populateForm(m: Mission) {
      this.service.formData = Object.assign({}, m);
    }

    onSuccess(m: Mission) {
      if(m.IsDone == false){
         m.IsDone = true;
        this.service.formData = m;

        this.service.update()
        .subscribe(res => {
          debugger;
          this.service.refreshList();
          this.toastr.success('To-Do is done', 'To-Do To-Dos');
        },
          err => {
            debugger;
            console.log(err);
        })

        this.resetForm();
      }
    }

    onUnSuccess(m: Mission) {
      if(m.IsDone == true){
         m.IsDone = false;
        this.service.formData = m;

        this.service.update()
        .subscribe(res => {
          debugger;
          this.service.refreshList();
          this.toastr.warning('To-Do is now not done', 'To-Do To-Dos');
        },
          err => {
            debugger;
            console.log(err);
        })

        this.resetForm();
      }
    }
  
    onDelete(MissionID: number) {
      if (confirm('Are you sure to delete your mission?')) {
        this.service.delete(MissionID)
          .subscribe(res => {
            debugger;
            this.service.refreshList();
            this.toastr.info('Deleted successfully', 'To-Do To-Dos');
          },
            err => {
              debugger;
              console.log(err);
            })
      }
    }

    onSort(btn: number) {
      this.service.sortList(btn);
      (err: any) => {
        debugger;
        console.log(err);
      }
    }

    onSave() {
      if (this.service.list != null)
        this.saveList();
    }

    isChanged(isDone: boolean){
      if(isDone == true) {
        const style = {'background' : 'lightgreen'};
        return style;
      }
    }

    saveList(){
      this.service.updateList().subscribe(
        res => {
          debugger;
          this.toastr.success('To-Do list succesfully saved', 'To-Do To-Dos');
          this.service.refreshList();
        })
    }

    resetForm(){
      this.service.formData = {
        MissionID: 0,
        Name: '',
        Description: '',
        DateToDo: new Date(),
        Created: new Date(),
        IsDone: false,
        Importance: 1
      }
    }

   searchMission(value: string){
      if(!value)
          this.service.refreshList();

      this.service.list = Object.assign([], this.service.list)
                                .filter(m => m.Name.toLowerCase().indexOf(value.toLowerCase()) > -1);
   }
}
