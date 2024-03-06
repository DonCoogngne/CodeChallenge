import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Todo } from 'src/models/todo.model';
import { TodoList } from 'src/models/todoList.model';
import { User } from 'src/models/user.model';
import { TodoService } from 'src/services/todo.service';
import { TodoVM } from 'src/viewModels/todoVM';

@Component({
  selector: 'app-todo',
  templateUrl: './todo.component.html',
  styleUrls: ['./todo.component.css']
})
export class TodoComponent implements OnInit {

  usersList : User[] = [];
  todoList : Todo[] = [];
  todoLists : TodoList[] = [];

  todoVM : TodoVM = {
    Id : 0,
    Name: '',
    DateCreated: new Date(),
    Complete : false,
    DateCompleted : new Date(),
    UserId : 0
  };

  selectedUserId: number | null = null;
  selectedTodoId: number | null = null;

  selectUser(userId: number): void {
    this.selectedUserId = userId;
  }

  selectTodo(id: number): void {
    this.selectedTodoId = id;
  }

  

  todoForm! : FormGroup;
  constructor(private route : ActivatedRoute,private fb :FormBuilder,private todoService : TodoService,private router : Router) { }

  ngOnInit(): void {
    this.todoForm = this.fb.group ({ 
    userId : 0,
    name: '',
    DateCreated : new Date(),
    complete: true,
    dateCompleted : new Date(),
    UserId: 0
    });
    this.todoService.getTodoListAll()
    .subscribe({
      next: (todoList) => {
        this.todoLists = todoList;
        this.selectedUserId = todoList[0].userId;
      },
      error: (response) => {
        console.log(response);
      }
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

    getTodoListA(id : number){
      this.todoService.getAllTodoList(id)
      .subscribe ({
        next : (response) => {
          debugger;
          this.todoList = response;
        }
      });
    }

    updateTodo(todoObject : Todo){
      this.todoVM = {
        Id : todoObject.id,
        Name: todoObject.name,
        DateCreated: todoObject.DateCreated,
        Complete : todoObject.complete,
        DateCompleted : todoObject.dateCompleted,
        UserId : todoObject.userId
      }
      this.todoService.updateTodo(this.todoVM)
      .subscribe ({
        next : (response) => {
          var ss = response;
          location.reload();
        }
      });
    }

    deleteTodo(id : number){
      this.todoService.deleteTodo(id)
      .subscribe ({
      next : (response) => {
        location.reload();
      }
    });

    }


    createTodo(){
      this.router.navigate(['Todo/AddTodo']);
    }
}
