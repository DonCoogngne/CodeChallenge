import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { User } from 'src/models/user.model';
import { HttpClient } from '@angular/common/http';
import { Todo } from 'src/models/todo.model';
import { TodoList } from 'src/models/todoList.model';

@Injectable({
  providedIn: 'root'
})
export class TodoService {

  private baseUrl:string = environment.baseUrl;
  constructor(private http : HttpClient) { }

  getAllUsers():Observable<User[]>{
    debugger;
    return this.http.get<User[]>(`${this.baseUrl}GetAllUsers`);
  }

  getAllTodoList(userId : number):Observable<Todo[]>{
    debugger;
    return this.http.get<Todo[]>(`${this.baseUrl}GeUserTodoList?uerid=` + userId);
  }

  getTodoListAll():Observable<TodoList[]>{
    debugger;
    return this.http.get<TodoList[]>(`${this.baseUrl}GeTodoListAll`);
  }
}
