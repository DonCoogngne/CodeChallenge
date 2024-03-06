import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TodoComponent } from './components/todo/todo.component';
import { AddTodoComponent } from './components/todo/add-todo/add-todo.component';

const routes: Routes = [
  {path:'Todo', component:TodoComponent},
  {path:'Todo/AddTodo', component:AddTodoComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
