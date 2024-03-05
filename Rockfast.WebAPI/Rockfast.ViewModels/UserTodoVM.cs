using System;
namespace Rockfast.ViewModels
{
	public class UserTodoVM
	{
        public int UserId { get; set; }
        public List<TodoVM> ToDoList { get; set; }
    }
}

