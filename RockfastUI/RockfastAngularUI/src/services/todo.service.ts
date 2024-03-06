import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { User } from 'src/models/user.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Todo } from 'src/models/todo.model';
import { TodoList } from 'src/models/todoList.model';
import { TodoVM } from 'src/viewModels/todoVM';

@Injectable({
  providedIn: 'root'
})
export class TodoService {

  private baseUrl:string = environment.baseUrl;
  constructor(private http : HttpClient) { }

  getAllUsers():Observable<User[]>{
    return this.http.get<User[]>(`${this.baseUrl}GetAllUsers`);
  }

  getAllTodoList(userId : number):Observable<Todo[]>{
    return this.http.get<Todo[]>(`${this.baseUrl}GeUserTodoList?uerid=` + userId);
  }

  getTodoListAll():Observable<TodoList[]>{
    return this.http.get<TodoList[]>(`${this.baseUrl}GeTodoListAll`);
  }

  saveProduce(todoObj:any){
    return this.http.post<TodoVM>(`${this.baseUrl}CreateTodo`,todoObj);
  }

   updateTodo(updateTodo : TodoVM) : Observable<TodoVM>{
     return this.http.put<TodoVM>(`${this.baseUrl}UpdateTodo`, updateTodo);
  }

  deleteTodo(id : Number) : Observable<Todo> {
    const httpOptions = {
      headers: new HttpHeaders({'Content-Type': 'application/json'})
    }

    return this.http.delete<Todo>(`${this.baseUrl}User/` + id, httpOptions);
  
  }
}
