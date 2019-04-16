import { MissionService } from 'src/app/shared/mission.service';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-mission',
  templateUrl: './mission.component.html',
  styles: []
})
export class MissionComponent implements OnInit {

  constructor(private service: MissionService,
    private toastr: ToastrService) { }

  ngOnInit() {
    this.resetForm();
  }

  resetForm(form?: NgForm) {
    if (form != null)
      form.form.reset();
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

  onSubmit(form: NgForm) {
    if (this.service.formData.MissionID == 0)
      this.insertRecord(form);
    else
      this.updateRecord(form);
  }

  insertRecord(form: NgForm) {
    this.service.create().subscribe(
      res => {
        debugger;
        this.resetForm(form);
        this.toastr.success('Submitted successfully', 'To-Do To-Dos');
        this.service.refreshList();
      },
      err => {
        debugger;
        console.log(err);
      }
    )
  }

  updateRecord(form: NgForm) {
    this.service.update().subscribe(
      res => {
        this.resetForm(form);
        this.toastr.success('Submitted successfully', 'To-Do To-Dos');
        this.service.refreshList();
      },
      err => {
        console.log(err);
      }
    )
  }
}
