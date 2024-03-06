import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from 'src/models/user.model';
import { TodoService } from 'src/services/todo.service';
import { TodoVM } from 'src/viewModels/todoVM';

@Component({
  selector: 'app-add-todo',
  templateUrl: './add-todo.component.html',
  styleUrls: ['./add-todo.component.css']
})
export class AddTodoComponent implements OnInit {
  usersList : User[] = [];

  todoVM : TodoVM = {
    Id : 0,
    Name: '',
    DateCreated: new Date(),
    Complete : false,
    DateCompleted : new Date(),
    UserId : 0
  };

  todoForm! : FormGroup;
  constructor(private route : ActivatedRoute,private fb :FormBuilder,private todoService : TodoService,private router : Router) { }

  ngOnInit(): void {
    this.todoForm = this.fb.group ({ 
      Id : 0,
      Name: '',
      DateCreated: new Date(),
      Complete : false,
      DateCompleted : new Date(),
      UserId : 0
      });
    this.todoService.getAllUsers()
    .subscribe({
      next: (users) => {
        this.usersList = users;
      },
      error: (response) => {
        console.log(response);
      }
    });
  }

  createTodo(){
    debugger;
    if(this.todoVM.UserId){
      this.todoService.saveProduce(this.todoVM)
      .subscribe({
        next : (response) => {
          this.router.navigate(['Todo']);
        }
      });
    }     
  }

}
