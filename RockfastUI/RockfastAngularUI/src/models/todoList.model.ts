import { Todo } from "./todo.model";

export interface TodoList {
    userId : number;
    name : string;
    toDoList : Todo[]
}