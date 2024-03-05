import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Todo } from 'src/models/todo.model';
import { TodoList } from 'src/models/todoList.model';
import { User } from 'src/models/user.model';
import { TodoService } from 'src/services/todo.service';

@Component({
  selector: 'app-todo',
  templateUrl: './todo.component.html',
  styleUrls: ['./todo.component.css']
})
export class TodoComponent implements OnInit {

  usersList : User[] = [];
  todoList : Todo[] = [];
  todoLists : TodoList[] = [];

  selectedUserId: number | null = null;

  selectUser(userId: number): void {
    this.selectedUserId = userId;
  }

  todoobject : Todo = {
    userId : 0,
    name: '',
    DateCreated : new Date(),
    Complete: false,
    DateCompleted : new Date(),
    UserId: 0
  };
  constructor(private route : ActivatedRoute,private todoService : TodoService) { }

  ngOnInit(): void {
    this.todoService.getTodoListAll()
    .subscribe({
      next: (todoList) => {
        debugger;
        this.todoLists = todoList;
        this.selectedUserId = todoList[0].userId;
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
}
